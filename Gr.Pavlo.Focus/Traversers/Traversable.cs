using Gr.Pavlo.Focus.Collections;
using System;
using System.Collections.Generic;

namespace Gr.Pavlo.Focus.Traversers
{
    internal abstract class Traversable<T>: List<ITraversableItem>, ITraversable
    {
        public T Item { get; private set; }

        public abstract ITraversable GetConnections();

        internal Traversable<T> AddItem(ITraversableItem item)
        {
            Add(item);
            return this;
        }

        internal Traversable<T> AddField<TField>(Func<T, TField> @delegate)
            => AddItem(@delegate as ITraversableItem);
    }
}
