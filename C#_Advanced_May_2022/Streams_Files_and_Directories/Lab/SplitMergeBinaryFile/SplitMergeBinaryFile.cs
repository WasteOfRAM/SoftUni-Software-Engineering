namespace SplitMergeBinaryFile
{
    using System;
    using System.IO;
    using System.Linq;

    public class SplitMergeBinaryFile
    {
        static void Main(string[] args)
        {
            string sourceFilePath = @"..\..\..\Files\example.png";
            string joinedFilePath = @"..\..\..\Files\example-joined.png";
            string partOnePath = @"..\..\..\Files\part-1.bin";
            string partTwoPath = @"..\..\..\Files\part-2.bin";

            SplitBinaryFile(sourceFilePath, partOnePath, partTwoPath);
            MergeBinaryFiles(partOnePath, partTwoPath, joinedFilePath);
        }

        public static void SplitBinaryFile(string sourceFilePath, string partOneFilePath, string partTwoFilePath)
        {
            using FileStream stream = File.OpenRead(sourceFilePath);
            long partOneLenght = (long)decimal.Ceiling((decimal)stream.Length / 2);
            long partTwoLenght = (long)decimal.Floor((decimal)stream.Length / 2);

            byte[] partOneBytes = new byte[partOneLenght];
            byte[] partTwoBytes = new byte[partTwoLenght];

            stream.Read(partOneBytes, 0, partOneBytes.Length);
            stream.Read(partTwoBytes, 0, partTwoBytes.Length);

            using FileStream partOneWriter = File.Create(partOneFilePath), partTwoWriter = File.Create(partTwoFilePath);

            partOneWriter.Write(partOneBytes, 0, partOneBytes.Length);
            partTwoWriter.Write(partTwoBytes, 0, partTwoBytes.Length);
        }

        public static void MergeBinaryFiles(string partOneFilePath, string partTwoFilePath, string joinedFilePath)
        {
            using FileStream partOne = File.OpenRead(partOneFilePath), partTwo = File.OpenRead(partTwoFilePath);
            byte[] partOneBytes = new byte[partOne.Length];
            byte[] partTwoBytes = new byte[partTwo.Length];

            partOne.Read(partOneBytes, 0, partOneBytes.Length);
            partTwo.Read(partTwoBytes, 0, partTwoBytes.Length);

            byte[] mergedBytes = partOneBytes.Concat(partTwoBytes).ToArray();

            using FileStream joinWriter = File.Create(joinedFilePath);
            joinWriter.Write(mergedBytes, 0, mergedBytes.Length);
        }
    }
}