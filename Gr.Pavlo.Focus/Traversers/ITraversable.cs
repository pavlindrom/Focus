using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus.Traversers
{
    internal interface ITraversable
    {
        IEnumerable<object> GetConnections();
    }
}
