using AngleSharp.Dom;

namespace STR.Core.Html
{
    public class ElementContainer : Container
    {
        private IElement _value;

        public ElementContainer(IElement value)
        {
            _value = value;
        }

        public IElement Value
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
            return Value.InnerHtml;
        }
    }
}
