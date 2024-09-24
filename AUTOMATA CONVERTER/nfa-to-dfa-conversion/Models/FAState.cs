namespace nfa_to_dfa_conversion.Models
{
    public class FAState
    {
        private readonly bool _isInitialState;
        private readonly bool _isFinalState;
        private readonly string _stateName;

        /// <summary>
        /// Hidden constructor of finite automata state.
        /// </summary>
        private FAState()
        {

        }

        /// <summary>
        /// Initializes Finite Automata State 
        /// </summary>
        /// <param name="stateName">e.g: q0, q1, q2, a, b, c, 1, 2, 3, etc...</param>
        /// <param name="isInitialState">Will this state is initial/start state? Default value is ( False: No)</param>
        /// <param name="isFinalState">Will this state is final state? Default value is ( False: No)</param>
        public FAState(string stateName, bool isInitialState = false, bool isFinalState = false)
        {
            this._stateName = stateName;
            this._isInitialState = isInitialState;
            this._isFinalState = isFinalState;
        }

        public bool IsInitialState
        {
            get
            {
                return this._isInitialState;
            }
        }

        public bool IsFinalState
        {
            get
            {
                return this._isFinalState;
            }
        }
        public string StateName
        {
            get
            {
                return this._stateName;
            }
        }

        public override string ToString()
        {
            return this.StateName;
        }
    }
}
