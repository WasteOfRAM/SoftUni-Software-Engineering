namespace ExtractBytes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    public class ExtractBytes
    {
        static void Main(string[] args)
        {
            string binaryFilePath = @"..\..\..\Files\example.png";
            string bytesFilePath = @"..\..\..\Files\bytes.txt";
            string outputPath = @"..\..\..\Files\output.bin";

            ExtractBytesFromBinaryFile(binaryFilePath, bytesFilePath, outputPath);
        }

        public static void ExtractBytesFromBinaryFile(string binaryFilePath, string bytesFilePath, string outputPath)
        {
            List<int> specialBytes = new List<int>();

            using (StreamReader reader = new StreamReader(bytesFilePath))
            {

                while (true)
                {
                    var line = reader.ReadLine();

                    if (line == null)
                        break;

                    byte byteFromLine = (byte)int.Parse(line);

                    specialBytes.Add(byteFromLine);
                }
            }


            using (var byteReadere = new FileStream(binaryFilePath, FileMode.Open))
            {
                using (var byteRiter = new FileStream(outputPath, FileMode.Create))
                {
                    byte[] buffer = new byte[4096];

                    while (true)
                    {
                        int bytesRead = byteReadere.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                            break;

                        foreach (var item in buffer)
                        {
                            if (specialBytes.Contains(item))
                                byteRiter.WriteByte(item);
                        }
                    }
                }
            }
        }
    }
}
