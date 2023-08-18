using STR.Core.Identifiers;

namespace STR.Core.Html.Identifiers
{
    public class Method_GetAttrModel : FunctionIdentityModel<ElementContainer, StringContainer>
    {
        public Method_GetAttrModel(string identifierName, string attrName) : base(identifierName, attrName)
        {
        }

        public override StringContainer Evaluate(ElementContainer element)
        {
            return new StringContainer(element.Value.Attributes[Params[0]].Value);
        }
    }
}
