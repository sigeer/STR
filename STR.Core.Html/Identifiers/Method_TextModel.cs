using AngleSharp.Dom;
using STR.Core.Exceptions;
using STR.Core.Identifiers;

namespace STR.Core.Html.Identifiers
{
    public class Method_TextModel : FunctionIdentityModel
    {
        public Method_TextModel(string identifierName) : base(identifierName)
        {
        }

        public override Container Evaluate(Container element)
        {
            if (element is ElementContainer el)
                return new StringContainer(el.Value.Text());

            throw new STRException("Method [Text] only support Element Select input");
        }
    }
}
