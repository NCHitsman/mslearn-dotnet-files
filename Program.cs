using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace files_module
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var storesDirectory = Path.Combine(currentDirectory, "stores");

            var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
            Directory.CreateDirectory(salesTotalDir);

            var salesFiles = FindFiles(storesDirectory);

            var salesTotal = CalculateSalesTotal(salesFiles);

            File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"${salesTotal}\n");

        }

        static IEnumerable<string> FindFiles(string folderName)
        {
            List<string> salesFiles = new List<string>();
            var foundFiles = Directory.EnumerateFiles(folderName, "*.jsoon", SearchOption.AllDirectories);
            foreach (var file in foundFiles)
            {
                salesFiles.Add(file);
            }

            return salesFiles;
        }

        static double CalculateSalesTotal(IEnumerable<string> salesFiles)
        {
            double salesTotal = 0;

            foreach (var file in salesFiles)
            {
                var fileInfo = File.ReadAllText(file);
                var fileObj = JsonConvert.DeserializeObject<SalesData>(fileInfo);
                Console.WriteLine($"Before Total:{salesTotal}, Adding Amount: {fileObj.Total}, New Total: {salesTotal + fileObj.Total}");
                salesTotal += fileObj.Total;
            }

            return salesTotal;
        }

        class SalesData
        {
            public double Total { get; set; }
        }
    }
}
