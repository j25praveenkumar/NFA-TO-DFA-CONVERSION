using System;

namespace nfa_to_dfa_conversion.Exceptions
{
    public class FAConverterException : Exception
    {
        public FAConverterException(string message) : base(message)
        {
        }

        public FAConverterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FAConverterException()
        {
        }
    }
}
