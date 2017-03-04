using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Gr.Pavlo.Focus.Traversers
{
    internal class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes
                .FromAssembly(GetType().Assembly)
                .BasedOn(typeof(Traversable<>))
                .LifestyleTransient());
        }
    }
}
