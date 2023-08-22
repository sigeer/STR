using AngleSharp.Dom;
using STR.Core.Exceptions;
using STR.Core.Identifiers;

namespace STR.Core.Html.Identifiers
{
    public class Method_HtmlModel : FunctionIdentityModel
    {
        public Method_HtmlModel(string identifierName) : base(identifierName)
        {
        }

        public override Container Evaluate(Container element)
        {
            if (element is ElementContainer el)
                return new StringContainer(el.Value.Html());

            throw new STRException("Method [Html] only support Element Select input");
        }
    }
}
