namespace SetCover
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class StartUp
    {
        static void Main()
        {
            int[] universe = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            int numberOfSets = int.Parse(Console.ReadLine());
            int[][] sets = new int[numberOfSets][];

            for (int row = 0; row < sets.Length; row++)
            {
                sets[row] = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            }

            List<int[]> selectedSets = ChooseSets(sets, universe);
            Console.WriteLine($"Sets to take ({selectedSets.Count}):");

            foreach (var set in selectedSets)
            {
                Console.WriteLine($"{{ {string.Join(", ", set)} }}");
            }
        }

        public static List<int[]> ChooseSets(IList<int[]> sets, IList<int> universe)
        {
            var selectedSets = new List<int[]>();

            while (universe.Count > 0)
            {
                // Returns the largest array that contains the most elements that are int the univers array
                int[] longestSet = sets.OrderByDescending(s => s.Count(x => universe.Contains(x))).FirstOrDefault();

                selectedSets.Add(longestSet);
                sets = sets.Where(s => s != longestSet).ToArray();
                universe = universe.Where(e => !longestSet.Contains(e)).ToArray();
            }

            return selectedSets;
        }
    }
}
