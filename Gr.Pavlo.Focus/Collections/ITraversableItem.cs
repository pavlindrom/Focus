using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus.Collections
{
    public interface ITraversableItem
    {
        string Name { get; }

        object Value { get; }
    }
}
