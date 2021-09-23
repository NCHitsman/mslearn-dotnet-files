using System;
using System.IO;
using System.Collections.Generic;


namespace files_module
{
    class Program
    {
        static void Main(string[] args)
        {
            var salesFiles = FindFiles("stores");

            foreach (var file in salesFiles)
            {
                Console.WriteLine(file);
            }

            string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";

            FileInfo info = new FileInfo(fileName);

            Console.WriteLine($"Full Name: {info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}");

        }

        static IEnumerable<string> FindFiles(string folderName)
        {
            List<string> salesFiles = new List<string>();

            var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

            foreach (var file in foundFiles)
            {
                if (file.EndsWith("sales.json"))
                {
                    salesFiles.Add(file);
                }
            }

            return salesFiles;

        }
    }
}
