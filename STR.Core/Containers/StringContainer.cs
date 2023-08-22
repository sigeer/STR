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
}
