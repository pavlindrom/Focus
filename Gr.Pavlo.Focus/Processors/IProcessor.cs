using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus.Processors
{
    internal interface IProcessor<T>
    {
        StructuralType Type { get; }

        long Process(T node);

        IEnumerable<TChildren> GetChildren<TChildren>();
    }
}
