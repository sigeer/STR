namespace STR.Core.Identifiers
{
    public enum IdentityType
    {
        Identity,
        Method
    }

    public abstract class BaseIdentifierModel
    {
        public BaseIdentifierModel(string identifierName)
        {
            CurrentType = IdentityType.Identity;
            IdentifierName = identifierName;
        }

        public IdentityType CurrentType { get; set; }
        public string IdentifierName { get; set; }
        public BaseIdentifierModel Child { get; set; }
        public string GetFirstIdentifierName()
        {
            return Child == null ? IdentifierName : Child.GetFirstIdentifierName();
        }
    }
}
