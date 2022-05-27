namespace OddLines
{
    using System;
    using System.IO;
    public class OddLines
    {
        static void Main()
        {
            string inputPath = @"..\..\..\Files\input.txt";
            string outputPath = @"..\..\..\Files\output.txt";

            ExtractOddLines(inputPath, outputPath);
        }

        public static void ExtractOddLines(string inputFilePath, string outputFilePath)
        {
            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                int lineIndex = 0;
                string line;
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    while((line = reader.ReadLine()) != null)
                    {
                        if(lineIndex % 2 == 1)
                            writer.WriteLine(line);

                        lineIndex++;
                    }
                }
            }

        }
    }
}
