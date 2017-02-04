using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Gr.Pavlo.Focus
{
    class SyntaxNodeVisitor : CSharpSyntaxWalker
    {
        HashSet<Type> seenTypes = new HashSet<Type>();

        public override void Visit(SyntaxNode node)
        {
            var type = node.GetType();
            if (!seenTypes.Contains(type))
            {
                Console.WriteLine(type.FullName);
                foreach(var prop in type.GetProperties())
                {
                    if (prop.DeclaringType == type)
                        Console.WriteLine($"   {prop.Name}: {prop.PropertyType.FullName}");
                }
                seenTypes.Add(type);
            }
            base.Visit(node);
        }
    }
}
