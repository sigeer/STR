using STR.Core;
using STR.Core.Html;

namespace STR.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("""select replace(name, 'J', 'K') from (select son from datasource)""", ExpectedResult = """["Kohn","Kohn1"]""")]
        [Test]
        public string JsonTest1(string queryCommand)
        {
            var inputString = """[{'father': {}, 'son': {'name': 'John', 'age': 25}}, {'father': {}, 'son': {'name': 'John1', 'age': 256}}]""";

            var core = new STRJsonReader(inputString);
            var data = core.RunCommand(queryCommand);
            return data.First().ToString();
        }

        [Test]
        public void HtmlTest()
        {
            string queryCommand = "select replace(replace(text('article.post-item:nth-child(2)'), '\\n', ''), ' ', '') from datasource";
            var html = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sample.html"));

            var core = new STRHtmlReader(html);
            var data = core.RunCommand(queryCommand);
            var final = string.Join("", data.Select(x => x.ToString()));
            Console.Write(final);
            Assert.That(!string.IsNullOrEmpty(final));
        }
    }
}