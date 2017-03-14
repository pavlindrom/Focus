using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.CodeAnalysis;

namespace Gr.Pavlo.Focus.Traversers
{
    internal class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<BaseTraversable<Solution>>().ImplementedBy<SolutionTraverser>());
        }
    }
}
