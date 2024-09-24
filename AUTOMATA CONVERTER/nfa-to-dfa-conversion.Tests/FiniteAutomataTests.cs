using nfa_to_dfa_conversion.Exceptions;
using nfa_to_dfa_conversion.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace nfa_to_dfa_conversion.Tests
{
    public class FiniteAutomataTests

    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Should_Pass_When_FiniteAutomata_AlphabetIsNotNull()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);
            Assert.Pass();
        }
        [Test]
        public void Should_ThrowsException_When_FiniteAutomata_AlphabetIsNull()
        {
            Assert.Throws<FAAlphabetException>(delegate
            {
                FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, null);
            });
        }
        [Test]
        public void Should_ThrowsException_When_FiniteAutomata_AlphabetIsEmpty()
        {
            Assert.Throws<FAAlphabetException>(delegate
            {
                List<char> alphabet = new List<char>();
                FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);
            });
        }
        [Test]
        public void Should_ReturnTrue_When_AddState_WithoutParamter()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            Assert.IsTrue(automata.AddState());
        }
        [Test]
        public void Should_ReturnTrue_When_AddState_WithOnlyPassIsInitialStateParameter()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            Assert.IsTrue(automata.AddState(isInitialState: true));
        }
        [Test]
        public void Should_ReturnTrue_When_AddState_WithOnlyPassIsFinalStateParameter()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            Assert.IsTrue(automata.AddState(isFinalState: true));
        }
        [Test]
        public void Should_ReturnFalse_When_AddState_MultipleAddInitialState()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            _ = automata.AddState(isInitialState: true);
            Assert.False(automata.AddState(isInitialState: true));
        }
        [Test]
        public void Should_ReturnTrue_When_AddState_MultipleAddFinalState()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            _ = automata.AddState(isFinalState: true);
            Assert.True(automata.AddState(isFinalState: true));
        }
        [Test]
        public void Should_ReturnTrue_When_AddState_WithFAState()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);
            FAState state = new FAState("State");
            Assert.True(automata.AddState(state));
        }
        [Test]
        public void Should_ReturnFalse_When_AddState_WithNullFaState()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);
            FAState state = null;
            Assert.False(automata.AddState(state));
        }
        [Test]
        public void Should_ReturnTrue_When_AddState_WithStateName()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);
            Assert.True(automata.AddState("StateName"));
        }
        [Test]
        public void Should_ReturnTrue_When_AddState_PassInitialStateTrueAndFinalStateTrue()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);
            Assert.True(automata.AddState(isInitialState: true, isFinalState: true));
        }
        [Test]
        public void Should_ReturnTrue_When_AddState_PassStateNameAndInitialStateAndFinalState()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);
            Assert.True(automata.AddState("State Name", isInitialState: true, isFinalState: true));
        }
        [Test]
        public void Should_ReturnTrue_When_UpdateState_WithFAState()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.TwoWayDFA, alphabet);
            FAState state = new FAState("State Name", isInitialState: true, isFinalState: true);
            automata.AddState(state);
            state = new FAState("State Name", isInitialState: true, isFinalState: false);
            Assert.True(automata.UpdateState(state));
        }
        [Test]
        public void Should_ReturnFalse_When_AddTransition_WithInvalidSymbol()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);
            _ = automata.AddState("q1", isInitialState: true);
            _ = automata.AddState("q2", isFinalState: true);

            Assert.False(automata.AddTransition('b', "q1", "q2"));
        }
        [Test]
        public void Should_ReturnTrue_When_AddTransition_WithValidSymbol()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);
            _ = automata.AddState("q1", isInitialState: true);
            _ = automata.AddState("q2", isFinalState: true);

            Assert.True(automata.AddTransition('a', "q1", "q2"));
        }
        [Test]
        public void Should_ReturnFalse_When_AddTransition_WithInvalidFromStateName()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);
            _ = automata.AddState("q1", isInitialState: true);
            _ = automata.AddState("q2", isFinalState: true);

            Assert.False(automata.AddTransition('a', "qqqq", "q2"));
        }
        [Test]
        public void Should_ReturnFalse_When_AddTransition_WithInvalidToStateName()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);
            _ = automata.AddState("q1", isInitialState: true);
            _ = automata.AddState("q2", isFinalState: true);

            Assert.False(automata.AddTransition('a', "q1", "qqqq"));
        }
        [Test]
        public void Should_ReturnFalse_When_AddTransition_WithNullFATransition()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            FATransition transition = null;
            Assert.False(automata.AddTransition(transition));
        }
        [Test]
        public void Should_ReturnTrue_When_AddTransition_WithFATranstion()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            FAState state1 = new FAState("q1", isInitialState: true, isFinalState: true);
            FATransition transition = new FATransition('a', state1, state1);

            Assert.True(automata.AddTransition(transition));
        }
        [Test]
        public void Should_ReturnFalse_When_AddTransition_MultipleToStatesInDFA()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            FAState state1 = new FAState("q1", isInitialState: true, isFinalState: true);
            FAState state2 = new FAState("q2");

            List<FAState> states = new List<FAState>() { state1, state2 };

            FATransition transition = new FATransition('a', state1, states);

            Assert.False(automata.AddTransition(transition));
        }
        [Test]
        public void Should_ReturnTrue_When_AddTransition_MultipleToStatesInNFA()
        {
            List<char> alphabet = new List<char>() { 'a' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.NFA, alphabet);

            FAState state1 = new FAState("q1", isInitialState: true, isFinalState: true);
            FAState state2 = new FAState("q2");

            List<FAState> states = new List<FAState>() { state1, state2 };

            FATransition transition = new FATransition('a', state1, states);

            Assert.True(automata.AddTransition(transition));
        }
        [Test]
        public void Should_ReturnTrue_When_IsValid_AutomataIsValidDFA()
        {
            List<char> alphabet = new List<char> { 'a', 'b', 'c' };
            FiniteAutomata dfaTest = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            _ = dfaTest.AddState(true); // q0
            _ = dfaTest.AddState(); // q1
            _ = dfaTest.AddState(isFinalState: true); // q2

            _ = dfaTest.AddTransition('a', "q0", "q1"); // q0 goes to q1 with transition 'a'.
            _ = dfaTest.AddTransition('b', "q0", "q2"); // q0 goes to q2 with transition 'b'.
            _ = dfaTest.AddTransition('c', "q0", "q0"); // q0 goes to q0 with transition 'c'.

            _ = dfaTest.AddTransition('a', "q1", "q0"); // q1 goes to q0 with 'a' transition.
            _ = dfaTest.AddTransition('b', "q1", "q1"); // q1 goes to q1 with 'b' transition.
            _ = dfaTest.AddTransition('c', "q1", "q2"); // q1 goes to q2 with 'c' transition.

            _ = dfaTest.AddTransition('a', "q2", "q0"); // q2 goes to q0 with 'a' transition.
            _ = dfaTest.AddTransition('b', "q2", "q2"); // q2 goes to q2 with 'b' transition.
            _ = dfaTest.AddTransition('c', "q2", "q2"); // q2 goes to q2 with 'c' transition.

            Assert.IsTrue(dfaTest.IsValid);
        }
        [Test]
        public void Should_ReturnFalse_When_IsValid_AutomataIsInvalidDFA()
        {
            List<char> alphabet = new List<char> { 'a', 'b', 'c' };
            FiniteAutomata dfaTest = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            _ = dfaTest.AddState(true); // q0
            _ = dfaTest.AddState(); // q1
            _ = dfaTest.AddState(isFinalState: true); // q2

            _ = dfaTest.AddTransition('b', "q0", "q2"); // q0 goes to q2 with 'b' transition.
            _ = dfaTest.AddTransition('c', "q0", "q0"); // q0 goes to q0 with 'c' transition.
            _ = dfaTest.AddTransition('c', "q0", "q1"); // q0 goes to q1 with 'c' transition.

            _ = dfaTest.AddTransition('a', "q1", "q0"); // q1 goes to q0 with 'a' transition.
            _ = dfaTest.AddTransition('b', "q1", "q1"); // q1 goes to q1 with 'b' transition.
            _ = dfaTest.AddTransition('c', "q1", "q2"); // q1 goes to q2 with 'c' transition.

            _ = dfaTest.AddTransition('a', "q2", "q0"); // q2 goes to q0 with 'a' transition.
            _ = dfaTest.AddTransition('b', "q2", "q2"); // q2 goes to q2 with 'b' transition.
            _ = dfaTest.AddTransition('c', "q2", "q2"); // q2 goes to q2 with 'c' transition.

            Assert.IsFalse(dfaTest.IsValid);
        }
        [Test]
        public void Should_ReturnTrue_When_IsValid_AutomataIsValidNFA()
        {
            List<char> alphabet = new List<char> { '0', '1' };
            FiniteAutomata nfaTest = new FiniteAutomata(FiniteAutomataType.NFA, alphabet);

            _ = nfaTest.AddState("A", isInitialState: true); //A
            _ = nfaTest.AddState("B");                       //B
            _ = nfaTest.AddState("C", isFinalState: true);   //C

            _ = nfaTest.AddTransition('0', "A", "A");     // A goes to A with a '0' transition.
            _ = nfaTest.AddTransition('1', "A", "B,C");   // A goes to B or C with a '1' transition.

            _ = nfaTest.AddTransition('0', "B", "A");     // B goes to B with a '0' transition.
            _ = nfaTest.AddTransition('1', "B", "A,C");   // B goes to A or C with a '1' transition.

            _ = nfaTest.AddTransition('0', "C", "A,B");   // C goes to A or B with a '0' transition.
            _ = nfaTest.AddTransition('1', "C", "C");     // C goes to C with a '1' transition.

            Assert.IsTrue(nfaTest.IsValid);
        }
        [Test]
        public void Should_Equal_When_InitalState_ComparedByInitialState()
        {
            List<char> alphabet = new List<char> { '0', '1' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.NFA, alphabet);

            FAState state = new FAState("A", isInitialState: true);
            automata.AddState(state);

            Assert.AreEqual(state, automata.InitialState);
        }
        [Test]
        public void Should_Equal_When_FinalState_ComparedByFinalState()
        {
            List<char> alphabet = new List<char> { '0', '1' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.NFA, alphabet);

            FAState state = new FAState("A", isFinalState: true);
            _ = automata.AddState(state);

            List<FAState> states = new List<FAState>() { state };
            Assert.AreEqual(states, automata.FinalState);
        }
        [Test]
        public void Should_ReturnTrue_When_Run_DFAWithABCABCABCABC()
        {
            List<char> alphabet = new List<char>() { 'a', 'b', 'c' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            _ = automata.AddState("q0", isInitialState: true, isFinalState: true);
            _ = automata.AddState("q1");
            _ = automata.AddState("q2");
            _ = automata.AddState("q3");

            _ = automata.AddTransition('a', "q0", "q1");
            _ = automata.AddTransition('b', "q0", "q1");
            _ = automata.AddTransition('c', "q0", "q1");

            _ = automata.AddTransition('a', "q1", "q2");
            _ = automata.AddTransition('b', "q1", "q2");
            _ = automata.AddTransition('c', "q1", "q2");

            _ = automata.AddTransition('a', "q2", "q3");
            _ = automata.AddTransition('b', "q2", "q3");
            _ = automata.AddTransition('c', "q2", "q3");

            _ = automata.AddTransition('a', "q3", "q0");
            _ = automata.AddTransition('b', "q3", "q0");
            _ = automata.AddTransition('c', "q3", "q0");

            Assert.True(automata.Run("abcabcabcabc"));
        }
        [Test]
        public void Should_ReturnFalse_When_Run_DFAWithABCABCABC()
        {
            List<char> alphabet = new List<char>() { 'a', 'b', 'c' };
            FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            _ = automata.AddState("q0", isInitialState: true, isFinalState: true);
            _ = automata.AddState("q1");
            _ = automata.AddState("q2");
            _ = automata.AddState("q3");

            _ = automata.AddTransition('a', "q0", "q1");
            _ = automata.AddTransition('b', "q0", "q1");
            _ = automata.AddTransition('c', "q0", "q1");

            _ = automata.AddTransition('a', "q1", "q2");
            _ = automata.AddTransition('b', "q1", "q2");
            _ = automata.AddTransition('c', "q1", "q2");

            _ = automata.AddTransition('a', "q2", "q3");
            _ = automata.AddTransition('b', "q2", "q3");
            _ = automata.AddTransition('c', "q2", "q3");

            _ = automata.AddTransition('a', "q3", "q0");
            _ = automata.AddTransition('b', "q3", "q0");
            _ = automata.AddTransition('c', "q3", "q0");

            Assert.False(automata.Run("abcabcabc"));
        }
        [Test]
        public void Should_ReturnTrue_When_IsValid_AutomataIsValid2DFA()
        {
            List<char> alphabet = new List<char> { '0', '1' };
            FiniteAutomata twdfaTest = new FiniteAutomata(FiniteAutomataType.TwoWayDFA, alphabet);

            _ = twdfaTest.AddState("q0", isInitialState: true);
            _ = twdfaTest.AddState("q1");
            _ = twdfaTest.AddState("q2");
            _ = twdfaTest.AddState("q3", isFinalState: true);

            _ = twdfaTest.AddTransition('0', "q0", "q1", 1);
            _ = twdfaTest.AddTransition('1', "q0", "q2", 1);

            _ = twdfaTest.AddTransition('0', "q1", "q3", 0);
            _ = twdfaTest.AddTransition('1', "q1", "q2", 0);

            _ = twdfaTest.AddTransition('0', "q2", "q2", 1);
            _ = twdfaTest.AddTransition('1', "q2", "q3", 1);

            _ = twdfaTest.AddTransition('0', "q3", "q1", 1);
            _ = twdfaTest.AddTransition('1', "q3", "q2", 0);

            Assert.True(twdfaTest.IsValid);
        }
    }
}