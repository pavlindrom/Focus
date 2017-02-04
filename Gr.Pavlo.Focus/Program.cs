using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.MSBuild;
using System.IO;

namespace Gr.Pavlo.Focus
{
    class Program
    {
        static void Main(string[] args)
        {
            // http://stackoverflow.com/a/31155633/2048017
            ExecuteAsync(args).GetAwaiter().GetResult();

            Console.WriteLine("All done, exiting is encouraged.");
            Console.ReadLine();
        }

        static async Task ExecuteAsync(string[] args)
        {
            var path = args[0];

            var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(path);

            using (var db = new Database("bolt://raspberrypi.local:7687", "neo4j", "graph"))
            {
                var s = db.Node("Solution", new Dictionary<string, object>
                {
                    { "name", Path.GetFileNameWithoutExtension(path) }
                });

                var walker = new SyntaxNodeVisitor();

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
                        Console.WriteLine($"  {tree.FilePath}");
                        walker.Visit(tree.GetRoot());
                    }
                }
            }
        }
    }
}
