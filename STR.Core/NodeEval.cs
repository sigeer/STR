using Irony.Parsing;
using STR.Core.Contants;
using STR.Core.Exceptions;
using STR.Core.Identifiers;
using System.Collections.Generic;

namespace STR.Core
{
    public class NodeEvalBase
    {
        protected readonly IdentifierResolver _identifierResolver;

        public NodeEvalBase(IdentifierResolver identifierResolver)
        {
            _identifierResolver = identifierResolver;
        }

        public string GetReadMethod(ParseTreeNode rootNode)
        {
            return rootNode.ChildNodes[0].ChildNodes[0].Term.Name;
        }

        public virtual IEnumerable<Container> ReadNode(ParseTreeNode node, IEnumerable<Container> rootObj)
        {
            return new List<EmptyContainer>();
        }

        public ParseTreeNode GetFirstNode(ParseTreeNode rootNode)
        {
            var nodePoint = rootNode;
            while (nodePoint.ChildNodes[3] != null)
            {
                if (nodePoint.Term.Name == TermConstants.NestedSelect)
                {
                    nodePoint = GetNestedNode(nodePoint);
                }
                else
                {
                    return nodePoint;
                }
            }
            return nodePoint;
        }

        public ParseTreeNode GetNestedNode(ParseTreeNode node)
        {
            if (node.ChildNodes[3] != null)
            {
                var aggreNode = node.ChildNodes[3].ChildNodes[0];
                if (aggreNode.Term.Name == TermConstants.NestedSelect)
                    return aggreNode.ChildNodes[1];
                else
                    return aggreNode;
            }

            return null;
        }
    }
}
