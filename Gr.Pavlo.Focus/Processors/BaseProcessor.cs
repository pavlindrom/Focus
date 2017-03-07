using System;

namespace Gr.Pavlo.Focus.Processors
{
    internal abstract class BaseProcessor<T>: IProcessor
    {
        public IContext Context { get; set; }

        public IDatabase Database { get; set; }

        public T Item { get; private set; }

        public BaseProcessor(T item)
        {
            Item = item;
        }

        void IProcessor.Process()
        {
            var inserted = Insert();
            Context.Add(inserted.Item1, inserted.Item2);
        }

        public abstract Tuple<StructuralType, long> Insert();
    }
}
