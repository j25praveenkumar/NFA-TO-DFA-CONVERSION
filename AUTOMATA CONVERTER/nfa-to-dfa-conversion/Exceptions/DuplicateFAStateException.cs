using System;

namespace nfa_to_dfa_conversion.Exceptions
{
    class DuplicateFAStateException : Exception
    {
        private const string _message_ = "This State already declared before";
        public DuplicateFAStateException() : base(_message_)
        {
        }

        public DuplicateFAStateException(Exception innerException) : base(_message_, innerException)
        {
        }
    }
}
