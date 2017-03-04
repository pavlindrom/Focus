using Gr.Pavlo.Focus.Collections;
using Gr.Pavlo.Focus.Processors;
using Gr.Pavlo.Focus.Traversers;
using System;

namespace Gr.Pavlo.Focus
{
    internal static class Processor
    {
        public static void Process<T>(T item)
        {
            Process(typeof(T), item);
        }

        public static void Process(Type type, object item)
        {
            var genericProcessorType = typeof(BaseProcessor<>);
            var processorType = genericProcessorType.MakeGenericType(type);

            var processor = (IProcessor)Program.DependencyContainer.Resolve(processorType, item);
            processor.Process();

            Traverse(type, item);
        }

        static void Traverse(Type type, object item)
        {
            var genericTraverserType = typeof(BaseTraverser<>);
            var traverserType = genericTraverserType.MakeGenericType(type);
            var traverser = (ITraversable)Program.DependencyContainer.Resolve(traverserType, item);
            foreach (var descendants in traverser)
            {
                Process(descendants.Value.GetType(), descendants.Value);
            }
        }
    }
}
