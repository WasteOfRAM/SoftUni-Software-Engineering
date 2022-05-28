namespace EvenLines
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class EvenLines
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\text.txt";

            Console.WriteLine(ProcessLines(inputFilePath));
        }

        public static string ProcessLines(string inputFilePath)
        {
            using StreamReader reader = new StreamReader(inputFilePath);

            List<string> procesedLines = new List<string>();

            int lineIndex = 0;

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (lineIndex % 2 == 0)
                {
                    string reversedLine = line.Replace('-', '@')
                                              .Replace(',', '@')
                                              .Replace('.', '@')
                                              .Replace('!', '@')
                                              .Replace('?', '@');

                    string[] lineSplit = reversedLine.Split();

                    Array.Reverse(lineSplit);

                    reversedLine = string.Join(' ', lineSplit);
                    procesedLines.Add(reversedLine);

                }

                lineIndex++;
            }

            return string.Join(Environment.NewLine, procesedLines);
        }
    }
}
