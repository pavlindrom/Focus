using System;
using Microsoft.CodeAnalysis;

namespace Gr.Pavlo.Focus.Processors
{
    internal class SolutionProcessor: BaseProcessor<Solution>
    {
        public SolutionProcessor(IContext context, Solution solution)
            :base(context, solution)
        { }

        public override Tuple<StructuralType, long> Insert()
        {
            
        }
    }
}
