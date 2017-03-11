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

        public abstract (StructuralType Type, long Id) Process();
    }
}
