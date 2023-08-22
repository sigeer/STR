using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace STR.Core.Identifiers
{
    public abstract class IdentifierResolver
    {
        public abstract BaseIdentifierModel GetIdentifierModel(ParseTreeNode node);
    }
}
