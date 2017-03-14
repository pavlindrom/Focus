using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Gr.Pavlo.Focus.Traversers
{
    internal class SolutionTraverser : BaseTraversable<Solution>
    {
        public override IEnumerable<object> GetConnections()
            => Item.Projects;
    }
}
