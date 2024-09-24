using nfa_to_dfa_conversion.Models;
using System;
using System.Linq;

namespace nfa_to_dfa_conversion
{
    public static class ConsoleOperations
    {

        public static void WriteBMarkReset(string message)
        {
            Program.BenchmarkResults.Add($"[Time To Completion] {message}: {Program.Timer.ElapsedMilliseconds} ms");
            Program.Timer.Restart();
        }

        public static void WriteAutomataInfo(FiniteAutomata automata)
        {
            string automataType = automata.AutomataType.ToString();
            Console.WriteLine($"{automataType} Alphabet: {string.Join(",", automata.Alphabet)}");
            Console.WriteLine($"{automataType} States: {string.Join(",", automata.States)}");
            Console.WriteLine($"{automataType} Initial State: {automata.InitialState}");
            Console.WriteLine($"{automataType} Final States: {string.Join(",", automata.FinalState)}\n");
            Console.WriteLine($"{automataType} Transitions\n--------------------");
            automata.Transitions.ToList().ForEach(tr => Console.WriteLine(tr.ToString()));
            Console.WriteLine();

            Program.Timer.Restart();
            bool automataIsValid = automata.IsValid;
            WriteBMarkReset($"{automataType} Validation");

            Console.Write($"{automataType} Validation: ");
            Console.WriteLine(automataIsValid ? "Valid" : "Invalid");
            Console.WriteLine("\n");
        }

        public static void WriteTitle(string message)
        {
            Console.WriteLine($"\n{message}");
            Console.WriteLine("------------------");
        }
    }
}
