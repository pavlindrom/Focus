namespace Gr.Pavlo.Focus.Processors
{
    internal abstract class BaseProcessor<T>: IProcessor
    {
        public IContext Context { get; set; }

        public IDatabase Database { get; set; }

        public T Item { get; set; }

        public abstract (StructuralType Type, long Id) Process();
    }
}
