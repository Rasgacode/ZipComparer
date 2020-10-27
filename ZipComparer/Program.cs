using System;

namespace DirectoryComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = TakeInputs();
            DirectoryComparer dc = new DirectoryComparer(inputs[0], inputs[1]);
            dc.CompareDirectoriesFileCount();
            dc.CompareDirectoriesFileContent();
        }

        private static string[] TakeInputs()
        {
            string[] inputs = new string[2];

            Console.WriteLine("Old project directory's path:");
            inputs[0] = @$"{Console.ReadLine()}";
            Console.WriteLine("New project directory's path:");
            inputs[1] = @$"{Console.ReadLine()}";

            return inputs;
        }
    }
}
