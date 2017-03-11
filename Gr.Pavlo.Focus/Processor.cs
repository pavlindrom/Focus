using Castle.Windsor;
using Gr.Pavlo.Focus.Collections;
using Gr.Pavlo.Focus.Processors;
using Gr.Pavlo.Focus.Traversers;
using System;

namespace Gr.Pavlo.Focus
{
    internal static class Processor
    {
        public static void Process<T>(T item, IWindsorContainer container)
        {
            Process(typeof(T), item, container);
        }

        public static void Process(Type type, object item, IWindsorContainer container)
        {
            var genericProcessorType = typeof(BaseProcessor<>);
            var processorType = genericProcessorType.MakeGenericType(type);

            var processor = (IProcessor)container.Resolve(processorType, new { item });
            var result = processor.Process();

            using (var childContainer = container.CreateChildContainerFromResult(result.Type, result.Id))
            {
                Traverse(type, item, childContainer);
            }
        }

        static void Traverse(Type type, object item, IWindsorContainer container)
        {
            var genericTraverserType = typeof(Traversable<>);
            var traverserType = genericTraverserType.MakeGenericType(type);
            var traverser = (ITraversable)container.Resolve(traverserType, new { item });
            foreach (var descendants in traverser)
            {
                Process(descendants.Value.GetType(), descendants.Value, container);
            }
        }
    }
}
