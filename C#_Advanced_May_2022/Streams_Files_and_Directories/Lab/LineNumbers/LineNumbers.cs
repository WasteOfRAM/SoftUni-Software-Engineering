namespace LineNumbers
{
    using System.IO;
    public class LineNumbers
    {
        static void Main(string[] args)
        {
            string inputPath = @"..\..\..\Files\input.txt";
            string outputPath = @"..\..\..\Files\output.txt";

            RewriteFileWithLineNumbers(inputPath, outputPath);
        }

        public static void RewriteFileWithLineNumbers(string inputFilePath, string outputFilePath)
        {
            using (var reader = new StreamReader(inputFilePath))
            {
                int lineNumber = 1;

                string line;
                using (var writer = new StreamWriter(outputFilePath))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        writer.WriteLine($"{lineNumber}. {line}");
                        lineNumber++;
                    }
                }
            }
        }
    }
}
