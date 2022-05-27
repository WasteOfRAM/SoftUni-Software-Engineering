namespace WordCount
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    public class WordCount
    {
        static void Main(string[] args)
        {
            string wordPath = @"..\..\..\Files\words.txt";
            string textPath = @"..\..\..\Files\text.txt";
            string outputPath = @"..\..\..\Files\output.txt";

            CalculateWordCounts(wordPath, textPath, outputPath);
        }

        public static void CalculateWordCounts(string wordsFilePath, string textFilePath, string outputFilePath)
        {
            var wordsCount = new List<Word>();

            string[] test = new string[] { "ewrewr", "rfhdth", "Asdfsf" };

            using (var reader = new StreamReader(wordsFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    foreach (var word in words)
                    {
                        wordsCount.Add(new Word(word));
                    }
                }
            }

            using (var reader = new StreamReader(textFilePath))
            {
                char[] delimiters = new char[] { '.', '?', '!', ' ', ';', ':', ',', '-' };

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] lineSplit = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var word in wordsCount)
                    {
                        var matchCount = lineSplit.Where(w => w.Equals(word.WordToCount, StringComparison.InvariantCultureIgnoreCase)).ToArray();
                        word.Count += matchCount.Length;
                    }
                }
            }

            using (var writer = new StreamWriter(outputFilePath))
            {
                foreach (var word in wordsCount.OrderByDescending(w => w.Count))
                {
                    writer.WriteLine($"{word.WordToCount} - {word.Count}");
                }
            }

            
        }
    }

    class Word
    {
        public Word(string word)
        {
            this.WordToCount = word;
        }

        public string WordToCount { get; set; }
        public int Count { get; set; } = 0;
    }
    
}
