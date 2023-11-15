namespace ColorReplacement
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Dictionary<string, string> colors = new ();
            using (var source = new StreamReader("Data/colors.txt", Encoding.UTF8))
            {
                string line;
                while ((line = source.ReadLine()) != null)
                {
                    string[] nameAndColor = line.Split(" ");
                    colors.Add(nameAndColor[1], nameAndColor[0]);
                }
            }

            Regex regex = new Regex(@"#([0-9A-F]{6}|[0-9A-F]{3})|rgb\((\s*\d{1,3}\s*,\s*){2}\d{1,3}\s*\)", RegexOptions.IgnoreCase);

            SortedList<string, string> usedColors = new ();
            using (var source = new StreamReader("Data/source.txt", Encoding.UTF8))
            using (var target = new StreamWriter("Data/target.txt"))
            {
                var text = source.ReadToEnd();

                text = regex.Replace(text, (match) =>
                {
                    string hexColor = ConvertToHex(match.Value);
                    if (colors.TryGetValue(hexColor, out string value))
                    {
                        usedColors.TryAdd(hexColor, value);
                        return value;
                    }
                    else
                    {
                        return match.Value;
                    }
                });

                target.Write(text);

                // reads source.txt, replaces colors, writes target.txt, collects data about replaced colors
            }

            using (var target = new StreamWriter("Data/used_colors.txt"))
            {
                foreach (var usedColor in usedColors)
                {
                    target.WriteLine($"{usedColor.Key} {usedColor.Value}");
                }
            }
        }

        private static string ConvertToHex(string notHex)
        {
            if (notHex.StartsWith("#") && notHex.Length == 4)
            {
                notHex = $"{notHex[1]}{notHex[1]}{notHex[2]}{notHex[2]}{notHex[3]}{notHex[3]}";
            }
            else if (notHex.StartsWith("rgb"))
            {
                string rgbPattern = @"rgb\((\s*(\d{1,3})\s*,\s*){2}(\d{1,3})\s*\)";

                Match match = Regex.Match(notHex, rgbPattern);

                if (match.Success)
                {
                    int red = int.Parse(match.Groups[2].Captures[0].Value);
                    int green = int.Parse(match.Groups[2].Captures[1].Value);
                    int blue = int.Parse(match.Groups[3].Value);

                    notHex = "#" + red.ToString("X2") + green.ToString("X2") + blue.ToString("X2");
                }
            }

            return notHex;
        }
    }
}
