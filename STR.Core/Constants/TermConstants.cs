namespace STR.Core.Contants
{
    public class TermConstants
    {
        public const string Select = "SELECT";
        public const string From = "FROM";
        public const string DataSource = "DataSource";
        public const string As = "AS";
        public const string Download = "DOWNLOAD";
        public const string Context = "CONTEXT";

        public const string PropertyList = "propertyList";
        public const string NestedSelect = "nestedSelect";
        public const string SelectStatement = "selectStatement";
        public const string Identifier = "identifier";
        public const string MethodCall = "methodCall";
        public const string IdentifierGroup = "IdentifierGroup";

        public static readonly string[] KeywordSource = new string[]
        {
            Select,
            From,
            DataSource,
            As,
            Download,
            Context
        };

    }
}
