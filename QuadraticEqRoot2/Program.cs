namespace QuadraticEqRoot2
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    internal class Program
    {
        private static void Main()
        {
            const string outputPath = "output.txt";

            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            using (var input = File.OpenText("input.txt"))
            using (var output = File.CreateText(outputPath))
            {
                string line;

                while ((line = input.ReadLine()) != null)
                {
                    List<double> values;
                    try
                    {
                        values = line.Split(',')
                        .Select(x => double.Parse(x, CultureInfo.InvariantCulture))
                        .ToList();

                        if (values.Count != 3)
                        {
                            throw new Exception("wrong number of arguments");
                        }

                        IList<double> roots = Solver.Solve(values[0], values[1], values[2]);

                        string rootsLine = string.Join(",", roots.Select(x => x.ToString(CultureInfo.InvariantCulture)));

                        output.WriteLine(rootsLine);
                    }
                    catch (Exception ex)
                    {
                        output.WriteLine($"error: {ex}");
                    }
                }
            }
        }
    }
}
