namespace WordCount
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string filesText = File.ReadAllText(@"Data\Беда_одна_не_ходит.txt");
            filesText += File.ReadAllText(@"Data\Начало.txt", Encoding.GetEncoding(1251));
            filesText += File.ReadAllText(@"Data\Хэппи_Энд.txt");

            int wordCount = CalculateWordCount(filesText);

            Console.WriteLine("Count of words in story files: {0}", wordCount);
        }

        private static int CalculateWordCount(string text)
        {
            Regex regex = new Regex(@"\b\w+\b", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(text);

            HashSet<string> uniqueWords = new HashSet<string>();

            foreach (Match match in matches)
            {
                uniqueWords.Add(match.Value.ToLower());
            }

            return uniqueWords.Count;
        }
    }
}
