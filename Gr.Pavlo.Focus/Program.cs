using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus
{
    class Program
    {
        public static IUnityContainer DependencyContainer = new UnityContainer();

        static void Main(string[] args)
        {
            Bootstrap();

            // http://stackoverflow.com/a/31155633/2048017
            ExecuteAsync(args).GetAwaiter().GetResult();

            Console.WriteLine("All done, exiting is encouraged.");
            Console.ReadLine();
        }

        static void Bootstrap()
        {
            DependencyContainer.RegisterType<IContext>();
        }

        static async Task ExecuteAsync(string[] args)
        {
            var path = args[0];

            var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(path);

            using (var db = new Database("bolt://localhost:7687", "neo4j", "graph"))
            {
                var s = db.Node("Solution", new Dictionary<string, object>
                {
                    { "name", Path.GetFileNameWithoutExtension(path) }
                });

                foreach (var project in solution.Projects)
                {
                    var p = db.Node("Project", new Dictionary<string, object>
                    {
                        { "name", project.Name },
                        { "assembly", project.AssemblyName }
                    });

                    db.Relationship(s, p, "PROJECT");
                    
                    var compilation = await project.GetCompilationAsync();
                    foreach (var tree in compilation.SyntaxTrees)
                    {
                        var c = db.Node("File", new Dictionary<string, object>
                        {
                            { "name", new Uri(tree.FilePath).MakeRelativeUri(new Uri(path)).ToString().Replace("/", "\\") }
                        });

                        db.Relationship(p, c, "CONTENT");
                    }
                }
            }
        }
    }
}
