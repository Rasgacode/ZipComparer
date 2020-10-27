using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirectoryComparer
{
    public class DirectoryComparer
    {
        private readonly string oldDirectoryPath;
        private readonly string newDirecotryPath;
        private readonly IEnumerable<string> oldProjectFiles;
        private readonly IEnumerable<string> newProjectFiles;

        public DirectoryComparer(string _oldDirectoryPath, string _newDirectoryPath)
        {
            oldDirectoryPath = CorrectDirectoryPath(_oldDirectoryPath);
            newDirecotryPath = CorrectDirectoryPath(_newDirectoryPath);
            oldProjectFiles = GetDirectoryFiles(oldDirectoryPath);
            newProjectFiles = GetDirectoryFiles(newDirecotryPath);
        }

        public void CompareDirectoriesFileCount()
        {
            IEnumerable<string> missingFromNew = oldProjectFiles.Where(file => !newProjectFiles.Contains(file));
            IEnumerable<string> missingFromOld = newProjectFiles.Where(file => !oldProjectFiles.Contains(file));

            if (missingFromNew.Count() > 0)
            {
                Console.WriteLine("These files were in OldProjectDirectory but not in the NewProjectDirectory:");
                foreach (string item in missingFromNew)
                {
                    Console.Write($"{item}, ");
                }
                Console.WriteLine();
            }

            if (missingFromOld.Count() > 0)
            {
                Console.WriteLine("These files were in NewProjectDirectory but not in the OldProjectDirectory:");
                foreach (string item in missingFromOld)
                {
                    Console.Write($"{item}, ");
                }
                Console.WriteLine();
            }

            if (missingFromOld.Count() == 0 && missingFromNew.Count() == 0)
            {
                Console.WriteLine("The two directories contains the same files.");
            }
        }

        public void CompareDirectoriesFileContent() 
        {
            bool success = true;
            IEnumerable<string> matchingFiles = oldProjectFiles.Where(file => newProjectFiles.Contains(file));

            foreach (string file in matchingFiles)
            {
                string oldProjectFilePath = oldDirectoryPath + file;
                string newProjectFilePath = newDirecotryPath + file;
                
                if (!FileCompare(oldProjectFilePath, newProjectFilePath))
                {
                    Console.WriteLine($"{oldProjectFilePath} and {newProjectFilePath} was not the same file.");
                    success = false;
                }
            }

            if (success && matchingFiles.Count() > 0)
            {
                Console.WriteLine("All matching files (depend on filename) have the same content in the selected directories.");
            }

            if (matchingFiles.Count() == 0)
            {
                Console.WriteLine("There are no files in the two directories that are matching.");
            }
        }

        private bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            if (file1 == file2)
            {
                return true;
            }

            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

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

        private IEnumerable<string> GetDirectoryFiles(string directoryPath)
        {
            try
            {
                return Directory.GetFiles(directoryPath).Select(file => file.Replace(directoryPath, ""));
            }
            catch (Exception)
            {
                Console.WriteLine($"Something is wrong with this path: {directoryPath}");
                throw new DirectoryNotFoundException();
            }
        }

        private string CorrectDirectoryPath(string directoryPath)
        {
            return directoryPath.Last() == '\\' ? directoryPath : directoryPath + @"\";
        }
    }
}
