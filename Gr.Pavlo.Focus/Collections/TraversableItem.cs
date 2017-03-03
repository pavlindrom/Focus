using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus.Collections
{
    public interface ITraversableKey
    {
        string Name { get; }
    }

    public interface ITraversableItem<TItem>: ITraversableKey
    {
        TItem Value { get; }
    }

    public class TraversableItem<TItem> : ITraversableItem<TItem>
    {
        public string Name { get; private set; }

        public TItem Value { get; private set; }

        public TraversableItem(string name, TItem value)
        {
            Name = name;
            Value = value;
        }
    }

    public class TraversableItem<TSource, TItem> : ITraversableItem<TItem>
    {
        public TSource Source { private get; set; }

        public Func<TSource, TItem> Delegate { get; private set; }

        public string Name { get; private set; }

        public TItem Value
        {
            get
            {
                if (Source == null)
                    throw new ArgumentNullException(nameof(Source));
                return Delegate(Source);
            }
        }

        public TraversableItem(string name, Func<TSource, TItem> @delegate)
        {
            Name = name;
            Delegate = @delegate;
        }

        public static implicit operator TraversableItem<TSource, TItem>(Expression<Func<TSource, TItem>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            var name = member.Member.Name;
            var del = expression.Compile();

            return new TraversableItem<TSource, TItem>(name, del);
        }

        public static implicit operator TraversableItem<TItem>(TraversableItem<TSource, TItem> expressive)
        {
            return new TraversableItem<TItem>(expressive.Name, expressive.Value);
        }
    }
}
