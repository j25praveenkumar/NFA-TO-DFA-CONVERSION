using nfa_to_dfa_conversion.Exceptions;
using nfa_to_dfa_conversion.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace nfa_to_dfa_conversion.Tests
{
    public class FiniteAutomataConverterTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Should_ReturnsDFAAutomata_When_ConvertNFAToDFA_WithValidNFA()
        {
            List<char> alphabet = new List<char> { '0', '1' };
            FiniteAutomata nfaTest = new FiniteAutomata(FiniteAutomataType.NFA, alphabet);

            _ = nfaTest.AddState("A", isInitialState: true); //A
            _ = nfaTest.AddState("B");                       //B
            _ = nfaTest.AddState("C", isFinalState: true);   //C

            _ = nfaTest.AddTransition('0', "A", "A");     // A goes to A with a '0' transition.
            _ = nfaTest.AddTransition('1', "A", "B,C");   // A goes to B or C with a '1' transition.

            _ = nfaTest.AddTransition('0', "B", "A");     // B goes to B with '0' transition.
            _ = nfaTest.AddTransition('1', "B", "A,C");   // B goes to A or C with transition '1'.

            _ = nfaTest.AddTransition('0', "C", "A,B");   // C goes to A or B with a '0' transition.
            _ = nfaTest.AddTransition('1', "C", "C");     // C goes to C with transition '1'.

            FiniteAutomata dfaTest = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);
            _ = dfaTest.AddState("A", isInitialState: true);
            _ = dfaTest.AddState("B&C", isFinalState: true);
            _ = dfaTest.AddState("A&B");
            _ = dfaTest.AddState("A&C", isFinalState: true);
            _ = dfaTest.AddState("A&B&C", isFinalState: true);

            _ = dfaTest.AddTransition('0', "A", "A");
            _ = dfaTest.AddTransition('1', "A", "B&C");

            _ = dfaTest.AddTransition('0', "B&C", "A&B");
            _ = dfaTest.AddTransition('1', "B&C", "A&C");

            _ = dfaTest.AddTransition('0', "A&B", "A");
            _ = dfaTest.AddTransition('1', "A&B", "A&B&C");

            _ = dfaTest.AddTransition('0', "A&C", "A&B");
            _ = dfaTest.AddTransition('1', "A&C", "B&C");

            _ = dfaTest.AddTransition('0', "A&B&C", "A&B");
            _ = dfaTest.AddTransition('1', "A&B&C", "A&B&C");


            FiniteAutomataConverter automataConverter = new FiniteAutomataConverter();
            FiniteAutomata converterDFA = automataConverter.ConvertNFAToDFA(nfaTest);

            if (converterDFA.InitialState.StateName == dfaTest.InitialState.StateName
                && converterDFA.FinalState.Count() == dfaTest.FinalState.Count()
                && converterDFA.States.Count() == dfaTest.States.Count()
                && converterDFA.Transitions.Count() == dfaTest.Transitions.Count()
                && converterDFA.States.Count() == dfaTest.States.Count()
                && converterDFA.Transitions.Last().TransitionSymbol == dfaTest.Transitions.Last().TransitionSymbol)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }
        [Test]
        public void Should_ThrowException_When_ConvertNFAToDFA_WithNullParameter()
        {
            _ = Assert.Throws<FAConverterException>(delegate
              {
                  FiniteAutomataConverter converter = new FiniteAutomataConverter();
                  _ = converter.ConvertNFAToDFA(null);
              });
        }
        [Test]
        public void Should_ReturnsItSelf_When_ConvertNFAToDFA_WithDFAParameter()
        {
            List<char> alphabet = new List<char> { 'a', 'b', 'c' };
            FiniteAutomata dfaTest = new FiniteAutomata(FiniteAutomataType.DFA, alphabet);

            _ = dfaTest.AddState(true); // q0
            _ = dfaTest.AddState(); // q1
            _ = dfaTest.AddState(isFinalState: true); // q2

            _ = dfaTest.AddTransition('a', "q0", "q1"); // q0 goes to q1 with 'a' transition.
            _ = dfaTest.AddTransition('b', "q0", "q2"); // q0 goes to q2 with 'b' transition.
            _ = dfaTest.AddTransition('c', "q0", "q0"); // q0 goes to q0 with 'c' transition.

            _ = dfaTest.AddTransition('a', "q1", "q0"); // q1 goes to q0 with 'a' transition.
            _ = dfaTest.AddTransition('b', "q1", "q1"); // q1 goes to q1 with 'b' transition.
            _ = dfaTest.AddTransition('c', "q1", "q2"); // q1 goes to q2 with 'c' transition.

            _ = dfaTest.AddTransition('a', "q2", "q0"); // q2 goes to q0 with 'a' transition.
            _ = dfaTest.AddTransition('b', "q2", "q2"); // q2 goes to q2 with 'b' transition.
            _ = dfaTest.AddTransition('c', "q2", "q2"); // q2 goes to q2 with 'c' transition.

            FiniteAutomataConverter converter = new FiniteAutomataConverter();
            FiniteAutomata converterDFA = converter.ConvertNFAToDFA(dfaTest);

            if (converterDFA.InitialState.StateName == dfaTest.InitialState.StateName
               && converterDFA.FinalState.Count() == dfaTest.FinalState.Count()
               && converterDFA.States.Count() == dfaTest.States.Count()
               && converterDFA.Transitions.Count() == dfaTest.Transitions.Count()
               && converterDFA.States.Count() == dfaTest.States.Count()
               && converterDFA.Transitions.Last().TransitionSymbol == dfaTest.Transitions.Last().TransitionSymbol)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void Should_ThrowException_When_Convert2DFAToDFA_WithNullParameter()
        {
            _ = Assert.Throws<FAConverterException>(delegate
              {
                  FiniteAutomataConverter converter = new FiniteAutomataConverter();
                  _ = converter.Convert2DFAToDFA(null);
              });
        }
        [Test]
        public void Should_ThrowException_When_Convert2DFAToDFA_InputAutomataTypeIsNot2DFA()
        {
            _ = Assert.Throws<FAConverterException>(delegate
              {
                  List<char> alphabet = new List<char> { 'a', 'b' };
                  FiniteAutomata automata = new FiniteAutomata(FiniteAutomataType.NFA, alphabet);
                  FiniteAutomataConverter converter = new FiniteAutomataConverter();
                  _ = converter.Convert2DFAToDFA(automata);
              });
        }
        [Test]
        public void Should_ReturnsDFAAutomata_When_Convert2DFAToDFA_WithValid2DFA()
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

            FiniteAutomataConverter automataConverter = new FiniteAutomataConverter();
            FiniteAutomata converterDFA = automataConverter.Convert2DFAToDFA(twdfaTest);

            if (converterDFA.InitialState.StateName == twdfaTest.InitialState.StateName
                && converterDFA.FinalState.Count() == twdfaTest.FinalState.Count())
            {
                Assert.Pass();
            }

            Assert.Fail();
        }
    }
}