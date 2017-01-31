using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.MSBuild;

namespace Gr.Pavlo.Focus
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteAsync(args).Wait();

            Console.ReadLine();
        }

        static async Task ExecuteAsync(string[] args)
        {
            var path = args[0];

            var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(path);
        }
    }
}
