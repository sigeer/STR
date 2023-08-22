using Irony.Parsing;
using STR.Core.Contants;
using STR.Core.Exceptions;
using STR.Core.Identifiers;

namespace STR.Core.Html.Identifiers
{
    public class HtmlIdentifierResolver : IdentifierResolver
    {
        public override BaseIdentifierModel GetIdentifierModel(ParseTreeNode node)
        {
            if (node.Term.Name == TermConstants.MethodCall)
            {
                var methodName = node.ChildNodes[0].Term.Name;

                if (methodName == MethodConstants.Replace)
                    return new Method_ReplaceModel(methodName,
                        node.ChildNodes[1].ChildNodes[3].Token.ValueString,
                        node.ChildNodes[1].ChildNodes[5].Token.ValueString)
                    {
                        Child = GetIdentifierModel(node.ChildNodes[1].ChildNodes[1].ChildNodes[0])
                    };

                if (methodName == MethodConstants.Html)
                    return new Method_HtmlModel(methodName)
                    {
                        Child = GetIdentifierModel(node.ChildNodes[1].ChildNodes[1])
                    };

                if (methodName == MethodConstants.Text)
                    return new Method_TextModel(methodName)
                    {
                        Child = GetIdentifierModel(node.ChildNodes[1].ChildNodes[1])
                    };

                if (methodName == MethodConstants.GetAttr)
                    return new Method_GetAttrModel(methodName,
                        node.ChildNodes[1].ChildNodes[3].Token.ValueString)
                    {
                        Child = GetIdentifierModel(node.ChildNodes[1].ChildNodes[1])
                    };

                throw new STRMethodNotSupportedException("not supported");
            }
            else if (node.Term.Name == TermConstants.Identifier)
                return new IdentifierModel(node.Token.ValueString);
            else if (node.Term.Flags == TermFlags.IsLiteral)
                return new IdentifierModel(node.Token.ValueString);
            else
                throw new STRMethodNotSupportedException("not supported");
        }
    }
}
