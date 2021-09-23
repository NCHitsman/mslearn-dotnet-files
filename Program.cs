using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

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

        static IEnumerable<string> FindFiles(string folderName) => Directory.EnumerateFiles(folderName, "*.json", SearchOption.AllDirectories);

        static double CalculateSalesTotal(IEnumerable<string> salesFiles) => salesFiles.Aggregate(0.0, (acc, file) => acc + JsonConvert.DeserializeObject<SalesData>(File.ReadAllText(file)).Total);

        class SalesData
        {
            public double Total { get; set; }
        }
    }
}
