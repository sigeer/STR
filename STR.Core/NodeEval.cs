using Irony.Parsing;
using STR.Core.Contants;
using STR.Core.Exceptions;
using STR.Core.Identifiers;

namespace STR.Core
{
    public class NodeEvalBase
    {
        public string GetReadMethod(ParseTreeNode rootNode)
        {
            return rootNode.ChildNodes[0].ChildNodes[0].Term.Name;
        }


        public virtual BaseIdentifierModel GetIdentifierModel(ParseTreeNode node)
        {
            if (node.Term.Name == TermConstants.MethodCall)
            {
                var methodName = node.ChildNodes[0].Term.Name;
                // ------------------  methodArguments / identifier / identifierGroup
                // var identityUnit = node.ChildNodes[1].ChildNodes[1].ChildNodes[0];

                if (methodName == MethodConstants.Replace)
                    return new Method_ReplaceModel(
                        methodName,
                        node.ChildNodes[1].ChildNodes[3].Token.ValueString,
                        node.ChildNodes[1].ChildNodes[5].Token.ValueString)
                    {
                        Child = GetIdentifierModel(node.ChildNodes[1].ChildNodes[1].ChildNodes[0])
                    };

                throw new STRMethodNotSupportedException("not supported");
            }
            else if (node.Term.Name == TermConstants.Identifier)
                return new IdentifierModel(node.Token.ValueString);
            else
                throw new STRMethodNotSupportedException("not supported");
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
