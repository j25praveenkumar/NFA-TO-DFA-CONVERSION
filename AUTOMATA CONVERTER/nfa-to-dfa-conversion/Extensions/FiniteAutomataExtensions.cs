using nfa_to_dfa_conversion.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace nfa_to_dfa_conversion.Extensions
{
    public static class FiniteAutomataExtensions
    {
        private static readonly Random _randomObj = new Random();
        /// <summary>
        ///  Returns random state from state list.
        /// </summary>
        /// <param name="source">Source list</param>
        /// <returns></returns>
        public static FAState RandomState(this IEnumerable<FAState> source)
        {
            int index = _randomObj.Next(0, source.Count());
            return source.ToArray()[index];
        }
    }
}
