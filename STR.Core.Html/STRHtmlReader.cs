using AngleSharp.Html.Parser;
using Irony.Parsing;
using STR.Core.Contants;
using STR.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace STR.Core.Html
{
    public class STRHtmlReader : STRReader
    {
        public STRHtmlReader(string content) : base(content)
        {
        }

        protected Lazy<Parser> _jsonParser = new Lazy<Parser>(() => new Parser(new LanguageData(new HtmlGrammar())));

        protected override ParseTreeNode GetRootNode(string command)
        {
            var parseTree = _jsonParser.Value.Parse(command);
            if (parseTree.HasErrors())
            {
                foreach (var message in parseTree.ParserMessages)
                {
                    throw new STRException(message.ToString());
                }
            }
            return parseTree.Root;
        }

        public override IEnumerable<Container> RunCommand(string command)
        {
            var root = GetRootNode(command);

            var eval = new DomEval();

            var htmlParser = new HtmlParser();
            var document = htmlParser.ParseDocument(_content);
            var jsonObject = eval.ReadNode(root, document.Children.Select(x => new ElementContainer(x)));
            if (eval.GetReadMethod(root) == TermConstants.Select)
                return jsonObject.OfType<StringContainer>();
            return null;
        }
    }
}
