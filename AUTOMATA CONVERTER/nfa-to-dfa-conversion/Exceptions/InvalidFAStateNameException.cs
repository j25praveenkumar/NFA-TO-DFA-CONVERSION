using System;

namespace nfa_to_dfa_conversion.Exceptions
{
    public class InvalidFAStateNameException : Exception
    {
        private const string _message_ = "Invalid state name";
        public InvalidFAStateNameException() : base(_message_)
        {
        }

        public InvalidFAStateNameException(Exception innerException) : base(_message_, innerException)
        {
        }
    }
}
