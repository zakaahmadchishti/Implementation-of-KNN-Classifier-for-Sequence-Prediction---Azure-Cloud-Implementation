using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class SequenceDataEntry
{
    public string SequenceName { get; set; }
    public List<int> SequenceData { get; set; }
}

public class Program
{
    public static void Main()
    {
        // Specify the directory containing your JSON files
        string directoryPath = "C:\\Users\\Lenovo\\Downloads\\TrainingData"; // Replace with your directory path

        ProcessJsonFiles(directoryPath);

        Console.WriteLine("Conversion completed for all files.");
    }

    private static void ProcessJsonFiles(string directoryPath)
    {
        var jsonFiles = Directory.GetFiles(directoryPath, "*.json");

        foreach (var filePath in jsonFiles)
        {
            try
            {
                Console.WriteLine($"Processing file: {filePath}");

                // Read the file content
                string fileContent = File.ReadAllText(filePath);

                // Initialize a list to hold the SequenceDataEntry objects
                var entries = new List<SequenceDataEntry>();

                // Split the file content by newlines and parse each JSON object
                var jsonObjects = fileContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var jsonObject in jsonObjects)
                {
                    try
                    {
                        var entry = JsonSerializer.Deserialize<SequenceDataEntry>(jsonObject.Trim());
                        if (entry != null)
                        {
                            entries.Add(entry);
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        Console.WriteLine($"JSON Parsing Error: {jsonEx.Message}");
                    }
                }

                // Serialize the list to a JSON array format
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonArrayContent = JsonSerializer.Serialize(entries, jsonOptions);

                // Write the formatted JSON array back to the file
                File.WriteAllText(filePath, jsonArrayContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {filePath}: {ex.Message}");
            }
        }
    }
}