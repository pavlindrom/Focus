using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gr.Pavlo.Focus
{
    internal class Database : IDatabase, IDisposable
    {
        IDriver driver;
        ISession session;

        public Database(string path, string username, string password)
        {
            driver = GraphDatabase.Driver(path, AuthTokens.Basic(username, password));
            session = driver.Session();
        }

        public long CreateNode(string label, Dictionary<string, object> parameters = null)
            => CreateNode(new List<string> { label }, parameters);

        public long CreateNode(IEnumerable<string> labels, Dictionary<string, object> parameters = null)
            => session.Run($@"
                    MERGE (n:{string.Join(":", labels)} {AllParameters(parameters)})
                    RETURN id(n) as id", parameters)
                .Single().Values["id"].As<long>();

        public long CreateRelationship(long fromId, long toId, string type, Dictionary<string, object> parameters = null)
            => session.Run($@"
                    MATCH (from), (to)
                    WHERE id(from) = {fromId} AND id(to) = {toId}
                    MERGE (from)-[r:{type} {AllParameters(parameters)}]->(to)
                    RETURN id(r) as id", parameters)
                .Single().Values["id"].As<long>();

        string AllParameters(Dictionary<string, object> parameters)
        {
            if (parameters.Count == 0)
            {
                return "";
            }

            var builder = new StringBuilder("{ ");
            foreach(var param in parameters.Keys)
            {
                builder.Append(param);
                builder.Append(": {");
                builder.Append(param);
                builder.Append("}");
            }
            builder.Append(" }");

            return builder.ToString();
        }

        public void Dispose()
        {
            session.Dispose();
            driver.Dispose();
        }
    }
}
