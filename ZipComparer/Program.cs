using System;
using System.IO;
using System.Linq;

namespace ZipComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            ZipCompare();
        }

        private static void ZipCompare()
        {
            string[] inputs = new string[2];

            while (!inputs.Contains("exit"))
            {
                Console.WriteLine("First zip's path:");
                inputs[0] = @$"{Console.ReadLine()}";
                Console.WriteLine("Second zip's path:");
                inputs[1] = @$"{Console.ReadLine()}";
                try
                {
                    if (FileCompare(inputs))
                    {
                        Console.WriteLine("Files are equal.");
                    }
                    else
                    {
                        Console.WriteLine("Files are not equal.");
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File was not found (wrong path)!");
                }

            }
        }

        private static bool FileCompare(string[] inputs)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            if (inputs[0] == inputs[1])
            {
                return true;
            }

            fs1 = new FileStream(inputs[0], FileMode.Open);
            fs2 = new FileStream(inputs[1], FileMode.Open);

            if (fs1.Length != fs2.Length)
            {
                fs1.Close();
                fs2.Close();

                return false;
            }

            do
            {
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            fs1.Close();
            fs2.Close();

            return ((file1byte - file2byte) == 0);
        }
    }
}
