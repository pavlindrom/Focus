using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus.Collections
{
    public interface ITraversable: IEnumerable
    {
    }

    public interface ITraversable<T>: IEnumerable<ITraversableKey>
    {

    }
}
