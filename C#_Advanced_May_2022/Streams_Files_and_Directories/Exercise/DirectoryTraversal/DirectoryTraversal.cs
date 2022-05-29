namespace DirectoryTraversal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class DirectoryTraversal
    {
        static void Main()
        {
            string path = Console.ReadLine();
            string reportFileName = @"\report.txt";

            string reportContent = TraverseDirectory(path);
            Console.WriteLine(reportContent);

            WriteReportToDesktop(reportContent, reportFileName);
        }

        public static string TraverseDirectory(string inputFolderPath)
        {
            DirectoryInfo dir = new DirectoryInfo(inputFolderPath);
            FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);

            var results = new Dictionary<string, List<KeyValuePair<string, decimal>>>();

            StringBuilder sb = new StringBuilder();

            foreach (FileInfo file in files)
            {
                if (!results.ContainsKey(file.Extension))
                    results[file.Extension] = new List<KeyValuePair<string, decimal>>();

                results[file.Extension].Add(new KeyValuePair<string, decimal>(file.Name, (decimal)file.Length / 1024));              //($"--{file.Name} - {file.Length / 1024}kb");
            }

            foreach (var item in results.OrderByDescending(f => f.Value.Count).ThenBy(f => f.Key))
            {
                sb.AppendLine(item.Key);
                foreach (var (name, size) in item.Value.OrderBy(x => x.Value))
                {
                    sb.AppendLine($"--{name} - {size:f3}kb");
                }
            }

            return sb.ToString();
        }

        public static void WriteReportToDesktop(string textContent, string reportFileName)
        {
            string outputFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + reportFileName;
            
            File.WriteAllText(outputFile, textContent);
        }
    }
}
