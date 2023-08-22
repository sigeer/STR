using STR.Core.Exceptions;
using STR.Core.Identifiers;

namespace STR.Core.Html.Identifiers
{
    public class Method_GetAttrModel : FunctionIdentityModel
    {
        public Method_GetAttrModel(string identifierName, string attrName) : base(identifierName, attrName)
        {
        }

        public override Container Evaluate(Container element)
        {
            if (element is ElementContainer el)
                return new StringContainer(el.Value.Attributes[Params[0]].Value);

            throw new STRException("Method [GetAttr] only support Element Select input");
        }
    }
}
