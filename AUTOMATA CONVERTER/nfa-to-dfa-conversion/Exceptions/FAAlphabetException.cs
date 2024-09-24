using System;

namespace nfa_to_dfa_conversion.Exceptions
{
    public class FAAlphabetException : Exception
    {
        public FAAlphabetException(string message) : base(message)
        {
        }

        public FAAlphabetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FAAlphabetException()
        {
        }
    }
}
