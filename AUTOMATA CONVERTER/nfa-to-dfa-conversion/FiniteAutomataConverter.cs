using nfa_to_dfa_conversion.Exceptions;
using nfa_to_dfa_conversion.Extensions;
using nfa_to_dfa_conversion.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace nfa_to_dfa_conversion
{
    public class FiniteAutomataConverter
    {
        private const char STATE_SEPARATOR = '&';

        /// <summary>
        /// Converts NFA to DFA 
        /// </summary>
        /// <param name="input">NFA object</param>
        /// <returns>DFA object</returns>
        public FiniteAutomata ConvertNFAToDFA(FiniteAutomata input)
        {
            if (input is null)
            {
                throw new FAConverterException("Input automata cannot be NULL.");
            }

            // If the type that comes in the parameter is DFA, the same is returned as there is no need for conversion.
            if (input.AutomataType == FiniteAutomataType.DFA)
            {
                return input;
            }

            FiniteAutomata DFA = new FiniteAutomata(FiniteAutomataType.DFA, input.Alphabet.ToList());

            // The first case is added as is.
            DFA = InsertInitalState(input, DFA);

            // The conversion is done.
            DFA = Convert(input, DFA);

            return DFA;
        }
        /// <summary>
        ///  Inserts initial state to DFA from NFA.
        /// </summary>
        /// <param name="NFA"></param>
        /// <param name="DFA"></param>
        /// <returns>Updated DFA object.</returns>
        private FiniteAutomata InsertInitalState(FiniteAutomata NFA, FiniteAutomata DFA)
        {
            _ = DFA.AddState(NFA.InitialState);

            // Only the transitions made by the initial state are fetched from the NFA.
            IEnumerable<FATransition> transitions = NFA.Transitions.Where(x => x.FromState == NFA.InitialState);
            foreach (FATransition transition in transitions)
            {
                if (transition.ToState.Count() == 1)
                {
                    // It is created if the target state does not exist.
                    FAState toState = transition.ToState.First();
                    _ = DFA.AddState(toState);

                    // It is compatible with DFA as it is a 1-1 pass. The transition is added as is.
                    _ = DFA.AddTransition(transition);
                }
                else if (transition.ToState.Count() > 1) // 1 den fazla to state varsa
                {
                    IEnumerable<FAState> toStates = transition.ToState;
                    // Combine the states by separating them.
                    string newStateName = string.Join(STATE_SEPARATOR, toStates);
                    // If any state is a final state, this state must be final.
                    bool isFinalState = toStates.Any(state => state.IsFinalState);

                    // It is created if the target state does not exist.
                    FAState aState = new FAState(newStateName, isFinalState: isFinalState);
                    _ = DFA.AddState(aState);

                    _ = DFA.AddTransition(transition.TransitionSymbol, transition.FromState.StateName, aState.StateName);
                }
                else // state of being 0
                {

                }
            }

            return DFA;
        }
        /// <summary>
        /// Converts NFA transitions into the DFA transition, except initial state.
        /// </summary>
        /// <param name="NFA"></param>
        /// <param name="DFA"></param>
        /// <returns>Updated DFA object.</returns>
        private FiniteAutomata Convert(FiniteAutomata NFA, FiniteAutomata DFA)
        {
            Queue<FAState> statesQueue = new Queue<FAState>();

            // Finds all states added by Initial state and adds them to the queue.
            IEnumerable<FAState> states = DFA.States.Where(x => !x.IsInitialState);
            foreach (FAState state in states)
            {
                statesQueue.Enqueue(state);
            }

            // It works for all objects in the queue.
            do
            {
                // The next element is brought from the queue.
                FAState fromState = statesQueue.Dequeue();

                // Since it is DFA, all passes must be used.
                foreach (char symbol in DFA.Alphabet)
                {
                    string[] subStateNames = fromState.StateName.Split(STATE_SEPARATOR);

                    List<string> newStateNames = new List<string>();
                    bool isFinalState = false;

                    foreach (string subStateName in subStateNames)
                    {
                        FAState subState = NFA.States.First(x => x.StateName == subStateName);
                        FATransition subTransition = NFA.Transitions.FirstOrDefault(x => x.FromState == subState && x.TransitionSymbol == symbol);

                        IEnumerable<FAState> subToStates = subTransition.ToState;
                        newStateNames.AddNotExists(subToStates.Select(x => x.StateName));

                        // If any state is a final state, this state must be final.
                        if (!isFinalState)
                        {
                            isFinalState = subToStates.Any(state => state.IsFinalState);
                        }
                    }

                    // Combines states by separating them.
                    string newStateName = string.Join(STATE_SEPARATOR, newStateNames.OrderBy(x => x));

                    // It is created if the target state does not exist.
                    FAState targetState;
                    if (!DFA.States.Any(x => x.StateName == newStateName))
                    {
                        targetState = new FAState(newStateName, isFinalState: isFinalState);
                        _ = DFA.AddState(targetState);

                        // The non-existent item is also added to the queue.
                        statesQueue.Enqueue(targetState);
                    }
                    else
                    {
                        targetState = DFA.States.First(x => x.StateName == newStateName);
                    }

                    _ = DFA.AddTransition(symbol, fromState.StateName, targetState.StateName);
                }

            } while (statesQueue.Count > 0);

            return DFA;
        }
        /// <summary>
        /// Converts 2DFA to DFA 
        /// </summary>
        /// <param name="input">2DFA object</param>
        /// <returns>DFA object</returns>
        

        private FAState GetOptimalState(FAState sourceState, IEnumerable<FATransition> filterTransition, FiniteAutomata DFA)
        {
            FAState selectedState = null;
            IEnumerable<FAState> finalStateFilter = filterTransition.SelectMany(x => x.ToState.Where(y => y.IsFinalState));
            if (finalStateFilter.Count() == 1)
            {
                selectedState = finalStateFilter.First();
            }
            else
            {
                bool xLoop = filterTransition.All(x => x.FromState == sourceState
                                                       && x.ToState.Any(y => y.StateName == sourceState.StateName)
                                                       && x.FromState.IsFinalState);
                if (xLoop)
                {
                    string emptySName = "Empty";
                    FAState emptySet = new FAState(emptySName);
                    if (!DFA.States.Any(x => x.StateName == emptySet.StateName))
                    {
                        _ = DFA.AddState(emptySet);

                        foreach (char symbol in DFA.Alphabet)
                        {
                            _ = DFA.AddTransition(symbol, emptySName, emptySName);
                        }
                    }
                    selectedState = emptySet;
                    return selectedState;
                }

                IEnumerable<FAState> selfRefStates = filterTransition.Where(x => x.ToState.Any(y => y == sourceState && x.FromState == sourceState) && !x.Direction).Select(x => x.FromState);
                FAState selfRefSt = selfRefStates.FirstOrDefault();
                if (selfRefSt != null)
                {
                    FAState updState = new FAState(selfRefSt.StateName, selfRefSt.IsInitialState);
                    DFA.UpdateState(updState);
                    selectedState = updState;
                    return selectedState;
                }


                IEnumerable<FAState> selfReferenceFilter = filterTransition.SelectMany(x => x.ToState.Where(y => y != sourceState));
                if (selfReferenceFilter.Count() == 1)
                {
                    selectedState = selfReferenceFilter.First();
                }
                else
                {
                    selectedState = filterTransition.First().FromState;
                }
            }

            return selectedState;
        }
    }
}