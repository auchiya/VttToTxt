using System.Text.RegularExpressions;

if (args.Length == 0)
{
    Console.WriteLine("Please drag and drop a .vtt file onto the executable.");
    return;
}

string filePath = args[0];

if (!File.Exists(filePath) || Path.GetExtension(filePath).ToLower() != ".vtt")
{
    Console.WriteLine("Invalid file. Please provide a .vtt file.");
    return;
}

try
{
    string fileContent = File.ReadAllText(filePath);

    string pattern = @"(.+?)\r\n\d\d:\d\d:\d\d\.\d\d\d\s-->\s\d\d:\d\d:\d\d\.\d\d\d\r\n(.+)";
    MatchCollection matches = Regex.Matches(fileContent, pattern, RegexOptions.Multiline);

    string outputFilePath = "output.txt";
    if (File.Exists(outputFilePath))
    {
        File.Delete(outputFilePath);
    }

    using (StreamWriter outputFile = new StreamWriter(outputFilePath))
    {
        foreach (Match match in matches)
        {
            Console.WriteLine(match.Groups[2].Value.Trim());

            string outputLine = match.Groups[2].Value.Trim();
            outputFile.WriteLine(outputLine);
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error reading file: " + ex.Message);
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();