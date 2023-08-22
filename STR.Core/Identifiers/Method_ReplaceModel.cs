using STR.Core.Exceptions;

namespace STR.Core.Identifiers
{
    public class Method_ReplaceModel : FunctionIdentityModel
    {

        public Method_ReplaceModel(string identifierName, params string[] @params) : base(identifierName, @params)
        {
        }

        public override Container Evaluate(Container identifierValue)
        {
            if (identifierValue is StringContainer str)
                return new StringContainer(str.Value.Replace(Params[0], Params[1]));

            throw new STRException("Replace Method only support string input");
        }
    }
}
