using nfa_to_dfa_conversion.Models;
using NUnit.Framework;

namespace nfa_to_dfa_conversion.Tests
{
    public class FiniteAutomataTransitionTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Should_ReturnsZero_When_CompareTo_Transition()
        {
            FAState state = new FAState("State1");
            FAState state2 = new FAState("State2");

            FATransition transition1 = new FATransition('a', state, state2);
            FATransition transition2 = new FATransition('a', state, state2);

            Assert.Zero(transition1.CompareTo(transition2));
        }
        [Test]
        public void Should_ReturnsNegative_When_CompareTo_Transition()
        {
            FAState state = new FAState("State1");
            FAState state2 = new FAState("State2");

            FATransition transition1 = new FATransition('a', state, state2);
            FATransition transition2 = new FATransition('b', state, state2);

            Assert.AreEqual(-1, transition1.CompareTo(transition2));
        }
    }
}