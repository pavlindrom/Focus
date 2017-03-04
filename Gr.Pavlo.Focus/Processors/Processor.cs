using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus.Processors
{
    internal abstract class Processor<T>
    {
        public IContext Context { get; private set; }

        public T Item { get; private set; }

        public Processor(IContext context, T item)
        {
            Context = context;
            Item = item;
        }

        public virtual void Process()
        {
            var inserted = Insert();
            Context.Add(inserted.Item1, inserted.Item2);
        }

        public virtual Tuple<StructuralType, long> Insert()
        {
            throw new NotImplementedException("A processor must either override Process, or Insert.");
        }

        public abstract IEnumerable<object> GetChildren();
    }
}
