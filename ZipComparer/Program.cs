using System;

namespace DirectoryComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = TakeInputs(args);
            DirectoryComparer dc = new DirectoryComparer(inputs[0], inputs[1]);
            dc.CompareDirectoriesFileCount();
            dc.CompareDirectoriesFileContent();
        }

        private static string[] TakeInputs(string[] args)
        {
            string[] inputs = new string[2];

            if (args.Length != 0)
            {
                inputs[0] = args[0];
                inputs[1] = args[1];
            }
            else
            {
                Console.WriteLine("Old project directory's path:");
                inputs[0] = @$"{Console.ReadLine()}";
                Console.WriteLine("New project directory's path:");
                inputs[1] = @$"{Console.ReadLine()}";
            }

            return inputs;
        }
    }
}
