namespace CopyDirectory
{
    using System;
    using System.IO;

    public class CopyDirectory
    {
        static void Main()
        {
            string inputPath =  @$"{Console.ReadLine()}";
            string outputPath = @$"{Console.ReadLine()}";

            CopyAllFiles(inputPath, outputPath);
        }

        public static void CopyAllFiles(string inputPath, string outputPath)
        {
            DirectoryInfo inputDir = new DirectoryInfo(inputPath);

            string[] files = Directory.GetFiles(inputPath);

            if(Directory.Exists(outputPath))
                Directory.Delete(outputPath, true);

            DirectoryInfo outputDir = Directory.CreateDirectory(outputPath);

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string distPath = Path.Combine(outputPath, fileName);
                File.Copy(file, distPath, true);
            }
        }
    }
}
