using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gr.Pavlo.Focus
{
    internal class Database : IDisposable
    {
        IDriver driver;
        ISession session;

        public Database(string path, string username, string password)
        {
            driver = GraphDatabase.Driver(path, AuthTokens.Basic(username, password));
            session = driver.Session();
        }

        public long Node(string label, Dictionary<string, object> parameters = null)
            => session.Run($"MERGE (n:{label} {AllParameters(parameters)}) RETURN id(n)", parameters).Single().As<long>();

        public long Relationship(long fromId, long toId, string type, Dictionary<string, object> parameters = null)
            => session.Run($@"
MATCH (from), (to)
WHERE id(from) = {fromId} AND id(to) = {toId}
MERGE (from)-[r:{type} {AllParameters(parameters)}]->(to)
RETURN id(r)", parameters).Single().As<long>();

        string AllParameters(Dictionary<string, object> parameters)
            => parameters == null || parameters.Count == 0 ? "" : $"{{ {string.Join(", ", parameters.Keys.Select(p => $"{p}: {{{p}}}"))} }}";

        public void Dispose()
        {
            session.Dispose();
            driver.Dispose();
        }
    }
}
