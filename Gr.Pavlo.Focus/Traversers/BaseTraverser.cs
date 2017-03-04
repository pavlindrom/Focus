using Gr.Pavlo.Focus.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus.Traversers
{
    internal abstract class BaseTraverser<T>: Traversable<T>
    {
        public BaseTraverser(T item)
            : base(item)
        { }

        protected Traversable<T> Get(ITraversableItem item)
            => Add(item);
    }
}
