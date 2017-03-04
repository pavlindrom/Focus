using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus.Collections
{
    internal class Traversable<T>: List<ITraversableItem>, ITraversable
    {
        public T Item { get; private set; }

        public Traversable(T item)
        {
            Item = item;
        }

        public new Traversable<T> Add(ITraversableItem item)
        {
            base.Add(item);
            return this;
        }
    }
}
