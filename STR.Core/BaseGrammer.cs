using Irony.Parsing;
using STR.Core.Contants;

namespace STR.Core
{
    public abstract class BaseGrammer : Grammar
    {
        public BaseGrammer() : base(caseSensitive: false)
        {
            var select = ToTerm(TermConstants.Select);
            var download = ToTerm(TermConstants.Download);
            var from = ToTerm(TermConstants.From);
            var identifier = new IdentifierTerminal(TermConstants.Identifier);
            var identifierGroup = new NonTerminal(TermConstants.IdentifierGroup);
            var source = ToTerm(TermConstants.DataSource);

            var function0 = SetHtmlFunction();
            var function3 = SetTextFunction();
            var function1 = SetGetAttrFunction();
            var function2 = SetReplaceFunction(identifierGroup);

            // 定义语法规则
            var selectStatement = new NonTerminal(TermConstants.SelectStatement);
            var propertyList = new NonTerminal(TermConstants.PropertyList);
            var nestedSelect = new NonTerminal(TermConstants.NestedSelect);

            identifierGroup.Rule = identifier | function0 | function1 | function2 | function3 | identifierGroup;

            propertyList.Rule = MakePlusRule(propertyList, ToTerm(","), identifierGroup);
            nestedSelect.Rule = ToTerm("(") + selectStatement + ToTerm(")");

            selectStatement.Rule = (select | download) + propertyList + from + (source | nestedSelect);
            // 设置根规则
            Root = selectStatement;

            // 设置语法注释符号
            CommentTerminal comment = new CommentTerminal("comment", "//", "\n", "\r\n");
            NonGrammarTerminals.Add(comment);
        }


        protected NonTerminal SetHtmlFunction()
        {
            var methodName = ToTerm(MethodConstants.Html);

            var methodCall = new NonTerminal(TermConstants.MethodCall);
            var methodArguments = new NonTerminal("methodArguments");
            var literal = new StringLiteral("literal", "'", StringOptions.AllowsAllEscapes);

            methodCall.Rule = methodName + methodArguments;

            methodArguments.Rule = ToTerm("(") + literal + ToTerm(")"); // 参数形式为 (expression)
            return methodCall;
        }

        protected NonTerminal SetTextFunction()
        {
            var methodName = ToTerm(MethodConstants.Text);

            var methodCall = new NonTerminal(TermConstants.MethodCall);
            var methodArguments = new NonTerminal("methodArguments");
            var literal = new StringLiteral("literal", "'", StringOptions.AllowsAllEscapes);

            methodCall.Rule = methodName + methodArguments;

            methodArguments.Rule = ToTerm("(") + literal + ToTerm(")"); // 参数形式为 (expression)
            return methodCall;
        }

        protected NonTerminal SetGetAttrFunction()
        {
            var methodName = ToTerm(MethodConstants.GetAttr);

            var methodCall = new NonTerminal(TermConstants.MethodCall);
            var methodArguments = new NonTerminal("methodArguments");
            var literal = new StringLiteral("literal", "'", StringOptions.AllowsAllEscapes);

            methodCall.Rule = methodName + methodArguments;

            methodArguments.Rule = ToTerm("(") + literal + ToTerm(",") + literal + ToTerm(")"); // 参数形式为 (expression, literal)
            return methodCall;
        }

        protected NonTerminal SetReplaceFunction(NonTerminal identifier)
        {
            var replace = ToTerm(MethodConstants.Replace);

            var methodCall = new NonTerminal(TermConstants.MethodCall);
            var methodArguments = new NonTerminal("methodArguments");
            var literal = new StringLiteral("literal", "'", StringOptions.AllowsAllEscapes);

            methodCall.Rule = replace + methodArguments;

            methodArguments.Rule = ToTerm("(") + identifier + ToTerm(",") + literal + ToTerm(",") + literal + ToTerm(")"); // 参数形式为 (expression, literal, literal)
            return methodCall;
        }
    }

}
