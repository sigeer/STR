using AngleSharp.Dom;
using STR.Core.Identifiers;

namespace STR.Core.Html.Identifiers
{
    public class Method_TextModel : FunctionIdentityModel<ElementContainer, StringContainer>
    {
        public Method_TextModel(string identifierName) : base(identifierName)
        {
        }

        public override StringContainer Evaluate(ElementContainer element)
        {
            return new StringContainer(element.Value.Text());
        }
    }
}
