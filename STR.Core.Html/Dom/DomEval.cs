using Irony.Parsing;
using STR.Core.Contants;
using STR.Core.Identifiers;
using System.Collections.Generic;
using System.Linq;

namespace STR.Core.Html
{
    public class DomEval : NodeEvalBase
    {
        public DomEval(IdentifierResolver identifierResolver) : base(identifierResolver)
        {
        }

        public override IEnumerable<Container> ReadNode(ParseTreeNode node, IEnumerable<Container> elements)
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
                        var identity = _identifierResolver.GetIdentifierModel(item);

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
                var function = identity as FunctionIdentityModel;
                return final.Select(x => function.Evaluate(x));

            }
        }
    }
}
