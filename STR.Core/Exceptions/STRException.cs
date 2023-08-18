using System;

namespace STR.Core.Exceptions
{
    public class STRException : Exception
    {
        public STRException(string message) : base(message)
        {
        }
    }

    public class STRMethodNotSupportedException : STRException
    {
        public STRMethodNotSupportedException(string message) : base(message)
        {

        }
    }

    public class STRIdentifierNotFoundException : STRException
    {
        public STRIdentifierNotFoundException(string message) : base(message)
        {

        }
    }
}
