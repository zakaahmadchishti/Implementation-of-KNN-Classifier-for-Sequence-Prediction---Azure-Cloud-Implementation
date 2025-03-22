using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Define the directory containing JSON files
        string directoryPath = @"C:\Users\Lenovo\Downloads\se-cloud-2023-2024\Myproject\KNN\dataset\trainingdatafolder";

        // Process each file in the directory
        foreach (string filePath in Directory.GetFiles(directoryPath, "*.json"))
        {
            ProcessFile(filePath);
        }

        Console.WriteLine("All files processed.");
    }

    static void ProcessFile(string filePath)
    {
        try
        {
            // Read the JSON file content
            string jsonContent = File.ReadAllText(filePath);

            // Parse the JSON content
            JArray jsonArray = JArray.Parse(jsonContent);

            // Iterate over each item in the JSON array
            foreach (JObject item in jsonArray.Children<JObject>())
            {
                JArray sequenceData = (JArray)item["SequenceData"];

                // Ensure the length of SequenceData is exactly 20
                if (sequenceData.Count > 20)
                {
                    // Truncate to 20 elements
                    sequenceData.RemoveAll();
                    foreach (var value in sequenceData.Take(20))
                    {
                        sequenceData.Add(value);
                    }
                }
                else if (sequenceData.Count < 20)
                {
                    // Add the last value to pad the array
                    var lastValue = sequenceData.LastOrDefault() ?? 0; // Default to 0 if no values exist
                    while (sequenceData.Count < 20)
                    {
                        sequenceData.Add(lastValue);
                    }
                }
            }

            // Serialize the modified JSON content back to the file
            File.WriteAllText(filePath, jsonArray.ToString(Formatting.Indented));

            Console.WriteLine($"Processed {Path.GetFileName(filePath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing file {Path.GetFileName(filePath)}: {ex.Message}");
        }
    }
}
