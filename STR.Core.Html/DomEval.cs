using Irony.Parsing;
using STR.Core.Contants;
using STR.Core.Exceptions;
using STR.Core.Html.Identifiers;
using STR.Core.Identifiers;
using System.Collections.Generic;
using System.Linq;

namespace STR.Core.Html
{
    public class DomEval : NodeEvalBase
    {
        public IEnumerable<Container> ReadNode(ParseTreeNode node, IEnumerable<Container> elements)
        {
            // 获取查询对象
            var nestedNode = GetNestedNode(node);
            if (nestedNode != null)
            {
                if (nestedNode.Term.Name != TermConstants.DataSource)
                    elements = ReadNode(nestedNode, elements);
            }

            if (node.Term.Name == TermConstants.SelectStatement)
            {
                var subNode = GetNestedNode(node);
                if (subNode != null)
                {
                    var propertyNode = node.ChildNodes[1].ChildNodes[0];
                    List<Container> list = new List<Container>();
                    foreach (var item in propertyNode.ChildNodes)
                    {
                        var identity = GetIdentifierModel(item);

                        list.AddRange(SetModelFromNode(identity, elements));
                    }
                    return list;
                }
            }

            return elements;
        }

        private IEnumerable<Container> SetModelFromNode(BaseIdentifierModel identity, IEnumerable<Container> elements)
        {
            if (identity.CurrentType == IdentityType.Identity)
                return elements.OfType<ElementContainer>().SelectMany(x =>
                    x.Value.QuerySelectorAll(identity.IdentifierName)
                    .Select(y => new ElementContainer(y))
                );
            else
            {

                IEnumerable<Container> final;
                if (identity.Child != null)
                {
                    final = SetModelFromNode(identity.Child, elements);
                }
                else
                {

                    final = elements.OfType<ElementContainer>().SelectMany(x => x.Value.QuerySelectorAll(identity.IdentifierName)).Select(x => new ElementContainer(x));

                }
                if (identity.IdentifierName != MethodConstants.Replace)
                {
                    var function = identity as FunctionIdentityModel<ElementContainer, StringContainer>;
                    return final.Select(x => function.Evaluate(x as ElementContainer));
                }
                else
                {
                    var function = identity as FunctionIdentityModel<StringContainer, StringContainer>;
                    return final.Select(x => function.Evaluate(x as StringContainer));
                }

            }
        }

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
