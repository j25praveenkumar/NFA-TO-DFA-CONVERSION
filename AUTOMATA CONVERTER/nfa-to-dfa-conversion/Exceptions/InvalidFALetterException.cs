using System;

namespace nfa_to_dfa_conversion.Exceptions
{
    public class InvalidFALetterException : Exception
    {
        private const string _message_ = "Transition, invalid letter input";
        public InvalidFALetterException() : base(_message_)
        {
        }

        public InvalidFALetterException(Exception innerException) : base(_message_, innerException)
        {
        }
    }
}
