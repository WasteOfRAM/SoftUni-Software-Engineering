namespace FolderSize
{
    using System;
    using System.IO;
    public class FolderSize
    {
        static void Main(string[] args)
        {
            string folderPath = @"..\..\..\Files\TestFolder";
            string outputPath = @"..\..\..\Files\output.txt";

            GetFolderSize(folderPath, outputPath);
        }

        public static void GetFolderSize(string folderPath, string outputFilePath)
        {
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);

            double sum = 0;

            foreach (var file in files)
            {
                sum += file.Length;
            }

            sum /= 1024;

            File.WriteAllText(outputFilePath, sum.ToString() + " KB");
        }
    }
}
