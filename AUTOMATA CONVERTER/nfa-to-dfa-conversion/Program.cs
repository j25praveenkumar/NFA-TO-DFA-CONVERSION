using nfa_to_dfa_conversion.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace nfa_to_dfa_conversion
{
    public static class Program
    {
        public static Stopwatch Timer = new Stopwatch();
        public static List<string> BenchmarkResults = new List<string>();
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter the input string: ");
            string inputString = Convert.ToString(Console.ReadLine());
            ConsoleOperations.WriteTitle("Input String");
            Console.WriteLine($">> {inputString}");

            NFAFunction(inputString);    
        }

        private static void NFAFunction(string inputString)
        {
            Timer.Start();
            FiniteAutomata automata = NFABuilder();
            ConsoleOperations.WriteBMarkReset("NFA Creation");
            ConsoleOperations.WriteTitle("NFA Info");
            ConsoleOperations.WriteAutomataInfo(automata);

            ConsoleOperations.WriteTitle("NFA Trace");
            Timer.Restart();
            bool result = automata.Run(inputString);
            ConsoleOperations.WriteBMarkReset("NFA Run");

            Timer.Restart();
            FiniteAutomataConverter dfaConverter = new FiniteAutomataConverter();
            FiniteAutomata DFAVersion = dfaConverter.ConvertNFAToDFA(automata);
            ConsoleOperations.WriteBMarkReset("Automata Conversion");

            Console.WriteLine("\n>>>NFA is converted to DFA<<<\n");
            ConsoleOperations.WriteTitle("DFA Info");
            ConsoleOperations.WriteAutomataInfo(DFAVersion);

            ConsoleOperations.WriteTitle("DFA Trace");
            Timer.Restart();
            bool resultDFA = DFAVersion.Run(inputString);
            ConsoleOperations.WriteBMarkReset("DFA Run");
            Timer.Stop();

            ConsoleOperations.WriteTitle("Automata Results");
            Console.WriteLine("NFA Response: Input is " + (result ? "Accepted" : "Rejected"));
            Console.WriteLine("DFA Response: Input is " + (resultDFA ? "Accepted" : "Rejected"));
        }
        

        private static FiniteAutomata NFABuilder()
        {
            List<char> alphabet = new List<char> { '0', '1' };
            FiniteAutomata nfaTest = new FiniteAutomata(FiniteAutomataType.NFA, alphabet);

            _ = nfaTest.AddState("A", isInitialState: true); //q0
            _ = nfaTest.AddState("B");                       //q1
            _ = nfaTest.AddState("C", isFinalState: true);   //q2

            _ = nfaTest.AddTransition('0', "A", "A");     // q0 goes to q0 with '0' transition.
            _ = nfaTest.AddTransition('1', "A", "B,C");   // q0 goes to q1 or q2 with transition '1'.

            _ = nfaTest.AddTransition('0', "B", "B");     // q1 goes to q1 with '0' transition.
            _ = nfaTest.AddTransition('1', "B", "A,C");   // q1 goes to q0 or q2 with transition '1'.

            _ = nfaTest.AddTransition('0', "C", "A,B");   // q2 goes to q0 or q1 with '0' transition.
            _ = nfaTest.AddTransition('1', "C", "C");     // q2 goes to q2 with transition '1'.

            return nfaTest;
        }
    }
}