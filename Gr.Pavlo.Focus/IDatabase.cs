using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gr.Pavlo.Focus
{
    public interface IDatabase
    {
        long CreateNode(string label, Dictionary<string, object> parameters = null);

        long CreateNode(IEnumerable<string> labels, Dictionary<string, object> parameters = null);

        long CreateRelationship(long from, long to, string name, Dictionary<string, object> parameters = null);
    }
}
