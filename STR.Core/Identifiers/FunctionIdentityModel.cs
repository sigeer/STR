using System;

namespace STR.Core.Identifiers
{
    public abstract class FunctionIdentityModel<TIdentityValueType, TReturnType> : BaseIdentifierModel where TIdentityValueType : Container where TReturnType : Container
    {
        public FunctionIdentityModel(string identifierName, params string[] @params) : base(identifierName)
        {
            CurrentType = IdentityType.Method;
            Params = @params;
            InputType = typeof(TIdentityValueType);
            OutputType = typeof(TReturnType);
        }
        public abstract TReturnType Evaluate(TIdentityValueType identifierValue);
        public string[] Params { get; set; }
        public Type InputType { get; set; }
        public Type OutputType { get; set; }
    }
}
