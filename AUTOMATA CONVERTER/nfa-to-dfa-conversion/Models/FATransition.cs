using nfa_to_dfa_conversion.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace nfa_to_dfa_conversion.Models
{
    public class FATransition : IComparable<FATransition>
    {
        private readonly FAState _fromState;
        private readonly IEnumerable<FAState> _toState;
        private readonly char _transitionSymbol;
        private  bool _direction;

        /// <summary>
        /// Initializes Finite Automata Transition Object
        /// </summary>
        /// <param name="transitionSymbol">Symobl from alphabet that shows which transition will be </param>
        /// <param name="fromState">Shows transition source state</param>
        /// <param name="toState">Shows transition destination state </param>
        public FATransition(char transitionSymbol, FAState fromState, FAState toState)
        {
            this._transitionSymbol = transitionSymbol;
            this._fromState = fromState;
            this._toState = new List<FAState>() { toState };
        }

        /// <summary>
        /// Initializes Finite Automata Transition Object
        /// </summary>
        /// <param name="transitionSymbol">Symbol from alphabet that shows which transition will be </param>
        /// <param name="fromState">Shows transition source state</param>
        /// <param name="toState">Shows transition destination states.A state will be chosen when runtime.(Multiple value allowed for Non-deterministic FA) </param>
        public FATransition(char transitionSymbol, FAState fromState, IEnumerable<FAState> toState)
        {
            this._transitionSymbol = transitionSymbol;
            this._fromState = fromState;
            this._toState = toState;
        }
        /// <summary>
        /// Initializes Finite Automata Transition Object
        /// </summary>
        /// <param name="transitionSymbol">Symbol from alphabet that shows which transition will be </param>
        /// <param name="fromState">Shows transition source state</param>
        /// <param name="toState">Shows transition destination states.A state will be chosen when runtime.(Multiple value allowed for Non-deterministic FA)</param>
        /// <param name="direction"> Automata direction 0= Left, 1= Right</param>
        public FATransition(char transitionSymbol, FAState fromState, IEnumerable<FAState> toState, bool direction)
        {
            this._transitionSymbol = transitionSymbol;
            this._fromState = fromState;
            this._toState = toState;
            this._direction = direction;
        }

        /// <summary>
        /// Returns the transition symbol.
        /// </summary>
        public char TransitionSymbol
        {
            get
            {
                return this._transitionSymbol;
            }
        }

        /// <summary>
        /// Returns the source state.
        /// </summary>
        public FAState FromState
        {
            get
            {
                return this._fromState;
            }
        }

        /// <summary>
        /// Returns the Transition Direction
        /// </summary>
        public bool Direction
        {
            get
            {
                return this._direction;
            }
            set
            {
                this._direction = value;
            }
        }

        /// <summary>
        /// Returns the destination states.
        /// </summary>
        public IEnumerable<FAState> ToState
        {
            get
            {
                return this._toState;
            }
        }

        /// <summary>
        /// Compares states with another state.
        /// </summary>
        /// <param name="other">Another finite automata transition, which will be compared with the transition.</param>
        /// <returns>If there are equal that will be return 0, otherwise wiil be return -1.</returns>
        public int CompareTo([AllowNull] FATransition other)
        {
            bool isFromStatesEqual = this.FromState.Compare(other.FromState);
            bool isToStatesEqual = this.ToState.Compare(other.ToState);
            bool isTransitionNamesEqual = this.TransitionSymbol == other.TransitionSymbol;
            bool isDirectionsEqual = this.Direction == other.Direction;
            // These states are equal if all their values ​​are the same, even if their reference/Pointer is different.
            if (isTransitionNamesEqual && isToStatesEqual && isFromStatesEqual && isDirectionsEqual)
            {
                return 0;
            }

            return -1;
        }

        /// <summary>
        ///  Checks object reference is equal with another one..
        /// </summary>
        /// <param name="obj">Another object, which will be compared with the object.</param>
        /// <returns>If there are equals, returns true.</returns>
        public override bool Equals(object obj)
        {
            // Compares this object with the object references that come as parameters.
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is null)
                return false;

            throw new NotImplementedException();
        }

        /// <summary>
        ///  Gets hashcode of transition object.
        /// </summary>
        /// <returns>hashcode of object.</returns>
        public override int GetHashCode()
        {
            string fromName = this.FromState?.StateName;

            // Combines all names into a single string, separated by commas.
            IEnumerable<string> toNameArray = this.ToState.Select(state => state.StateName);
            string toName = string.Join(",", toNameArray);

            // It combines all parameters with a greater than sign between them and takes the hash.
            string hashData = string.Join('>', this.TransitionSymbol, fromName, toName);
            int hashValue = hashData.GetHashCode();

            return hashValue;
        }


        public static bool operator ==(FATransition left, FATransition right)
        {
            if (left is null)
                return right is null;

            bool isFromStatesEqual = left.FromState.Compare(right.FromState);
            bool isToStatesEqual = left.ToState.Compare(right.ToState);
            bool isTransitionNamesEqual = left.TransitionSymbol == right.TransitionSymbol;
            bool isDirectionsEqual = left.Direction == right.Direction;

            return (isTransitionNamesEqual && isToStatesEqual && isFromStatesEqual && isDirectionsEqual);
        }

        public static bool operator !=(FATransition left, FATransition right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            string toStates = string.Join(",", this.ToState);
            return $"{this.FromState.StateName}, {this.TransitionSymbol} transition to {{{toStates}}}";
        }
    }
}
