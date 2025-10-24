using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Name_sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check for argument
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: name-sorter <input-file-path>");
                return;
            }

            string inputFile = args[0];
            string outputFile = "sorted-names-list.txt";

            try
            {
                // Read names from file
                var names = File.ReadAllLines(inputFile)
                                .Where(line => !string.IsNullOrWhiteSpace(line))
                                .ToList();

                // Sort names
                var sortedNames = NameSorter.SortNames(names);

                // Print to console
                foreach (var name in sortedNames)
                    Console.WriteLine(name);

                // Write to file
                File.WriteAllLines(outputFile, sortedNames);

                Console.WriteLine($"\nSorted names written to {outputFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }


    //I could have moved this small class to its own file but as it's trivial and only used here, I'll leave it here.
    public static class NameSorter
    {
        public static List<string> SortNames(List<string> names)
        {
            return names.OrderBy(n =>
            {
                var parts = n.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string lastName = parts.Last();
                string givenNames = string.Join(" ", parts.Take(parts.Length - 1));
                return (lastName, givenNames);
            }).ToList();
        }
    }
}

