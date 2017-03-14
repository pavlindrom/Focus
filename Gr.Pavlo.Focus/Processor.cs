using Castle.MicroKernel.Registration;
using Castle.Windsor;
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

        static void Process(Type type, object item, IWindsorContainer container)
        {
            container.Register(Component.For(type).Instance(item));

            var genericProcessorType = typeof(BaseProcessor<>);
            var processorType = genericProcessorType.MakeGenericType(type);

            var processor = (IProcessor)container.Resolve(processorType);
            var result = processor.Process();

            using (var childContainer = container.CreateChildContainerFromResult(result.Type, result.Id))
            {
                Traverse(type, item, childContainer);
            }
        }

        static void Traverse(Type type, object item, IWindsorContainer container)
        {
            var genericTraverserType = typeof(BaseTraversable<>);
            var traverserType = genericTraverserType.MakeGenericType(type);
            var traverser = (ITraversable)container.Resolve(traverserType);
            foreach (var descendant in traverser.GetConnections())
            {
                Process(descendant.GetType(), descendant, container);
            }
        }
    }
}
