using Irony.Parsing;
using STR.Core.Contants;
using STR.Core.Exceptions;

namespace STR.Core.Identifiers
{
    public class JsonIdetifierResolver : IdentifierResolver
    {
        public override BaseIdentifierModel GetIdentifierModel(ParseTreeNode node)
        {
            if (node.Term.Name == TermConstants.Identifier)
                return new IdentifierModel(node.Token.ValueString);
            else if (node.Term.Name == TermConstants.MethodCall)
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
            else
                throw new STRMethodNotSupportedException("not supported");
        }
    }
}
