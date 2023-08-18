namespace STR.Core.Identifiers
{
    public class Method_ReplaceModel : FunctionIdentityModel<StringContainer, StringContainer>
    {

        public Method_ReplaceModel(string identifierName, params string[] @params) : base(identifierName, @params)
        {
        }

        public override StringContainer Evaluate(StringContainer identifierValue)
        {
            return new StringContainer(identifierValue.Value.Replace(Params[0], Params[1]));
        }
    }
}
