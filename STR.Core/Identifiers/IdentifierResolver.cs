using Irony.Parsing;

namespace STR.Core.Identifiers
{
    public abstract class IdentifierResolver
    {
        public abstract BaseIdentifierModel GetIdentifierModel(ParseTreeNode node);
    }
}
