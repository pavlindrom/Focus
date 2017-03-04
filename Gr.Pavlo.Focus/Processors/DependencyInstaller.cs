using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;

namespace Gr.Pavlo.Focus.Processors
{
    internal class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes
                .FromAssembly(GetType().Assembly)
                .BasedOn(typeof(BaseProcessor<>))
                .LifestyleTransient());
        }
    }
}
