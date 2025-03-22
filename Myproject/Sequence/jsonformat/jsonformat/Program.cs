using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main(string[] args)
    {
        string inputDirectory = @"C:\Users\Lenovo\Downloads\TrainingData";
        string outputDirectory = @"C:\Users\Lenovo\Downloads\TrainingData";

        ProcessDirectory(inputDirectory, outputDirectory);
    }

    static void ProcessDirectory(string inputDir, string outputDir)
    {
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        foreach (string file in Directory.GetFiles(inputDir, "*.json"))
        {
            string fileName = Path.GetFileName(file);
            string outputFile = Path.Combine(outputDir, fileName);
            ProcessFile(file, outputFile);
        }
    }

    static void ProcessFile(string inputFile, string outputFile)
    {
        try
        {
            string content = File.ReadAllText(inputFile);
            string cleanedContent = CleanJsonContent(content);

            if (TryParseJson(cleanedContent, out JArray jsonData))
            {
                // Trim the SequenceData arrays to a maximum of 20 values
                TrimSequenceData(jsonData);

                File.WriteAllText(outputFile, JsonConvert.SerializeObject(jsonData, Formatting.Indented));
                Console.WriteLine($"Cleaned and trimmed JSON written to {outputFile}");
            }
            else
            {
                Console.WriteLine($"Failed to clean and write JSON data from {inputFile}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing file {inputFile}: {ex.Message}");
        }
    }

    static string CleanJsonContent(string content)
    {
        // Remove merge conflict markers
        string pattern = @"<<<<<<< .*\n|=======\n|>>>>>>> .*\n";
        return Regex.Replace(content, pattern, "", RegexOptions.Multiline);
    }

    static bool TryParseJson(string content, out JArray jsonData)
    {
        jsonData = null;
        try
        {
            jsonData = JArray.Parse(content);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    static void TrimSequenceData(JArray jsonData)
    {
        foreach (var item in jsonData)
        {
            if (item["SequenceData"] != null)
            {
                var sequenceData = (JArray)item["SequenceData"];
                if (sequenceData.Count > 20)
                {
                    item["SequenceData"] = new JArray(sequenceData.Take(20));
                }
            }
        }
    }
}
