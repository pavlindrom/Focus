using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using System.Reflection;
using System.Linq;
using System.Collections;

namespace Gr.Pavlo.Focus
{
    class SyntaxNodeVisitor : CSharpSyntaxWalker
    {
        Database database;
        Stack<long> semantic = new Stack<long>();

        public SyntaxNodeVisitor(Database db, long projectId)
        {
            database = db;
        }

        public override void Visit(SyntaxNode node)
        {
            if (node.IsStructuredTrivia) return;

            long? semanticParent = null;
            if (semantic.Count > 0)
            {
                semanticParent = semantic.Peek();
            }

            var nodeId = DatabaseNodeId(node);

            if (semanticParent.HasValue)
            {
                database.Relationship(nodeId, semanticParent.Value, "PARENT");
            }

            semantic.Push(DatabaseNodeId(node));
            base.Visit(node);
            semantic.Pop();
        }

        long DatabaseNodeId(SyntaxNode node)
        {
            var type = node.GetType();

            var relationships = new List<Tuple<string, long>>();
            var parameters = new Dictionary<string, object>();
            foreach (var property in type.GetProperties())
            {
                if (property.DeclaringType == type)
                {
                    var propType = GetPropertyType(property.PropertyType);
                    switch(propType)
                    {
                        case PropertyType.Token:
                            var token = (SyntaxToken)property.GetValue(node);
                            if (!token.IsPartOfStructuredTrivia())
                            {
                                var tokenParameters = new Dictionary<string, object>
                                {
                                    { "Kind", token.Kind() },
                                    { "IsKeyword", token.IsKeyword() },
                                    { "Text", token.Text }
                                };
                                relationships.Add(Tuple.Create(property.Name, database.Node("SyntaxToken", tokenParameters)));
                            }
                            break;
                        case PropertyType.Node:
                            var value = property.GetValue(node) as SyntaxNode;
                            if (value != null)
                            {
                                relationships.Add(Tuple.Create(property.Name, DatabaseNodeId(value)));
                            }
                            break;
                        case PropertyType.NodeList:
                            relationships.AddRange((property.GetValue(node) as IEnumerable)
                                .OfType<SyntaxNode>()
                                .Select(n => Tuple.Create(property.Name, DatabaseNodeId(n))));
                            break;
                        case PropertyType.Parameter:
                            parameters.Add(property.Name, property.GetValue(node));
                            break;
                    }
                }
            }

            var nodeId = database.Node(node.GetType().Name, parameters);
            foreach(var relationship in relationships)
            {
                database.Relationship(nodeId, relationship.Item2, relationship.Item1);
            }

            return nodeId;
        }

        PropertyType GetPropertyType(Type property)
        {
            if (typeof(SyntaxToken) == property || typeof(SyntaxToken).IsAssignableFrom(property))
            {
                return PropertyType.Token;
            }
            else if (typeof(SyntaxNode) == property || typeof(SyntaxNode).IsAssignableFrom(property))
            {
                if (typeof(IStructuredTriviaSyntax).IsAssignableFrom(property))
                {
                    return PropertyType.NobodyCares;
                }

                return PropertyType.Node;
            }
            else
            {
                var enumerableType = property.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    ?.GenericTypeArguments.FirstOrDefault();

                if (enumerableType == null)
                {
                    return PropertyType.Parameter;
                }
                else if (GetPropertyType(enumerableType) == PropertyType.Node)
                {
                    return PropertyType.NodeList;
                }
                else
                {
                    return PropertyType.NobodyCares;
                }
            }
        }

        enum PropertyType
        {
            Node,
            Token,
            NodeList,
            Parameter,
            NobodyCares
        }

        class RelationshipInfo
        {
            public long Id { get; set; }

            public string Name { get; set; }
        }
    }
}
