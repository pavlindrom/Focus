using Gr.Pavlo.Focus.Collections;
using System;
using System.Linq.Expressions;

namespace Gr.Pavlo.Focus.Traversers
{
    public class TraversableItem<TSource, TItem> : ITraversableItem
    {
        public TSource Source { private get; set; }

        public Func<TSource, TItem> Delegate { get; private set; }

        public string Name { get; private set; }

        public object Value
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
    }
}
