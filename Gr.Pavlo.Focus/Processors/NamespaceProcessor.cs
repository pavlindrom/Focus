using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus.Processors
{
    internal class NamespaceProcessor : BaseProcessor<NamespaceDeclarationSyntax>
    {
        public NamespaceProcessor(IContext context, NamespaceDeclarationSyntax item)
            : base(context, item)
        { }

        public override Tuple<StructuralType, long> Insert()
        {
            
        }
    }
}
