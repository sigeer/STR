using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace STR.Core
{
    public class StringContainer : Container
    {
        private string _value;

        public StringContainer(string value)
        {
            _value = value;
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public class JsonContainer : Container
    {
        private JToken _value;

        public JsonContainer(JToken value)
        {
            _value = value;
        }

        public JsonContainer(string value)
        {
            _value = JToken.Parse(value);
        }

        public JToken Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(Value);
        }
    }
}
