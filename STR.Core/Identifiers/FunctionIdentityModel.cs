using System;

namespace STR.Core.Identifiers
{
    public abstract class FunctionIdentityModel : BaseIdentifierModel
    {
        public FunctionIdentityModel(string identifierName, params string[] @params) : base(identifierName)
        {
            CurrentType = IdentityType.Method;
            Params = @params;
        }
        public abstract Container Evaluate(Container identifierValue);
        public string[] Params { get; set; }
    }
}
