namespace STR.Core.Identifiers
{
    public class IdentifierModel : BaseIdentifierModel
    {
        public IdentifierModel(string identifierName) : base(identifierName)
        {
            CurrentType = IdentityType.Identity;
        }
    }
}
