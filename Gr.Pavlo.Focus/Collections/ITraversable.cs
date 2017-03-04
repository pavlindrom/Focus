using System.Collections.Generic;

namespace Gr.Pavlo.Focus.Collections
{
    public interface ITraversable: IEnumerable<ITraversableItem>
    {
        ITraversable GetConnections();
    }
}
