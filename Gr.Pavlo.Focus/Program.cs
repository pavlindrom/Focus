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
            ExecuteAsync(args).Wait();

            Console.ReadLine();
        }

        static async Task ExecuteAsync(string[] args)
        {
            var path = args[0];

            var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(path);

            using (var db = new Database("bolt://raspberrypi.local:7867", "neo4j", "graph"))
            {
                var s = db.Node("Solution", new Dictionary<string, object>
                {
                    { "id", solution.Id.Id },
                    { "name", Path.GetFileNameWithoutExtension(path) },
                    { "version", solution.Version }
                });

                var walker = new SyntaxNodeVisitor();

                foreach (var project in solution.Projects)
                {
                    var p = db.Node("Project", new Dictionary<string, object>
                    {
                        { "id", project.Id.Id },
                        { "name", project.Name },
                        { "assembly", project.AssemblyName },
                        { "version", project.Version }
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
