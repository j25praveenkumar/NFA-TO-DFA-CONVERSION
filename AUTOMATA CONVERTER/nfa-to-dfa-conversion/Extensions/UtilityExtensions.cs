using System.Collections.Generic;

namespace nfa_to_dfa_conversion.Extensions
{
    public static class UtilityExtensions
    {

        /// <summary>
        /// Adds the item to list if item does not exist.
        /// </summary>
        /// <param name="list">Source list</param>
        /// <param name="input">Input list</param>
        public static void AddNotExists(this IList<string> list, IEnumerable<string> input)
        {
            foreach (string item in input)
            {
                if (list.Contains(item))
                {
                    continue;
                }
                list.Add(item);
            }
        }
    }
}
