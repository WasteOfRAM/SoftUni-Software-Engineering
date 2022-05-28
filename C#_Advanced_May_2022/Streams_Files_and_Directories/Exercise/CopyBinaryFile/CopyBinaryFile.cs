namespace CopyBinaryFile
{
    using System;
    using System.IO;

    public class CopyBinaryFile
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\copyMe.png";
            string outputFilePath = @"..\..\..\copyMe-copy.png";

            CopyFile(inputFilePath, outputFilePath);
        }

        public static void CopyFile(string inputFilePath, string outputFilePath)
        {
            using FileStream stream = new FileStream(inputFilePath, FileMode.Open);

            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);

            using FileStream streamWriter = new FileStream(outputFilePath, FileMode.Create);

            streamWriter.Write(buffer, 0, buffer.Length);
        }
    }
}
