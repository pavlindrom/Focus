using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.CodeAnalysis.MSBuild;
using System;

namespace Gr.Pavlo.Focus
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Bootstrap();

            var path = args[0];

            var workspace = MSBuildWorkspace.Create();
            var solution = workspace.OpenSolutionAsync(path).GetAwaiter().GetResult();

            Processor.Process(solution, container);

            Console.WriteLine("All done, exiting is encouraged.");
            Console.ReadLine();
        }

        static IWindsorContainer Bootstrap()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IContext>().ImplementedBy<Context>().LifestyleSingleton());
            container.Register(Component.For<IDatabase>()
                .Instance(new Database("bolt://localhost:7687", "neo4j", "graph")));
            
            container.Install(
                new Processors.DependencyInstaller(),
                new Traversers.DependencyInstaller());

            container.Register(Component.For<IWindsorContainer>().Instance(container));

            return container;
        }
    }
}
