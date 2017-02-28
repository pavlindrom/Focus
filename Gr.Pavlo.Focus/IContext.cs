using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus
{
    interface IContext
    {
        long PackageId { get; }

        long ClassId { get; }

        long StaticClassId { get; }

        long SubClassId { get; }

        long MethodId { get; }

        long BlockId { get; }

        void Add(StructuralType type, long id);

        void Remove(StructuralType type);
    }
}
