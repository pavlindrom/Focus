using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus
{
    class Program
    {
        public static IWindsorContainer DependencyContainer;

        static void Main(string[] args)
        {
            Bootstrap();

            var path = args[0];

            var workspace = MSBuildWorkspace.Create();
            var solution = workspace.OpenSolutionAsync(path).GetAwaiter().GetResult();

            Processor.Process(solution);

            Console.WriteLine("All done, exiting is encouraged.");
            Console.ReadLine();
        }

        static void Bootstrap()
        {
            DependencyContainer = new WindsorContainer();
            DependencyContainer.Register(Component.For<IContext>());
            DependencyContainer.Register(Component.For<IDatabase>()
                .Instance(new Database("bolt://localhost:7687", "neo4j", "graph")));

            DependencyContainer.Install(
                FromAssembly.Named("Gr.Pavlo.Focus.Processors"),
                FromAssembly.Named("Gr.Pavlo.Focus.Traversers"));
        }
    }
}
