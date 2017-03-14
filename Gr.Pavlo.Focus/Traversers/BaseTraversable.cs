using System.Collections.Generic;

namespace Gr.Pavlo.Focus.Traversers
{
    internal abstract class BaseTraversable<T>: ITraversable
    {
        public T Item { get; set; }

        public abstract IEnumerable<object> GetConnections();
    }
}
