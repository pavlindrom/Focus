using Gr.Pavlo.Focus.Collections;
using Microsoft.CodeAnalysis;

namespace Gr.Pavlo.Focus.Traversers
{
    internal class SolutionTraverser : Traversable<Solution>
    {
        public override ITraversable GetConnections()
            => AddField(s => s.Projects);
    }
}
