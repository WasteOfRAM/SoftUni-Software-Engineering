namespace MergeFiles
{
    using System;
    using System.IO;
    public class MergeFiles
    {
        static void Main(string[] args)
        {
            var firstInputFilePath = @"..\..\..\Files\input1.txt";
            var secondInputFilePath = @"..\..\..\Files\input2.txt";
            var outputFilePath = @"..\..\..\Files\output.txt";

            MergeTextFiles(firstInputFilePath, secondInputFilePath, outputFilePath);
        }

        public static void MergeTextFiles(string firstInputFilePath, string secondInputFilePath, string outputFilePath)
        {
            using (StreamReader readerInput1 = new StreamReader(firstInputFilePath), readerInput2 = new StreamReader(secondInputFilePath))
            {
                string line1 = readerInput1.ReadLine();
                string line2 = readerInput2.ReadLine();
                using (var writer = new StreamWriter(outputFilePath))
                {
                    while (true)
                    {
                        if (line1 == null && line2 == null)
                            break;

                        if (line1 != null)
                            writer.WriteLine(line1);

                        if (line2 != null)
                            writer.WriteLine(line2);


                        line1 = readerInput1.ReadLine();
                        line2 = readerInput2.ReadLine();
                    }
                }
            }
        }
    }
}
