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

namespace NeoCortexApiSample
{
    class Program
    {

        // Main method to start the program
        static void Main(string[] args)
        {
            while (true)
            { 
                RunMultiSequenceLearningExperiment();
            }
            //RunMultiSequenceLearningExperiment();

            // Start a experiment to demonstrate KNN classifier prediction sequence based on SDR's
            //KNNClassificationExperiment();
            



        }

        /// The KNN Classification experiment is written by Global Variable
        /// <summary>
        /// Run a KNN Classifier Experiment on Simple sequence SDR's
        /// </summary>
        private static void KNNClassificationExperiment()
        {
            // Define the file name and directory path for the dataset
            string datasetFileName = "Dataset_KNN.json";
            string datasetDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "DataSet");
            string datasetFilePath = Path.Combine(datasetDirectory, datasetFileName);

            // Instantiate the Classifierleaning and KNNClassifier classes
            Classifierleaning classifierleaning = new Classifierleaning();
            KNNClassifier kNNClassifier = new KNNClassifier();

            // Load the dataset from the specified file path
            List<SequenceDataEntry> sequenceDataEntries = classifierleaning.LoadDataset(datasetFilePath);

            // Split the dataset into training and testing sets
            Classifierleaning.SplitDataset(sequenceDataEntries, out List<List<double>> trainingFeatures, out List<string> trainingLabels, out List<List<double>> testingFeatures, out List<string> testingLabels, 0.7);

            // Perform classification on the testing set using the KNN algorithm
            List<string> predictedLabels = kNNClassifier.Classifier(testingFeatures, trainingFeatures, trainingLabels, k: 3);

            // Calculate the accuracy of the classifier
            kNNClassifier.CalculateAccuracy(predictedLabels, testingLabels);

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

        //// Global Variable just edited the sequences as per our requirements 
        /// <summary>
        /// Runs a multi-sequence learning experiment using various types of sequences.
        /// </summary>
        private static void RunMultiSequenceLearningExperiment()
        {
            // Initialize a dictionary to store sequences, where each sequence is represented by a list of doubles.
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();

            // Define the first sequence (S1) with even numbers: 2, 4, 6, 8, 10, 12, 14.
            sequences.Add("S1", new List<double>(new double[] { 2, 4, 6, 8, 10, 12, 14 }));

            // Define the second sequence (S2) with odd numbers starting from 3: 3, 5, 7, 9, 11, 13, 15.
            sequences.Add("S2", new List<double>(new double[] { 3, 5, 7, 9, 11, 13, 15 }));

            // Define the thirth sequence (S3) with numbers that are neither odd nor even: 4.5, 11.4, 12.8, 15.5, 16.6, 17.7.
            sequences.Add("S3", new List<double>(new double[] { 4.5, 11.4, 12.8, 15.5, 16.6, 17.7 }));

      

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