using System;

namespace nfa_to_dfa_conversion.Exceptions
{
    public class MultipleFAInitalStateException : Exception
    {
        private const string _message_ = "Multiple initial state declaration is not allowed";
        public MultipleFAInitalStateException() : base(_message_)
        {
        }

        public MultipleFAInitalStateException(Exception innerException) : base(_message_, innerException)
        {
        }
    }
}
