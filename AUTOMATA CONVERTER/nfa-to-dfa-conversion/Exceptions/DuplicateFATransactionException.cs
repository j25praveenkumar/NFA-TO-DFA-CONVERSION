using System;

namespace nfa_to_dfa_conversion.Exceptions
{
    public class DuplicateFATransactionException : Exception
    {
        private const string _message_ = "This transaction already declared before";
        public DuplicateFATransactionException() : base(_message_)
        {

        }

        public DuplicateFATransactionException(Exception innerException) : base(_message_, innerException)
        {
        }
    }
}
