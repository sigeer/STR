using Irony.Parsing;
using STR.Core.Contants;
using STR.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace STR.Core
{
    public abstract class STRReader
    {
        protected string _content;

        public STRReader(string content)
        {
            _content = content;
        }

        protected abstract ParseTreeNode GetRootNode(string command);
        public abstract IEnumerable<Container> RunCommand(string command);
    }
    public class STRJsonReader : STRReader
    {
        public STRJsonReader(string content) : base(content)
        {
        }

        protected Lazy<Parser> _jsonParser = new Lazy<Parser>(() => new Parser(new LanguageData(new JsonQueryGrammar())));

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

            var eval = new JsonEval();
            var jsonObject = eval.ReadNode(root, new JsonContainer(_content));
            if (eval.GetReadMethod(root) == TermConstants.Select)
                return new List<Container>() { jsonObject };
            return null;
        }
    }
}
