using Castle.MicroKernel;
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
            using (var childContainer = new WindsorContainer())
            {
                container.AddChildContainer(childContainer);
                container.Register(Component.For(type).Instance(item).OnlyNewServices());

                var genericProcessorType = typeof(BaseProcessor<>);
                var processorType = genericProcessorType.MakeGenericType(type);

                try
                {
                    var processor = (IProcessor)childContainer.Resolve(processorType);
                    var result = processor.Process();

                    var context = childContainer.Resolve<IContext>();
                    childContainer.Register(Component.For<IContext>().Instance(context.Extend(result.Type, result.Id)));
                }
                catch (ComponentNotFoundException)
                {
                    // some kind of logging here :)
                }

                Traverse(type, item, childContainer);
            }
        }

        static void Traverse(Type type, object item, IWindsorContainer container)
        {
            var genericTraverserType = typeof(BaseTraversable<>);
            var traverserType = genericTraverserType.MakeGenericType(type);

            try
            {
                var traverser = (ITraversable)container.Resolve(traverserType);
                foreach (var descendant in traverser.GetConnections())
                {
                    Process(descendant.GetType(), descendant, container);
                }
            }
            catch (ComponentNotFoundException)
            {
                // same kind of logging here :)
            }
        }
    }
}
