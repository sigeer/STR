using AngleSharp.Dom;
using STR.Core.Identifiers;

namespace STR.Core.Html.Identifiers
{
    public class Method_HtmlModel : FunctionIdentityModel<ElementContainer, StringContainer>
    {
        public Method_HtmlModel(string identifierName) : base(identifierName)
        {
        }

        public override StringContainer Evaluate(ElementContainer element)
        {
            return new StringContainer(element.Value.Html());
        }
    }
}
