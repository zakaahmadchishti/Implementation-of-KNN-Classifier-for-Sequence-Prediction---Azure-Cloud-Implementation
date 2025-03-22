//Global Variable
using KNNImplementation;
using NeoCortexApi;
using NeoCortexApi.Encoders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using static NeoCortexApiSample.MultiSequenceLearning;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Identity;

using System.IO;


namespace NeoCortexApiSample
{
    class Program
    {

        // Main method to start the program
        static void Main(string[] args)
        {

            //RunMultiSequenceLearningExperiment();

            KNNClassificationExperiment();

        }
         

private static void INPUTS ()
{
    //all inputs and the connections will go there
        string Datasetfilepath = "/Users/zakaahmedchishti/Projects/New/New/Dataset_KNN.json";
        string Connectionstring="";



}





        /// <summary>
        /// Run a KNN Classifier Experiment on Simple sequence SDR's
        /// </summary>
        /// <param name="Datasetfilepath"></param>

        private static void KNNClassificationExperiment()
        {
                        string Datasetfilepath = "/Users/zakaahmedchishti/Projects/New/New/Dataset_KNN.json";

            Classifierleaning classifierleaning = new Classifierleaning();

            KNNClassifier kNNClassifier = new KNNClassifier();

            List<SequenceDataEntry> sequenceDataEntries = classifierleaning.LoadDataset(Datasetfilepath);

            Classifierleaning.SplitDataset(sequenceDataEntries, out List<List<double>> trainingFeatures, out List<string> trainingLabels, out List<List<double>> testingFeatures, out List<string> testingLabels, 0.7);

            List<string> predictedLabels = kNNClassifier.Classifier(testingFeatures, trainingFeatures, trainingLabels, k: 3);

            kNNClassifier.CalculateAccuracy(predictedLabels, testingLabels);

        }


      
        //Moving data into cloud    
        private static void Upload_TrainingFeatures()
        {
      

// TODO: Replace <storage-account-name> with your actual storage account name
var blobServiceClient = new BlobServiceClient(
        new Uri("https://<storage-account-name>.blob.core.windows.net"),
        new DefaultAzureCredential());

//Create a unique name for the container
string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

// Create the container and return a container client object
//BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

// Create a local file in the ./data/ directory for uploading and downloading
string localPath = "data";
Directory.CreateDirectory(localPath);
string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
string localFilePath = Path.Combine(localPath, fileName);

// Write text to the file
//await File.WriteAllTextAsync(localFilePath, "Hello, World!");

// Get a reference to a blob
//BlobClient blobClient = containerClient.GetBlobClient(fileName);

//Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

// Upload data from the local file
//await blobClient.UploadAsync(localFilePath, true);

Console.WriteLine("Listing blobs...");

// List all blobs in the container
await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
{
    Console.WriteLine("\t" + blobItem.Name);
}

// Download the blob to a local file
// Append the string "DOWNLOADED" before the .txt extension 
// so you can compare the files in the data directory
string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");

Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

// Download the blob's contents and save it to a file
await blobClient.DownloadToAsync(downloadFilePath);






        }






        /// <summary>
        /// Runs a multi-sequence learning experiment using simple sequences.
        /// </summary>
        private static void RunMultiSimpleSequenceLearningExperiment()
        {
            // Initialize a dictionary to store sequences, where each sequence is represented by a list of doubles.
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();

            // Define the first sequence (S1) with prime numbers: 2, 3, 7.
            sequences.Add("S1", new List<double>(new double[] { 2, 3, 7 }));

            // Define the second sequence (S2) with non-prime numbers: 10, 15, 21.
            sequences.Add("S2", new List<double>(new double[] { 10, 15, 21 }));

            // Initialize the multi-sequence learning experiment.
            MultiSequenceLearning experiment = new MultiSequenceLearning();

            // Run the experiment to build the prediction engine.
            var predictor = experiment.Run(sequences);
        }


        /// <summary>
        /// Runs a multi-sequence learning experiment using various types of sequences.
        /// </summary>
        private static void RunMultiSequenceLearningExperiment()
        {
            // Initialize a dictionary to store sequences, where each sequence is represented by a list of doubles.
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();

            // Define the first sequence (S1) with even numbers: 2, 4, 6, 8, 10, 12, 14.
            sequences.Add("S1", new List<double>(new double[] { 2, 4, 6, 8, 10, 12, 14 }));

            // Define the second sequence (S2) with even numbers: 2, 6, 12, 14.
            //sequences.Add("S2", new List<double>(new double[] { 2, 6, 12, 14 }));

            // Define the third sequence (S3) with odd numbers starting from 3: 3, 5, 7, 9, 11, 13, 15.
            sequences.Add("S3", new List<double>(new double[] { 3, 5, 7, 9, 11, 13, 15 }));

            // Define the fourth sequence (S4) with odd numbers: 3, 9, 13, 15.
            //sequences.Add("S4", new List<double>(new double[] { 3, 9, 13, 15 }));

            // Define the fifth sequence (S5) with numbers that are neither odd nor even: 4.5, 11.4, 12.8, 15.5, 16.6, 17.7.
            sequences.Add("S5", new List<double>(new double[] { 4.5, 11.4, 12.8, 15.5, 16.6, 17.7 }));

            // Define the sixth sequence (S6) with numbers that are neither odd nor even: 4.5, 11.4, 12.8, 16.6.
            //sequences.Add("S6", new List<double>(new double[] { 4.5, 11.4, 12.8, 16.6 }));

            // Initialize the multi-sequence learning experiment.
            MultiSequenceLearning experiment = new MultiSequenceLearning();

            // Run the experiment to build the prediction engine.
            var predictor = experiment.Run(sequences);
        }

        /// <summary>
        /// Predicts the next element in the sequence using the provided predictor.
        /// </summary>
        /// <param name="predictor">The predictor used for making predictions.</param>
        /// <param name="list">The list of elements for which predictions need to be made.</param>
        private static void PredictNextElement(Predictor predictor, double[] list)
        {
            // Output a separator for better readability in debug output.
            Debug.WriteLine("------------------------------");

            // Iterate through each element in the provided list.
            foreach (var item in list)
            {
                // Predict the next element based on the current item in the sequence.
                var res = predictor.Predict(item);

                // Check if predictions are available.
                if (res.Count > 0)
                {
                    // Output each prediction along with its similarity score.
                    foreach (var pred in res)
                    {
                        Debug.WriteLine($"{pred.PredictedInput} - {pred.Similarity}");
                    }

                    // Extract the predicted sequence and the next predicted element.
                    var tokens = res.First().PredictedInput.Split('_');
                    var tokens2 = res.First().PredictedInput.Split('-');

                    // Output the predicted sequence and the next predicted element.
                    Debug.WriteLine($"Predicted Sequence: {tokens[0]}, predicted next element {tokens2.Last()}");
                }
                else
                {
                    // Output a message if no predictions are available.
                    Debug.WriteLine("Nothing predicted :(");
                }
            }

            // Output a separator for better readability in debug output.
            Debug.WriteLine("------------------------------");
        }
    }
}









//TestingFeatures is our test Data
//TrainingFeatures is our training Data

//Training Labels
//Testing Labels
