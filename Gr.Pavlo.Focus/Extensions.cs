using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Gr.Pavlo.Focus
{
    internal static class Extensions
    {
        public static IWindsorContainer CreateChildContainerFromResult(this IWindsorContainer container, StructuralType type, long id)
        {
            var childContext = container.Resolve<IContext>().Extend(type, id);

            var childContainer = new WindsorContainer();
            childContainer.Register(Component.For<IWindsorContainer>().Instance(childContainer));
            childContainer.Register(Component.For<IContext>().Instance(childContext));

            container.AddChildContainer(childContainer);
            return childContainer;
        }
    }
}
