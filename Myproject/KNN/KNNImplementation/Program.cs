﻿//Global Variable
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

        public static async Task Main()
        {
            for (int i = 0; i < 1; i++)
            {
               await RunMultiSequenceLearningExperiment();
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
        private static async Task RunMultiSequenceLearningExperiment()
        {
            // Initialize a dictionary to store sequences, where each sequence is represented by a list of doubles.
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();
            

           


            /*
            sequences.Add("S1", new List<double>(new double[] { 30.6, 11.6, 13.7, 10.5, 36.2, 9.3, 8.1 }));
            sequences.Add("S2", new List<double>(new double[] { 23.4, 6.5, 47.2, 11.1, 28.3, 9.3, 39.1 }));
            sequences.Add("S3", new List<double>(new double[] { 13.4, 33.1, 6.9, 46.4, 46.9, 37.3, 2 })); 
            sequences.Add("S4", new List<double>(new double[] { 12.8, 41.2, 17.8, 28.7, 4.3, 12.3, 20.8 }));
            sequences.Add("S5", new List<double>(new double[] { 47, 0.9, 24.6, 27.1, 25.7, 29.4, 34.7 }));
            sequences.Add("S6", new List<double>(new double[] { 4.1, 24.1, 22.9, 8.7, 27.2, 44.6, 20.1 }));
            sequences.Add("S7", new List<double>(new double[] { 26.8, 12.7, 13.5, 3, 35.8, 12.3, 33 }));
            sequences.Add("S8", new List<double>(new double[] { 19.8, 35.1, 41.3, 21.5, 30.6, 32.9, 31.5 }));
            sequences.Add("S9", new List<double>(new double[] { 17.3, 0.8, 47, 16.5, 19.2, 4.2, 11.2 }));
            sequences.Add("S10", new List<double>(new double[] { 41.1, 44.8, 39.5, 26.1, 30.2, 10.6, 46.5 }));
            sequences.Add("S11", new List<double>(new double[] { 18.5, 16.1, 13.6, 23.7, 4.4, 39, 1 }));
            sequences.Add("S12", new List<double>(new double[] { 36.9, 35.5, 21.2, 14.9, 32.6, 28.7, 0.2 }));
            sequences.Add("S13", new List<double>(new double[] { 0.8, 16.4, 19.3, 45.1, 23.2, 7.7, 33.2 }));
            sequences.Add("S14", new List<double>(new double[] { 4.9, 9.5, 22.4, 16.7, 0.7, 32.9, 12.8 }));
            sequences.Add("S15", new List<double>(new double[] { 36.8, 16.9, 21.3, 26.6, 34.7, 33.5, 44.9 }));
            sequences.Add("S16", new List<double>(new double[] { 21.6, 38.8, 46.9, 33.9, 28.5, 0.4, 5.5 }));
            sequences.Add("S17", new List<double>(new double[] { 44.2, 37.5, 33.9, 6.4, 15.8, 17.1, 22.6 }));
            sequences.Add("S18", new List<double>(new double[] { 14.2, 21.7, 33.3, 25, 22.8, 7.5, 22.4 }));
            sequences.Add("S19", new List<double>(new double[] { 10.5, 31.9, 45.2, 39.4, 23.6, 10.9, 45.4 }));
            sequences.Add("S20", new List<double>(new double[] { 38.1, 14.1, 0.7, 32.8, 8.9, 44.3, 27.6 }));
            sequences.Add("S21", new List<double>(new double[] { 25, 2.4, 7, 33.5, 25.8, 40, 37.6 }));
            sequences.Add("S22", new List<double>(new double[] { 36.3, 15.1, 19.6, 14.3, 36.8, 6.9, 39.4 }));
            sequences.Add("S23", new List<double>(new double[] { 31.5, 29.6, 10.3, 4.3, 35.1, 47.2, 43.1 }));
            sequences.Add("S24", new List<double>(new double[] { 38.3, 5.7, 39.1, 31, 18.3, 10.8, 41 }));
            sequences.Add("S25", new List<double>(new double[] { 21.3, 13.2, 6.4, 12.5, 32.5, 17.1, 21.2 }));
            sequences.Add("S26", new List<double>(new double[] { 20.4, 30.9, 27.6, 26.2, 12.2, 28.1, 27.6 }));
            sequences.Add("S27", new List<double>(new double[] { 2.9, 17.6, 42.6, 47.3, 21.1, 28.7, 13.6 }));
            sequences.Add("S28", new List<double>(new double[] { 13.7, 1.6, 40.4, 11.6, 21.9, 27.7, 41.8 }));
            sequences.Add("S29", new List<double>(new double[] { 1.5, 13.4, 27.7, 32, 33.8, 22.1, 43.4 }));
            sequences.Add("S30", new List<double>(new double[] { 11.2, 29, 27.3, 2.9, 24.3, 4.5, 19.9 }));
            sequences.Add("S31", new List<double>(new double[] { 16.2, 14, 37.7, 5, 14.6, 34.7, 4.6 }));
            sequences.Add("S32", new List<double>(new double[] { 42.8, 16.2, 28.3, 13.4, 33.9, 10, 17 }));
            sequences.Add("S33", new List<double>(new double[] { 24.8, 16.8, 25.6, 9.8, 44.8, 2.5, 30 }));
            sequences.Add("S34", new List<double>(new double[] { 41.3, 36.8, 7.3, 23.3, 27.5, 44.4, 46 }));
            sequences.Add("S35", new List<double>(new double[] { 29.5, 26.1, 21.3, 13.3, 13.7, 39.5, 8.3 }));
            sequences.Add("S36", new List<double>(new double[] { 21.7, 39.9, 21.4, 11.6, 28.3, 16.2, 3.1 }));
            sequences.Add("S37", new List<double>(new double[] { 30.6, 24.2, 17, 40, 45.8, 1.9, 27 }));
            sequences.Add("S38", new List<double>(new double[] { 24.5, 6.9, 20.5, 45.2, 24.4, 11.8, 12.2 }));
            sequences.Add("S39", new List<double>(new double[] { 36.4, 43.5, 32.8, 14.4, 7.3, 25.7, 4.4 }));
            sequences.Add("S40", new List<double>(new double[] { 3.7, 22.6, 33.6, 38.1, 44.3, 4.3, 38.2 }));
            sequences.Add("S41", new List<double>(new double[] { 6.3, 19.8, 26.1, 11, 31.2, 9.8, 4.5 }));
            sequences.Add("S42", new List<double>(new double[] { 4.1, 22.6, 23.6, 10.7, 34.5, 41.1, 7.7 }));
            sequences.Add("S43", new List<double>(new double[] { 20.8, 27.1, 4, 10.1, 38.6, 31.8, 32 }));
            sequences.Add("S44", new List<double>(new double[] { 10.1, 16.6, 36.3, 15, 45.5, 37.4, 28.7 }));
                        

            sequences.Add("S45", new List<double>(new double[] { 31.8, 4.8, 36.4, 40.8, 32.3, 31.2, 13.1 }));
            sequences.Add("S46", new List<double>(new double[] { 45.7, 24.3, 8.8, 46.7, 3.6, 29.8, 35.3 }));
            sequences.Add("S47", new List<double>(new double[] { 17.2, 11.6, 3.4, 27.6, 29.7, 5.8, 3 }));
            sequences.Add("S48", new List<double>(new double[] { 11.7, 6, 1.9, 18.6, 2.2, 35.6, 17 }));
            sequences.Add("S49", new List<double>(new double[] { 24.9, 43.6, 19.4, 15.4, 17.8, 15.6, 25.3 }));
            sequences.Add("S50", new List<double>(new double[] { 12.2, 10.5, 11.1, 3.4, 17, 18.2, 5.5 }));
            sequences.Add("S51", new List<double>(new double[] { 34.3, 15, 26.9, 4.6, 40.2, 38.6, 42.2 }));
            sequences.Add("S52", new List<double>(new double[] { 35.5, 31.4, 43.4, 35.7, 24, 45.2, 31.9 }));
            sequences.Add("S53", new List<double>(new double[] { 31.8, 14.7, 29.1, 45.6, 16.1, 39.5, 7.1 }));
            sequences.Add("S54", new List<double>(new double[] { 45.8, 38.9, 39.6, 22.8, 42.8, 43.6, 41.5 }));
            sequences.Add("S55", new List<double>(new double[] { 31.2, 2.9, 8.2, 9, 46.2, 23, 44.7 }));
            sequences.Add("S56", new List<double>(new double[] { 6.6, 47.7, 40.5, 30.4, 44.1, 23.3, 46.5 }));
            sequences.Add("S57", new List<double>(new double[] { 39, 40.3, 14.2, 31.7, 36, 41.5, 46 }));
            sequences.Add("S58", new List<double>(new double[] { 3.1, 44.6, 47.1, 47.4, 24.8, 44.7, 3.1 }));
            sequences.Add("S59", new List<double>(new double[] { 32, 20.1, 44.4, 27.7, 43, 34.2, 3.7 }));
            sequences.Add("S60", new List<double>(new double[] { 44, 11.6, 18.7, 8.2, 47.2, 38.6, 33.5 }))
            sequences.Add("S61", new List<double>(new double[] { 35.5, 24.9, 3.7, 39, 29.3, 0.8, 33.3 }));
            sequences.Add("S62", new List<double>(new double[] { 42.9, 32.4, 38.4, 34.6, 3.7, 6.3, 31.8 }));
            sequences.Add("S63", new List<double>(new double[] { 38.4, 28.4, 18.9, 45.9, 2.5, 18.3, 16.1 }));
            sequences.Add("S64", new List<double>(new double[] { 7.1, 20.7, 6.1, 28.1, 27.8, 47.7, 31.5 }));
            sequences.Add("S65", new List<double>(new double[] { 22.9, 16.6, 6.2, 32.5, 32.7, 7, 21.2 }));
            sequences.Add("S66", new List<double>(new double[] { 10.6, 20.6, 0.2, 47.1, 44.1, 45.2, 6.1 }));
            sequences.Add("S67", new List<double>(new double[] { 27.6, 10.4, 24.9, 2.3, 36.4, 47.5, 2.3 }));
            sequences.Add("S68", new List<double>(new double[] { 20.1, 30.3, 40.9, 4.4, 6.9, 4.2, 24.3 }));
            sequences.Add("S69", new List<double>(new double[] { 30.3, 22, 34, 20.4, 37.2, 4.6, 22.4 }));
            sequences.Add("S70", new List<double>(new double[] { 1.6, 11.2, 8.9, 41.1, 18.4, 1.1, 38.9 }));
            sequences.Add("S71", new List<double>(new double[] { 42.8, 13.6, 25.5, 23.7, 42.5, 16.1, 31.9 }));
            sequences.Add("S72", new List<double>(new double[] { 24.5, 7.7, 3.6, 33.4, 36.9, 33.8, 34.8 }));
            sequences.Add("S73", new List<double>(new double[] { 16.1, 30.7, 36, 10.5, 34, 23.9, 4.1 }));
            sequences.Add("S74", new List<double>(new double[] { 8.3, 1.6, 2.5, 26.1, 46.6, 37.5, 32.2 }));
            sequences.Add("S75", new List<double>(new double[] { 32.2, 21.2, 20.2, 45.7, 3.7, 28.2, 34.2 }));
            sequences.Add("S76", new List<double>(new double[] { 41.9, 5.3, 14.2, 31.3, 9.3, 29.6, 12.4 }));
            sequences.Add("S77", new List<double>(new double[] { 16.7, 7.1, 31.8, 0.2, 27.6, 4, 23.5 }));
            sequences.Add("S79", new List<double>(new double[] { 37.2, 13, 41.1, 1.3, 11.3, 0.9, 29.3 }));
            sequences.Add("S80", new List<double>(new double[] { 30.1, 38.1, 7.6, 48, 35.1, 33.8, 5.1 }));
            sequences.Add("S81", new List<double>(new double[] { 22.6, 5.1, 40.1, 44.6, 9.3, 46.5, 38.1 }));
            sequences.Add("S82", new List<double>(new double[] { 4.8, 46.9, 0.6, 25.9, 22.2, 44.3, 20.2 }));
            sequences.Add("S83", new List<double>(new double[] { 5.7, 22.8, 33.7, 20.5, 45.7, 9.7, 1.6 }));
            sequences.Add("S84", new List<double>(new double[] { 13.7, 9.8, 30.8, 31.9, 32.9, 10.1, 9.3 }));
            sequences.Add("S85", new List<double>(new double[] { 41.8, 47.2, 39.6, 13.8, 38.6, 45.5, 7.5 }));
            sequences.Add("S86", new List<double>(new double[] { 26.8, 42.1, 42.9, 45.4, 6, 7.1, 38.6 }));
            sequences.Add("S87", new List<double>(new double[] { 28.4, 42.5, 19, 0.1, 6.4, 4.4, 25.6 }));
            sequences.Add("S88", new List<double>(new double[] { 43.7, 23.4, 45.4, 11.9, 18.7, 40.8, 15.3 }));
            sequences.Add("S89", new List<double>(new double[] { 21.8, 39.9, 5.4, 29, 22.2, 1.6, 39.8 }));
            sequences.Add("S90", new List<double>(new double[] { 1.9, 41.5, 13.2, 2.3, 21.4, 35.6, 15.5 }));
            sequences.Add("S91", new List<double>(new double[] { 27.5, 38.2, 6.9, 6.3, 31.8, 19.9, 41.8 }));
            sequences.Add("S92", new List<double>(new double[] { 31.7, 11.2, 33, 31.2, 16.2, 46.9, 19.8 }));
            sequences.Add("S93", new List<double>(new double[] { 46.9, 28.9, 40.4, 13.5, 45, 10, 18.8 }));
            sequences.Add("S94", new List<double>(new double[] { 14.7, 39.3, 2.1, 30.8, 14.8, 35.8, 16.7 }));
            sequences.Add("S95", new List<double>(new double[] { 39.5, 2, 40.9, 33.4, 15.8, 34.2, 13.9 }));
            sequences.Add("S96", new List<double>(new double[] { 32.8, 19.7, 18.3, 20.3, 40.9, 6.4, 41.2 }));
            sequences.Add("S97", new List<double>(new double[] { 19.7, 38, 40.6, 6, 20.2, 41, 4.6 }));
            sequences.Add("S98", new List<double>(new double[] { 31.3, 16.8, 20.1, 11.3, 12.4, 24.3, 35.2 }));
            sequences.Add("S99", new List<double>(new double[] { 41, 23.6, 26, 26.8, 16.4, 19.6, 8.1 }));
            sequences.Add("S100", new List<double>(new double[] { 6.8, 21.2, 10.2, 41.4, 44.2, 27.3, 10.1 }));
            sequences.Add("S101", new List<double>(new double[] { 33.4, 19, 29.6, 27.5, 37.7, 1.5, 5.5 }));
            sequences.Add("S102", new List<double>(new double[] { 10, 38.7, 13.9, 8, 39.4, 20.5, 22 }));
            sequences.Add("S103", new List<double>(new double[] { 40.3, 11.2, 21.3, 24.7, 25.3, 39.7, 21.1 }));
            sequences.Add("S104", new List<double>(new double[] { 42.1, 11.8, 19.5, 36.2, 17.1, 16.2, 14 }));
            sequences.Add("S105", new List<double>(new double[] { 6.8, 17, 7.6, 1.1, 22.3, 22.4, 20.3 }));
            sequences.Add("S106", new List<double>(new double[] { 31.2, 16.7, 19.8, 22.2, 41.5, 28.3, 34.7 }));
            sequences.Add("S107", new List<double>(new double[] { 0.3, 11.7, 38.8, 12.9, 23.6, 40, 3.6 }));
            sequences.Add("S108", new List<double>(new double[] { 44.5, 33.2, 10.4, 41.6, 27.6, 35, 22.4 }));
            sequences.Add("S109", new List<double>(new double[] { 11.7, 14.6, 10.7, 34.6, 20.3, 31.1, 29.5 }));
            sequences.Add("S110", new List<double>(new double[] { 4.9, 5.3, 43.3, 13.6, 45.3, 47.2, 46.6 }));
            sequences.Add("S111", new List<double>(new double[] { 40.2, 28.4, 14.9, 17.4, 5, 10.6, 22.5 }));
            sequences.Add("S112", new List<double>(new double[] { 5.6, 23.5, 15.9, 32.1, 42.1, 37.9, 27.1 }));
            sequences.Add("S113", new List<double>(new double[] { 19.3, 8.5, 17.8, 23.1, 17.1, 21.2, 10.6 }));
            sequences.Add("S114", new List<double>(new double[] { 44.8, 38.4, 45.5, 47.8, 0.6, 18.3, 46 });
            sequences.Add("S115", new List<double>(new double[] { 44.5, 14.1, 44.4, 30.3, 2.4, 47.9, 26.9 }));
            sequences.Add("S116", new List<double>(new double[] { 33.7, 3.3, 21.6, 19.7, 36.3, 29.8, 36.7 }));
            sequences.Add("S117", new List<double>(new double[] { 13, 9.9, 31.6, 11.2, 20.7, 21.6, 5.2 }));
            sequences.Add("S118", new List<double>(new double[] { 33.7, 19.7, 23.5, 0.9, 35, 28.2, 7.3 }));
            sequences.Add("S119", new List<double>(new double[] { 5.7, 18.4, 8.6, 39.6, 10.6, 4.9, 34.1 }));
            sequences.Add("S120", new List<double>(new double[] { 30.8, 31.5, 45.5, 5.5, 18.3, 44.9, 9.5 }));
            sequences.Add("S121", new List<double>(new double[] { 20, 48, 43.8, 39.7, 33.9, 30.7, 30.4 }));
            sequences.Add("S122", new List<double>(new double[] { 10.2, 38.1, 14.6, 4.6, 9.3, 45.5, 9.4 }));
            sequences.Add("S123", new List<double>(new double[] { 34.9, 39.2, 17.7, 37.6, 15.9, 23.7, 40.2 }));
            sequences.Add("S124", new List<double>(new double[] { 38.6, 19, 40.2, 5.2, 24.8, 17.6, 34.9 }));
            sequences.Add("S125", new List<double>(new double[] { 9.7, 13.2, 4.4, 27.5, 2.5, 31.3, 5.2 }));
            sequences.Add("S126", new List<double>(new double[] { 27.8, 9.1, 39, 6.3, 47.6, 13.2, 24.5 }));
            sequences.Add("S127", new List<double>(new double[] { 37.2, 23.6, 19, 16.1, 6, 37.8, 2.8 }));
            sequences.Add("S128", new List<double>(new double[] { 31.1, 14.8, 10.5, 20.3, 19.8, 13.8, 44.9 }));
            sequences.Add("S129", new List<double>(new double[] { 11.8, 40.8, 46.6, 2.6, 16.2, 32.7, 27.6 }));
            sequences.Add("S130", new List<double>(new double[] { 44.4, 30.2, 40.2, 22.8, 44.4, 8.1, 0.9 }));
            sequences.Add("S131", new List<double>(new double[] { 22.7, 22.3, 42.8, 7.8, 14.1, 45.6, 32.3 }));
            sequences.Add("S132", new List<double>(new double[] { 10.1, 44.8, 23.4, 1, 28.8, 8.7, 46.3 }));
            sequences.Add("S133", new List<double>(new double[] { 31.1, 18.7, 2.8, 27.3, 40.9, 13, 8.6 }));
            sequences.Add("S134", new List<double>(new double[] { 24.1, 1.6, 44.3, 22.2, 2.5, 22.3, 24.8 }));
            sequences.Add("S135", new List<double>(new double[] { 24.9, 25.4, 23.9, 46.2, 28.7, 18.7, 5.9 }));
            sequences.Add("S136", new List<double>(new double[] { 1.3, 35.3, 11.1, 45.3, 40.7, 16.8, 30.6 }));
            sequences.Add("S137", new List<double>(new double[] { 47.2, 22.9, 26.4, 34.8, 42.5, 2.2, 41.1 }));
            sequences.Add("S138", new List<double>(new double[] { 43.3, 47.1, 8.2, 6, 11.4, 31.3, 30.4 }));
            sequences.Add("S139", new List<double>(new double[] { 45.5, 44.1, 27.1, 34.7, 18.5, 13.8, 13.8 }));
            sequences.Add("S140", new List<double>(new double[] { 29.6, 13, 13.1, 17.4, 45.9, 8.3, 21.3 }));
            sequences.Add("S141", new List<double>(new double[] { 7, 35, 3.7, 24.9, 17, 8.3, 9.7 }));
            sequences.Add("S142", new List<double>(new double[] { 47.7, 34.6, 46.7, 12.1, 37.4, 47.2, 6.8 }));
            sequences.Add("S143", new List<double>(new double[] { 43.1, 16.5, 21.4, 21.8, 10.6, 30, 47.6 }));
            sequences.Add("S144", new List<double>(new double[] { 20.5, 22.5, 2.2, 30.9, 9.2, 19.7, 36.9 }));
            sequences.Add("S145", new List<double>(new double[] { 40.7, 5.6, 14.9, 27.1, 21.6, 21.4, 12.2 }));
            sequences.Add("S146", new List<double>(new double[] { 3.9, 25.4, 30.3, 27.9, 15.9, 12.9, 23.6 }));
            sequences.Add("S147", new List<double>(new double[] { 25.1, 24.1, 5.7, 24.1, 12.2, 23.9, 27.9 }));
            sequences.Add("S148", new List<double>(new double[] { 21.2, 28.9, 10.4, 46.6, 2.5, 10.1, 41.4 }));
            sequences.Add("S149", new List<double>(new double[] { 40.6, 33.3, 13.1, 47.3, 26.1, 30.6, 27.8 }));
            sequences.Add("S150", new List<double>(new double[] { 13.7, 28.9, 15.5, 18.9, 17.2, 15.9, 47.4 }));
            sequences.Add("S151", new List<double>(new double[] { 43.2, 45.5, 46.1, 0.2, 40.6, 3.8, 3.2 }));
            sequences.Add("S152", new List<double>(new double[] { 2, 8.6, 13.1, 27.3, 31.2, 7.1, 24.1 }));
            sequences.Add("S153", new List<double>(new double[] { 4.1, 21.1, 14.6, 14, 7.3, 27, 25.5 }));
            sequences.Add("S154", new List<double>(new double[] { 36.3, 37.5, 25.3, 13.2, 15.5, 42.3, 19.5 }));
            sequences.Add("S155", new List<double>(new double[] { 2.5, 7.8, 18.9, 30.2, 37.8, 30.8, 5.5 }));
            sequences.Add("S156", new List<double>(new double[] { 40.4, 37.1, 28.8, 41.6, 30.9, 0.8, 35.6 }));
            sequences.Add("S157", new List<double>(new double[] { 12.8, 8.7, 34.1, 2.4, 13.4, 17.7, 14.8 }));
            sequences.Add("S158", new List<double>(new double[] { 20.8, 25.6, 36, 44.9, 43.9, 44.3, 46.8 }));
            sequences.Add("S159", new List<double>(new double[] { 15.4, 28.9, 27.8, 47.1, 25, 22.9, 17.4 }));
            sequences.Add("S160", new List<double>(new double[] { 22.6, 37.6, 40.4, 29, 0.5, 47.5, 19 }));
            sequences.Add("S161", new List<double>(new double[] { 47.1, 42.6, 6.8, 31.3, 34.6, 4, 6.6 }));
            sequences.Add("S162", new List<double>(new double[] { 17.8, 20.3, 13.4, 8.5, 13.1, 37, 24.3 }));
            sequences.Add("S163", new List<double>(new double[] { 3.4, 23.9, 0.7, 0.3, 10, 28.2, 17.9 }));
            sequences.Add("S164", new List<double>(new double[] { 22.9, 46.3, 20.4, 0.9, 36.1, 34.2, 47.2 }));
            sequences.Add("S165", new List<double>(new double[] { 36.6, 21.9, 21, 43.9, 5.5, 7.9, 3.4 }));
            sequences.Add("S166", new List<double>(new double[] { 33.8, 14.3, 36, 43.9, 29.3, 35.3, 15 }));
            sequences.Add("S167", new List<double>(new double[] { 41.5, 7.3, 24.7, 27.9, 26.9, 19.4, 18.5 }));
            sequences.Add("S168", new List<double>(new double[] { 0.2, 34.6, 38.6, 6.4, 29.3, 0.6, 16 }));
            sequences.Add("S169", new List<double>(new double[] { 10.6, 47.9, 7.3, 17.2, 42.7, 3.4, 21.2 }));
            sequences.Add("S170", new List<double>(new double[] { 15.7, 28.3, 3, 37.1, 29.6, 26, 30.3 }));
            sequences.Add("S171", new List<double>(new double[] { 7.2, 16, 17.6, 0.3, 34, 4.4, 46.1 }));
            sequences.Add("S172", new List<double>(new double[] { 6.5, 25.8, 22.4, 13.3, 8.2, 33.4, 4.6 }));
            sequences.Add("S173", new List<double>(new double[] { 44.2, 45.8, 13.5, 3.1, 26.1, 21.1, 47.6 }));
            sequences.Add("S174", new List<double>(new double[] { 31.1, 25.6, 13.5, 34.3, 45.3, 34.5, 22.1 }));
            sequences.Add("S175", new List<double>(new double[] { 45.5, 7.6, 45.1, 40.3, 24, 26.8, 9.3 }));
            sequences.Add("S176", new List<double>(new double[] { 43.7, 27.7, 20.5, 28.4, 45.5, 0.1, 17.2 }));
            sequences.Add("S177", new List<double>(new double[] { 7.9, 24.6, 20.9, 32.1, 35.2, 24.5, 6.5 }));
            sequences.Add("S178", new List<double>(new double[] { 45.2, 10.1, 5.4, 16.7, 36, 24.1, 41.7 }));
            sequences.Add("S179", new List<double>(new double[] { 19.8, 8.2, 9.6, 11.2, 34.4, 13.6, 8.5 }));
            sequences.Add("S181", new List<double>(new double[] { 39.8, 27.5, 35.2, 2.4, 20.3, 34.2, 42.6 })); 
            sequences.Add("S180", new List<double>(new double[] { 43, 29.8, 39.9, 40.4, 10.4, 46.6, 16.1 }));
          
*/
           
/*
            sequences.Add("S200", new List<double>(new double[] { 9.5, 38.6, 24.3, 44.4, 0.2, 34.9, 36.6 }));
            sequences.Add("S201", new List<double>(new double[] { 2.8, 27.5, 44.2, 13.4, 35.6, 37.5, 14.9 }));
            sequences.Add("S202", new List<double>(new double[] { 34.4, 17.8, 43.9, 29.5, 40.9, 32.4, 29.2 }));
            sequences.Add("S203", new List<double>(new double[] { 0.3, 37.2, 0.6, 44, 28.4, 47.2, 41.3 }));
            sequences.Add("S204", new List<double>(new double[] { 29.9, 10.9, 43.7, 11.9, 13.4, 32.4, 13.3 }));
            sequences.Add("S205", new List<double>(new double[] { 28.3, 39.8, 42, 3.3, 36.6, 41.1, 4.2 }));
            sequences.Add("S206", new List<double>(new double[] { 45, 35.4, 26.8, 25.1, 18.7, 28.8, 36.3 }));
            sequences.Add("S207", new List<double>(new double[] { 26.7, 25.7, 5.4, 44.9, 31.5, 29.2, 8.8 }));
            sequences.Add("S208", new List<double>(new double[] { 20.2, 28.3, 24.8, 23.5, 31.3, 37.3, 41.3 }));
            sequences.Add("S209", new List<double>(new double[] { 47.1, 42.6, 6.8, 31.3, 34.6, 4, 6.6 }));
            sequences.Add("S210", new List<double>(new double[] { 17.8, 20.3, 13.4, 8.5, 13.1, 37, 24.3 }));
            sequences.Add("S211", new List<double>(new double[] { 3.4, 23.9, 0.7, 0.3, 10, 28.2, 17.9 }));
            sequences.Add("S212", new List<double>(new double[] { 22.9, 46.3, 20.4, 0.9, 36.1, 34.2, 47.2 }));
            sequences.Add("S213", new List<double>(new double[] { 36.6, 21.9, 21, 43.9, 5.5, 7.9, 3.4 }));
            sequences.Add("S214", new List<double>(new double[] { 40.6, 33.3, 13.1, 47.3, 26.1, 30.6, 27.8 }));
            sequences.Add("S215", new List<double>(new double[] { 13.7, 28.9, 15.5, 18.9, 17.2, 15.9, 47.4 }));
            sequences.Add("S216", new List<double>(new double[] { 43.2, 45.5, 46.1, 0.2, 40.6, 3.8, 3.2 }));
            sequences.Add("S217", new List<double>(new double[] { 2, 8.6, 13.1, 27.3, 31.2, 7.1, 24.1 }));
            sequences.Add("S218", new List<double>(new double[] { 4.1, 21.1, 14.6, 14, 7.3, 27, 25.5 }));
            sequences.Add("S219", new List<double>(new double[] { 36.3, 37.5, 25.3, 13.2, 15.5, 42.3, 19.5 }));
            sequences.Add("S220", new List<double>(new double[] { 2.5, 7.8, 18.9, 30.2, 37.8, 30.8, 5.5 }));
            sequences.Add("S221", new List<double>(new double[] { 40.4, 37.1, 28.8, 41.6, 30.9, 0.8, 35.6 }));
            sequences.Add("S222", new List<double>(new double[] { 12.8, 8.7, 34.1, 2.4, 13.4, 17.7, 14.8 }));
            sequences.Add("S223", new List<double>(new double[] { 45.5, 7.6, 45.1, 40.3, 24, 26.8, 9.3 }));
            sequences.Add("S224", new List<double>(new double[] { 21.6, 47.5, 41.2, 10, 29.6, 0.2, 2.4 }));
            sequences.Add("S225", new List<double>(new double[] { 40.1, 32.5, 34.9, 8.6, 13.9, 14.6, 43.2 }));
            sequences.Add("S226", new List<double>(new double[] { 0.7, 4.7, 12, 38.2, 23.1, 0, 47.7 }));
            sequences.Add("S227", new List<double>(new double[] { 21.1, 42.1, 2.5, 44.9, 27.3, 27.2, 5.3 }));
            sequences.Add("S228", new List<double>(new double[] { 20.6, 19.2, 34.8, 38.8, 45.3, 1.2, 46.3 }));
            sequences.Add("S229", new List<double>(new double[] { 16, 44.5, 34.2, 15.2, 22.2, 45.3, 21.2 }));
            sequences.Add("S230", new List<double>(new double[] { 9.1, 16.3, 36.7, 16.5, 3.1, 1.3, 1.6 }));
            sequences.Add("S231", new List<double>(new double[] { 5.7, 0.8, 45.4, 43, 32.9, 43.9, 21 }));
            sequences.Add("S232", new List<double>(new double[] { 12.1, 42.7, 40.8, 46.5, 10.8, 7.5, 31.9 }));
            sequences.Add("S233", new List<double>(new double[] { 34.1, 4.2, 9.4, 39.2, 21.9, 6.9, 16 }));
            sequences.Add("S234", new List<double>(new double[] { 46.2, 34.5, 24.7, 5.9, 23.2, 18.7, 8.1 }));
            sequences.Add("S235", new List<double>(new double[] { 1.7, 37.1, 45.8, 35.8, 33.4, 27.2, 24.3 }));
            sequences.Add("S236", new List<double>(new double[] { 43, 24, 12.6, 38.4, 44.9, 1.7, 36.8 }));
            sequences.Add("S237", new List<double>(new double[] { 4.3, 20.1, 36.5, 8.4, 41.4, 31.9, 25.7 }));
            sequences.Add("S238", new List<double>(new double[] { 32.3, 15.1, 23.3, 16.2, 12.3, 37.2, 3.6 }));
            sequences.Add("S239", new List<double>(new double[] { 6, 1.6, 16, 27.2, 42.5, 38.6, 19.7 }));
            sequences.Add("S240", new List<double>(new double[] { 3.6, 21.4, 28.6, 23.8, 20, 3.7, 8.3 }));
            sequences.Add("S241", new List<double>(new double[] { 18.8, 16.2, 19.2, 39.3, 9.9, 13.8, 40.8 }));
            sequences.Add("S242", new List<double>(new double[] { 38.9, 39.7, 24.5, 9.2, 25.2, 16.4, 27.3 }));
            sequences.Add("S243", new List<double>(new double[] { 9.7, 29.4, 10.4, 22.8, 14.9, 5.7, 33.1 }));
            sequences.Add("S244", new List<double>(new double[] { 42.5, 21.9, 9.9, 32.4, 3.8, 11.7, 25 }));
            sequences.Add("S245", new List<double>(new double[] { 9.9, 3.6, 6.6, 4.3, 29.5, 40, 13.4 }));
            sequences.Add("S246", new List<double>(new double[] { 27.7, 2.8, 47.8, 4.7, 43.7, 9.8, 42.5 }));
            sequences.Add("S247", new List<double>(new double[] { 11.9, 22.2, 0.6, 8.8, 0.7, 33.8, 26.5 }));
            sequences.Add("S248", new List<double>(new double[] { 26.3, 35.8, 23.8, 33.9, 15.7, 7.3, 27 }));
            sequences.Add("S249", new List<double>(new double[] { 19.7, 0.6, 47, 32.4, 40.9, 6.7, 42 }));
            sequences.Add("S250", new List<double>(new double[] { 17.3, 40.5, 28.5, 40.7, 12, 40.9, 13.5 }));
            sequences.Add("S251", new List<double>(new double[] { 29.9, 16.6, 12.1, 13.8, 29.4, 29, 35.8 }));
            sequences.Add("S252", new List<double>(new double[] { 11.2, 39.2, 30.5, 18.2, 31.9, 35.3, 41.2 }));
            sequences.Add("S253", new List<double>(new double[] { 36.1, 1.8, 14.9, 36.8, 35.6, 37.2, 45.9 }));
            sequences.Add("S254", new List<double>(new double[] { 29.9, 8.9, 47.6, 12.9, 16.2, 42.2, 2.5 }));
            sequences.Add("S255", new List<double>(new double[] { 24.5, 31, 5.1, 20.6, 26.8, 44.9, 7.5 }));
            sequences.Add("S256", new List<double>(new double[] { 1.2, 40.8, 22.1, 43, 27.6, 12.5, 16.7 }));
            sequences.Add("S257", new List<double>(new double[] { 4, 21.1, 33.9, 2.6, 12.6, 29, 27.8 }));
            sequences.Add("S258", new List<double>(new double[] { 24.1, 26.8, 38.2, 37.7, 48, 42.7, 1.7 }));
            sequences.Add("S259", new List<double>(new double[] { 30.7, 29.3, 18.1, 25.5, 24.1, 40.7, 38.6 }));
            sequences.Add("S260", new List<double>(new double[] { 9.1, 34.9, 39.6, 12.2, 34.4, 13, 18.6 }));
            sequences.Add("S261", new List<double>(new double[] { 18.9, 8.1, 41.1, 14.5, 28.4, 31.6, 16.5 }));
            sequences.Add("S262", new List<double>(new double[] { 19.4, 29.5, 44.2, 41.8, 24.3, 27.1, 18.5 }));
            sequences.Add("S263", new List<double>(new double[] { 39.3, 4.3, 40.4, 0.2, 22, 41.1, 46.7 }));
            sequences.Add("S264", new List<double>(new double[] { 11.6, 2.3, 38.7, 45.3, 28.8, 1.8, 6.6 }));
            sequences.Add("S265", new List<double>(new double[] { 9.4, 4.6, 45, 7.6, 43.4, 37.5, 41.7 }));
            sequences.Add("S266", new List<double>(new double[] { 45.1, 36, 40.7, 4.3, 28.8, 14, 6.3 }));
            sequences.Add("S267", new List<double>(new double[] { 29.3, 8.2, 8.2, 13.8, 34.6, 25.7, 13.2 }));
            sequences.Add("S268", new List<double>(new double[] { 38.4, 46, 32.4, 0.4, 28.1, 32.7, 37.5 }));
            sequences.Add("S269", new List<double>(new double[] { 36.5, 30.8, 7.5, 5.4, 36.3, 19.2, 15.4 }));
            sequences.Add("S270", new List<double>(new double[] { 31.9, 31.5, 21.7, 20.5, 2, 24.4, 7.8 }));
            sequences.Add("S271", new List<double>(new double[] { 28.9, 8.5, 24.7, 44, 19.8, 38.4, 30.8 }));
            sequences.Add("S272", new List<double>(new double[] { 26.6, 14.7, 7.7, 10.5, 26.7, 22.2, 42.9 }));
            sequences.Add("S273", new List<double>(new double[] { 40.3, 22.4, 40.2, 38.7, 39.8, 31, 10.1 }));
            sequences.Add("S274", new List<double>(new double[] { 8.4, 27.8, 18.2, 22.4, 35.3, 3.3, 15.5 }));
            sequences.Add("S275", new List<double>(new double[] { 9.6, 8.1, 10.4, 11.4, 24.5, 44.7, 35.2 }));
            sequences.Add("S276", new List<double>(new double[] { 18.1, 33.5, 7.7, 12.1, 7.2, 36.7, 6.2 }));
            sequences.Add("S277", new List<double>(new double[] { 1.7, 24.7, 14.9, 2.2, 42.2, 33.6, 16.3 }));
            sequences.Add("S278", new List<double>(new double[] { 16.3, 21.8, 36.1, 43.5, 10, 23.7, 40.1 }));
            sequences.Add("S279", new List<double>(new double[] { 38.5, 19.9, 4.5, 19.2, 2.4, 39.4, 33.9 }));
            sequences.Add("S280", new List<double>(new double[] { 6.1, 31.7, 3.7, 41.3, 23.8, 16.2, 19.5 }));
            sequences.Add("S281", new List<double>(new double[] { 45.4, 18.1, 38.7, 26, 44.2, 7.4, 7 }));
            sequences.Add("S282", new List<double>(new double[] { 27.6, 44.2, 7.1, 19.8, 44.8, 21.4, 2.8 }));
            sequences.Add("S283", new List<double>(new double[] { 12.9, 33.1, 2.3, 14.7, 37.3, 30.4, 27.9 }));
            sequences.Add("S284", new List<double>(new double[] { 2.5, 23.1, 35.7, 27.5, 42.7, 6.8, 21.7 }));
            sequences.Add("S285", new List<double>(new double[] { 38.3, 20.8, 29.5, 39.1, 34.6, 9.7, 43.3 }));
            sequences.Add("S286", new List<double>(new double[] { 41.2, 31.2, 37.3, 22.9, 23, 36.2, 5.2 }));
            sequences.Add("S287", new List<double>(new double[] { 39.7, 20, 14.2, 33.3, 42.6, 8, 38 }));
            sequences.Add("S288", new List<double>(new double[] { 38.2, 19.5, 24.4, 41.1, 6.2, 38.4, 47 }));
            sequences.Add("S289", new List<double>(new double[] { 34.8, 36.3, 24.9, 12.9, 28.2, 45, 37.2 }));
            sequences.Add("S290", new List<double>(new double[] { 36.9, 26.2, 46.5, 37.1, 12.3, 40.5, 17.2 }));
            sequences.Add("S291", new List<double>(new double[] { 4, 23.4, 8.9, 41.6, 36.1, 14.1, 46.2 }));
            sequences.Add("S292", new List<double>(new double[] { 16.4, 5.3, 38.9, 40.6, 3.6, 12.9, 35 }));
            sequences.Add("S293", new List<double>(new double[] { 11.2, 15.2, 46.2, 35.9, 29.7, 36.9, 23.4 }));
            sequences.Add("S294", new List<double>(new double[] { 9.8, 17.2, 28, 23, 33.2, 29.5, 46.6 }));
            sequences.Add("S295", new List<double>(new double[] { 26.7, 16.4, 14.7, 8.7, 1.5, 19.5, 40.9 }));
            sequences.Add("S296", new List<double>(new double[] { 32.4, 40.6, 0.4, 20.7, 24.5, 2.8, 8.8 }));
            sequences.Add("S297", new List<double>(new double[] { 22.9, 45.5, 39.9, 26.5, 47.8, 16, 35.2 }));
            sequences.Add("S298", new List<double>(new double[] { 18.9, 25.9, 21.4, 35.9, 41.2, 31.3, 26.7 }));
            sequences.Add("S299", new List<double>(new double[] { 6.6, 37.3, 0.3, 24.2, 19.8, 44.7, 33.8 }));
            sequences.Add("S300", new List<double>(new double[] { 40.8, 12.1, 23.7, 19.6, 26.3, 18, 46.5 }));
            sequences.Add("S301", new List<double>(new double[] { 47.1, 25.7, 4.2, 44.3, 24.3, 32.6, 35.3 }));
            sequences.Add("S302", new List<double>(new double[] { 3.8, 32.5, 16.8, 27.7, 24.5, 41.1, 23.1 }));
            sequences.Add("S303", new List<double>(new double[] { 46.7, 40.8, 26, 23.8, 40.2, 31, 7.4 }));
            sequences.Add("S304", new List<double>(new double[] { 44.3, 18.1, 23.6, 25.3, 42.7, 25, 23.9 }));
       
            sequences.Add("S305", new List<double>(new double[] { 28.5, 9.6, 13.5, 36.2, 27, 33.5, 21.7 }));
            sequences.Add("S306", new List<double>(new double[] { 37.4, 13.3, 1.7, 25.4, 27.3, 1.2, 22 }));
            sequences.Add("S307", new List<double>(new double[] { 29.6, 20.1, 18.9, 30.6, 41.7, 4.4, 7.9 }));
            sequences.Add("S308", new List<double>(new double[] { 4.6, 2.1, 34.4, 37.8, 30, 13.2, 18.4 }));
            sequences.Add("S309", new List<double>(new double[] { 41.4, 19.5, 34.5, 40.3, 15.9, 10.5, 42.1 }));
            sequences.Add("S310", new List<double>(new double[] { 10.4, 14, 1.7, 15.2, 0.8, 21.8, 36.7 }));
            sequences.Add("S311", new List<double>(new double[] { 23.1, 9.2, 7.8, 38.1, 22.6, 10.1, 34.8 }));
            sequences.Add("S312", new List<double>(new double[] { 47.6, 37.4, 28.9, 11.9, 44.7, 33.3, 2.5 }));
            sequences.Add("S313", new List<double>(new double[] { 18, 41.3, 46.1, 36.7, 46.2, 19.2, 15.4 }));
            sequences.Add("S314", new List<double>(new double[] { 43.1, 39.9, 46.9, 20.1, 18.3, 30.2, 11.7 }));
            sequences.Add("S315", new List<double>(new double[] { 0.4, 1.3, 34.2, 9.9, 6.5, 14.2, 32.5 }));
            sequences.Add("S316", new List<double>(new double[] { 11.4, 10.6, 37.8, 37.6, 12.5, 37.1, 27.4 }));
            sequences.Add("S317", new List<double>(new double[] { 13.4, 45.6, 36.7, 21.4, 16.4, 23.3, 30.6 }));
            sequences.Add("S318", new List<double>(new double[] { 1.9, 43.1, 39.1, 28.7, 39.4, 4.4, 28.4 }));
            sequences.Add("S319", new List<double>(new double[] { 27.1, 4.1, 2.8, 0.6, 34.2, 34.5, 7.7 }));
            sequences.Add("S320", new List<double>(new double[] { 3.4, 23.1, 25.3, 0.4, 3.7, 36.9, 25.9 }));
            sequences.Add("S321", new List<double>(new double[] { 7.2, 11.1, 25.6, 15.4, 34.3, 34.7, 24.9 }));
            sequences.Add("S322", new List<double>(new double[] { 19.1, 1.6, 41.7, 39.9, 41.5, 3.3, 22.7 }));
            sequences.Add("S323", new List<double>(new double[] { 14.5, 1.8, 45.5, 11.4, 28.6, 1.6, 11.1 }));
            sequences.Add("S324", new List<double>(new double[] { 9.4, 4.6, 26.5, 40.9, 28.9, 37.8, 40.2 }));
            sequences.Add("S325", new List<double>(new double[] { 25.3, 38.4, 22.7, 26.6, 41.4, 9.2, 9.7 }));
            sequences.Add("S326", new List<double>(new double[] { 36.1, 17.3, 16.4, 45.2, 12.6, 47.6, 15.9 }));
            sequences.Add("S327", new List<double>(new double[] { 13.1, 5.3, 6.5, 35.1, 37.3, 31.9, 37.1 }));
            sequences.Add("S328", new List<double>(new double[] { 13.6, 18.9, 7.7, 26.5, 11.4, 15.4, 29.2 }));
            sequences.Add("S329", new List<double>(new double[] { 2.1, 2.7, 5.3, 41.6, 4.5, 42.8, 6.6 }));
            sequences.Add("S330", new List<double>(new double[] { 4.6, 33.5, 16.8, 44.2, 31, 26, 7.8 }));
            sequences.Add("S331", new List<double>(new double[] { 37.1, 13.2, 14.4, 20.9, 17.2, 18.2, 9.4 }));
            sequences.Add("S332", new List<double>(new double[] { 19.6, 15.8, 7.5, 24.9, 28, 22.8, 42 }));
            sequences.Add("S333", new List<double>(new double[] { 36, 19.2, 16.6, 42.8, 30.6, 5.1, 23.6 }));
            sequences.Add("S334", new List<double>(new double[] { 39.6, 36, 45.9, 16.5, 27.2, 14.4, 3.7 }));
            sequences.Add("S335", new List<double>(new double[] { 46.8, 41.8, 9, 28.2, 9.8, 40.5, 32.9 }));
            sequences.Add("S336", new List<double>(new double[] { 14.2, 27, 32.8, 36.1, 39.9, 43.4, 31.3 }));
            sequences.Add("S337", new List<double>(new double[] { 12.7, 46.5, 46.5, 12.2, 31.2, 22.8, 2.4 }));
            sequences.Add("S338", new List<double>(new double[] { 45.1, 21.5, 23.9, 8.3, 7.3, 5.8, 13.1 }));
            sequences.Add("S339", new List<double>(new double[] { 37.2, 31.9, 16.7, 6.9, 11.9, 38.6, 32.1 }));
            sequences.Add("S340", new List<double>(new double[] { 0.2, 45.4, 12.3, 18.1, 28.7, 9.5, 19.2 }));
            sequences.Add("S341", new List<double>(new double[] { 4.4, 9, 12.3, 46.3, 35.2, 7.2, 30.2 }));
            sequences.Add("S342", new List<double>(new double[] { 9, 26.4, 23.8, 33.2, 27, 22.6, 14.5 }));
            sequences.Add("S343", new List<double>(new double[] { 44.7, 41, 47.5, 36, 41.4, 15.8, 35.6 }));
            sequences.Add("S344", new List<double>(new double[] { 1.6, 13.9, 30.3, 19.3, 14.3, 9.2, 34.2 }));
            sequences.Add("S345", new List<double>(new double[] { 29.6, 22.7, 23.1, 15.9, 15.4, 4, 23.8 }));
            sequences.Add("S346", new List<double>(new double[] { 22.7, 44.8, 33.1, 1.5, 30.9, 16.9, 24.5 }));
            sequences.Add("S347", new List<double>(new double[] { 41.2, 41.5, 15.9, 6.1, 3.1, 6.1, 20.2 }));
            sequences.Add("S348", new List<double>(new double[] { 16, 36.2, 21.5, 4.5, 37.8, 8, 18.9 }));
            sequences.Add("S349", new List<double>(new double[] { 16.6, 16.8, 38, 13.4, 23.9, 1.1, 22.7 }));
            sequences.Add("S350", new List<double>(new double[] { 19.4, 39.5, 2.4, 38.7, 21.6, 24.9, 22.8 }));
            sequences.Add("S351", new List<double>(new double[] { 31.3, 3.2, 8.7, 39, 3.9, 20, 1.7 }));
            sequences.Add("S352", new List<double>(new double[] { 26.2, 14.2, 23.5, 6.1, 28.4, 41.6, 33.4 }));
            sequences.Add("S353", new List<double>(new double[] { 41.2, 6, 35.5, 15.2, 11.9, 13.3, 26.5 }));
            sequences.Add("S354", new List<double>(new double[] { 38.8, 38.4, 32.9, 10.8, 28, 29.3, 45 }));
            sequences.Add("S355", new List<double>(new double[] { 26.6, 28.9, 43, 32.6, 10.2, 27.1, 48 }));
            sequences.Add("S356", new List<double>(new double[] { 17.5, 40.8, 26.2, 36.7, 13.4, 13.9, 9.8 }));
            sequences.Add("S357", new List<double>(new double[] { 39.4, 33.3, 42.5, 9.4, 35, 3.8, 17.7 }));
            sequences.Add("S358", new List<double>(new double[] { 13.9, 46, 28.8, 9.5, 38.1, 16.8, 35.1 }));
            sequences.Add("S359", new List<double>(new double[] { 18.6, 32.9, 0.8, 14.5, 29.7, 40.1, 23.4 }));
            sequences.Add("S360", new List<double>(new double[] { 7.3, 32.6, 35.4, 39.8, 27.1, 0.3, 17.5 }));
            sequences.Add("S361", new List<double>(new double[] { 1.2, 47.5, 26, 8.1, 36.9, 10.7, 43.2 }));
            sequences.Add("S362", new List<double>(new double[] { 26.1, 1.3, 21.3, 5.3, 10.7, 30.9, 9.9 }));
            sequences.Add("S363", new List<double>(new double[] { 40.2, 45.9, 2.2, 2.3, 26.4, 10.5, 43.7 }));
            sequences.Add("S364", new List<double>(new double[] { 12.7, 1.3, 32.5, 34.2, 7.9, 13.6, 3.7 }));
            sequences.Add("S365", new List<double>(new double[] { 40.5, 21.4, 36.2, 31.5, 2.3, 39.1, 23.4 }));
            sequences.Add("S366", new List<double>(new double[] { 45.5, 39.4, 11.2, 27.7, 40.8, 29.2, 24 }));
            sequences.Add("S367", new List<double>(new double[] { 20.3, 47.2, 0.4, 17.9, 47.9, 22, 26.8 }));
            sequences.Add("S368", new List<double>(new double[] { 1.5, 40.1, 19, 30.5, 6.9, 47.5, 30.9 }));
            sequences.Add("S369", new List<double>(new double[] { 14.5, 39.5, 46.2, 3.9, 23.6, 38.2, 2.9 }));
            sequences.Add("S370", new List<double>(new double[] { 7.2, 30.7, 6, 32.4, 15.9, 47, 7.4 }));
            sequences.Add("S371", new List<double>(new double[] { 5.4, 47, 2.6, 43, 16.8, 14, 30.4 }));
            sequences.Add("S372", new List<double>(new double[] { 18.9, 20.1, 26.4, 25.7, 26.7, 4.6, 1.5 }));
            sequences.Add("S373", new List<double>(new double[] { 3.4, 2.8, 10.6, 5.2, 2.1, 36.4, 4 }));
            sequences.Add("S374", new List<double>(new double[] { 1.5, 43.8, 12.7, 2.6, 5.8, 20.1, 29 }));
            sequences.Add("S375", new List<double>(new double[] { 22.3, 36.1, 26.4, 10.2, 2, 39.6, 32.1 }));
            sequences.Add("S376", new List<double>(new double[] { 45.4, 46.4, 24.6, 35.8, 35.1, 1.3, 23.6 }));
            sequences.Add("S377", new List<double>(new double[] { 39.4, 39.4, 35, 2.3, 29, 24.6, 22.3 }));
            sequences.Add("S378", new List<double>(new double[] { 2, 2.4, 39.9, 1.2, 34.2, 5.1, 34.4 }));
            sequences.Add("S379", new List<double>(new double[] { 7.7, 45.4, 8.5, 41.6, 28.9, 25.1, 47.3 }));
            sequences.Add("S380", new List<double>(new double[] { 45.8, 25.7, 13.7, 38.7, 5.3, 15.5, 40.7 }));
            sequences.Add("S381", new List<double>(new double[] { 35.5, 40.8, 0.1, 45.6, 47.1, 1.8, 30.4 }));
            sequences.Add("S382", new List<double>(new double[] { 22, 40.6, 28.8, 47.9, 8.1, 45.8, 18 }));
            sequences.Add("S383", new List<double>(new double[] { 33.5, 2.3, 28.9, 36.7, 33.5, 25.9, 0.2 }));
            sequences.Add("S384", new List<double>(new double[] { 26.2, 11.4, 36.8, 27.8, 14.4, 15.9, 44.2 }));
            sequences.Add("S385", new List<double>(new double[] { 24.8, 0.2, 0.4, 30.5, 20.8, 19.8, 17.8 }));
            sequences.Add("S386", new List<double>(new double[] { 16.6, 37.1, 23.8, 13.4, 22.1, 28.9, 1.8 }));
            sequences.Add("S387", new List<double>(new double[] { 34.5, 36, 2.7, 9.2, 8.6, 2.6, 16 }));
            sequences.Add("S388", new List<double>(new double[] { 46, 31.2, 23.7, 7.5, 19.3, 32.7, 28.5 }));
            sequences.Add("S389", new List<double>(new double[] { 32.7, 38.7, 43.9, 31.8, 30.5, 3.2, 19.7 }));
            sequences.Add("S390", new List<double>(new double[] { 36.8, 43, 16.7, 29.2, 5.2, 15.4, 27.2 }));
            sequences.Add("S391", new List<double>(new double[] { 17.7, 27.8, 40.2, 13.4, 16.6, 31.6, 7.7 }));
            sequences.Add("S392", new List<double>(new double[] { 6.1, 35.3, 15.5, 27.4, 41.3, 31.3, 17.5 }));
            sequences.Add("S393", new List<double>(new double[] { 4.5, 28.6, 40.3, 42.9, 10.2, 39.2, 0.1 }));
            sequences.Add("S394", new List<double>(new double[] { 34.2, 30.1, 21.3, 16.5, 20.9, 32.8, 42.1 }));
            sequences.Add("S395", new List<double>(new double[] { 8.1, 7, 29.8, 23, 29.2, 6.4, 15.9 }));
            sequences.Add("S396", new List<double>(new double[] { 43.7, 34.6, 19.2, 36.1, 13.1, 1.7, 14.3 }));
            sequences.Add("S397", new List<double>(new double[] { 29.4, 47.6, 41.2, 22.5, 32.6, 40.2, 11.3 }));
            sequences.Add("S398", new List<double>(new double[] { 10.2, 20.5, 45.4, 26.2, 5.1, 10.5, 36.8 }));
            sequences.Add("S399", new List<double>(new double[] { 12.1, 38.7, 17.4, 34, 38.5, 45, 11.3 }));
            
            sequences.Add("S400", new List<double>(new double[] { 44.2, 17.5, 21.7, 42.3, 40.2, 24, 40.5 }));
            sequences.Add("S401", new List<double>(new double[] { 42.6, 11.1, 39.2, 15.3, 43.4, 22.9, 31.9 }));
            sequences.Add("S402", new List<double>(new double[] { 4.8, 7.9, 13.3, 29, 41.6, 43.8, 21.3 }));
            sequences.Add("S403", new List<double>(new double[] { 11.7, 16.8, 13.6, 4.1, 22, 29.6, 3.4 }));
            sequences.Add("S404", new List<double>(new double[] { 16.4, 13.4, 38.4, 33.3, 30.4, 45.7, 43.9 }));
            sequences.Add("S405", new List<double>(new double[] { 12.9, 35.2, 30.5, 21, 42, 28.6, 22.7 }));
            sequences.Add("S406", new List<double>(new double[] { 23.4, 19.7, 11.2, 44.7, 6.8, 6.8, 40.7 }));
            sequences.Add("S407", new List<double>(new double[] { 10, 44.4, 33.7, 4.7, 25.4, 15.1, 24 }));
            sequences.Add("S408", new List<double>(new double[] { 10, 7.3, 23.5, 46.6, 39.2, 9.9, 40.4 }));
            sequences.Add("S409", new List<double>(new double[] { 23.8, 8.6, 18.9, 11.3, 0.9, 8.2, 17.8 }));
            sequences.Add("S410", new List<double>(new double[] { 18.6, 27.1, 45.4, 43.5, 23.6, 30.8, 15.5 }));
            sequences.Add("S411", new List<double>(new double[] { 46.3, 22, 25.8, 3.1, 15.2, 1.2, 29.4 }));
            sequences.Add("S412", new List<double>(new double[] { 32.7, 20, 47.2, 8.7, 7.4, 19.6, 12.5 }));
            sequences.Add("S413", new List<double>(new double[] { 38.2, 11, 2.9, 41.4, 22, 0.4, 22.5 }));
            sequences.Add("S414", new List<double>(new double[] { 45, 13.9, 1.6, 4.8, 1.7, 31, 17.8 }));
            sequences.Add("S415", new List<double>(new double[] { 25.9, 28.8, 0.4, 11.6, 13.6, 23.2, 9.7 }));
            sequences.Add("S416", new List<double>(new double[] { 42.2, 39.6, 43.8, 5.4, 41, 10.6, 8.7 }));
            sequences.Add("S417", new List<double>(new double[] { 7.1, 8.8, 46.8, 12.1, 47, 35.6, 29.9 }));
            sequences.Add("S418", new List<double>(new double[] { 6.3, 47.1, 34.3, 20.3, 44.5, 34.8, 17.4 }));
            sequences.Add("S419", new List<double>(new double[] { 1.9, 13.5, 38.7, 14.9, 3.2, 2.5, 41.6 }));
            sequences.Add("S420", new List<double>(new double[] { 45.5, 40.8, 31, 0.7, 10.5, 30.7, 21 }));
            sequences.Add("S421", new List<double>(new double[] { 2.9, 4.4, 45.2, 34.2, 22.3, 5.2, 19.3 }));
            sequences.Add("S422", new List<double>(new double[] { 4.9, 24.7, 20.1, 8.4, 45, 22.6, 30.4 }));
            sequences.Add("S423", new List<double>(new double[] { 38.7, 10.1, 39.7, 28.9, 7.6, 46.4, 7.7 }));
            sequences.Add("S424", new List<double>(new double[] { 33.7, 29.8, 23.8, 4.5, 30.3, 18.7, 31.5 }));
            sequences.Add("S425", new List<double>(new double[] { 12.3, 27.5, 21.8, 26.7, 6.5, 14.4, 42.8 }));
            sequences.Add("S426", new List<double>(new double[] { 44, 8.5, 7.7, 29, 40.9, 30.7, 15.8 }));
            sequences.Add("S427", new List<double>(new double[] { 6, 35.4, 39, 19.1, 26.6, 24.9, 24.3 }));
            sequences.Add("S428", new List<double>(new double[] { 37.5, 23, 45.1, 18.5, 46.6, 14.5, 1.6 }));
            sequences.Add("S429", new List<double>(new double[] { 0.5, 16.4, 31.8, 1.6, 40.3, 11.3, 25 }));
            sequences.Add("S430", new List<double>(new double[] { 34.8, 14.4, 37.6, 23, 39.4, 16.4, 40.6 }));
            sequences.Add("S431", new List<double>(new double[] { 8.5, 6.9, 21.5, 23.3, 0.9, 41.9, 47.3 }));
            sequences.Add("S432", new List<double>(new double[] { 20.6, 13.2, 40.8, 6.2, 47.7, 29.7, 34.1 }));
            sequences.Add("S433", new List<double>(new double[] { 43.6, 28.1, 46.6, 12.6, 19.3, 33.8, 10 }));
            sequences.Add("S434", new List<double>(new double[] { 29.5, 35.6, 31.8, 39.8, 15.5, 21.7, 8.3 }));
            sequences.Add("S435", new List<double>(new double[] { 5.7, 15.3, 28.5, 4.7, 39.5, 44.2, 42.4 }));
            sequences.Add("S436", new List<double>(new double[] { 8.4, 19.4, 34.1, 10.6, 36.7, 38.2, 27.9 }));
            sequences.Add("S437", new List<double>(new double[] { 15.1, 40.9, 15.6, 29.1, 14.2, 1.2, 41.3 }));
            sequences.Add("S438", new List<double>(new double[] { 37.5, 30.7, 42.3, 19.1, 25.1, 44.9, 47.4 }));
            sequences.Add("S439", new List<double>(new double[] { 5.6, 14.5, 0.8, 43.4, 30.8, 36.6, 27.9 }));
            sequences.Add("S440", new List<double>(new double[] { 7.9, 37.8, 3.2, 11.3, 46.3, 30.6, 7.6 }));
            sequences.Add("S441", new List<double>(new double[] { 31, 1.3, 42, 7.5, 35.3, 25, 16.9 }));
            sequences.Add("S442", new List<double>(new double[] { 14.6, 2.2, 8, 14.6, 25.2, 14, 2.1 }));
            sequences.Add("S443", new List<double>(new double[] { 33.9, 10.4, 21.2, 3, 18.7, 0.2, 25.7 }));
            sequences.Add("S444", new List<double>(new double[] { 23.1, 23.9, 28.2, 35.8, 38.4, 27.1, 21.7 }));
            sequences.Add("S445", new List<double>(new double[] { 6.9, 20.6, 28.7, 44.9, 15.8, 23, 24.7 }));
            sequences.Add("S446", new List<double>(new double[] { 34.4, 38, 2.1, 37, 4.8, 27.7, 5.9 }));
            sequences.Add("S447", new List<double>(new double[] { 5, 10, 34.6, 14, 4.3, 45.5, 8.3 }));
            sequences.Add("S448", new List<double>(new double[] { 32, 4.1, 8.2, 46.8, 41.8, 27.6, 16.2 }));
            sequences.Add("S449", new List<double>(new double[] { 13.6, 43.9, 21.9, 0.9, 36.9, 4.7, 45.5 }));
            sequences.Add("S450", new List<double>(new double[] { 10.7, 47.8, 19.5, 0.7, 16.9, 2.1, 4.7 }));
            sequences.Add("S451", new List<double>(new double[] { 38.3, 22.7, 25, 32.9, 3.2, 39.2, 18.7 }));
            sequences.Add("S452", new List<double>(new double[] { 40.8, 11.2, 24.2, 28.7, 28.7, 9.4, 36.6 }));
            sequences.Add("S453", new List<double>(new double[] { 20.5, 11.3, 0.9, 13.3, 40.6, 12.7, 40.7 }));
            sequences.Add("S454", new List<double>(new double[] { 39.1, 15.9, 39.4, 1.2, 28.9, 22.4, 30.7 }));
            sequences.Add("S455", new List<double>(new double[] { 8.4, 14.9, 44.5, 2.2, 47.2, 19.5, 0.5 }));
            sequences.Add("S456", new List<double>(new double[] { 13.9, 24.5, 38.3, 39.4, 5.1, 3.7, 21.4 }));
            sequences.Add("S457", new List<double>(new double[] { 18.9, 37, 13.7, 15.5, 10.4, 12, 24.8 }));
            sequences.Add("S458", new List<double>(new double[] { 7, 32.5, 43.9, 43.7, 45.8, 32.9, 26.5 }));
            sequences.Add("S459", new List<double>(new double[] { 17.3, 12.6, 35.7, 31.3, 24.3, 27.8, 22.3 }));
            sequences.Add("S460", new List<double>(new double[] { 34.7, 17.2, 35.9, 26, 0.7, 20, 34.8 }));
            sequences.Add("S461", new List<double>(new double[] { 43.2, 31.9, 43.6, 44.4, 43.1, 14.7, 41 }));
            sequences.Add("S462", new List<double>(new double[] { 6.4, 43.7, 32.8, 44.5, 3.2, 3.9, 47.3 }));
            sequences.Add("S463", new List<double>(new double[] { 24.7, 16.8, 40.9, 5, 7.4, 11.5, 27.9 }));
            sequences.Add("S464", new List<double>(new double[] { 15.3, 35.2, 18, 2.3, 11.2, 9.8, 26 }));
            sequences.Add("S465", new List<double>(new double[] { 18.4, 29.2, 36.3, 40.8, 12, 47.3, 35.5 }));
            sequences.Add("S466", new List<double>(new double[] { 35.7, 40.2, 6.8, 17.2, 0.5, 15.9, 5.4 }));
            sequences.Add("S467", new List<double>(new double[] { 10.8, 22.3, 30.7, 23.7, 38.5, 32, 3.2 }));
            sequences.Add("S468", new List<double>(new double[] { 46.5, 43.2, 3.5, 3.6, 26, 20.7, 30 }));
            sequences.Add("S469", new List<double>(new double[] { 7.2, 26.3, 15.7, 47.5, 38.8, 28.1, 12.3 }));
            sequences.Add("S470", new List<double>(new double[] { 39.2, 16, 31, 5.7, 19, 38.6, 31.9 }));
            sequences.Add("S471", new List<double>(new double[] { 17.6, 30, 21.4, 14.3, 39.5, 22.2, 6.9 }));
            sequences.Add("S472", new List<double>(new double[] { 20.9, 26.6, 21.1, 0.5, 22.3, 29.4, 34.6 }));
            sequences.Add("S473", new List<double>(new double[] { 24.1, 33.7, 19.6, 35.7, 45.5, 46.1, 29.8 }));
            sequences.Add("S474", new List<double>(new double[] { 34.5, 17, 30.8, 21.5, 2.4, 34.5, 44.6 }));
            sequences.Add("S475", new List<double>(new double[] { 4.6, 26.7, 6.8, 6, 31.8, 18.1, 3.1 }));
            sequences.Add("S476", new List<double>(new double[] { 17.7, 34.8, 40.3, 42.6, 8.8, 32.3, 23.9 }));
            sequences.Add("S477", new List<double>(new double[] { 9.5, 44.9, 30.1, 5.4, 3.2, 39.8, 34.8 }));
            sequences.Add("S478", new List<double>(new double[] { 46.5, 11.1, 1.9, 43, 18.8, 37.3, 45.5 }));
            sequences.Add("S479", new List<double>(new double[] { 10.5, 23.3, 39.6, 4, 9.4, 16.9, 28.2 }));
            sequences.Add("S480", new List<double>(new double[] { 39.7, 45.9, 31.8, 13, 45, 40.4, 31.7 }));
            sequences.Add("S481", new List<double>(new double[] { 24.7, 42.1, 19.3, 28.8, 40.1, 28.1, 46.3 }));
            sequences.Add("S482", new List<double>(new double[] { 43.6, 31, 6.2, 40.5, 8, 27, 42.7 }));
            sequences.Add("S483", new List<double>(new double[] { 3.6, 32.2, 34.6, 1.5, 37, 28.6, 24.6 }));
            sequences.Add("S484", new List<double>(new double[] { 38.4, 37.1, 12.8, 32.2, 31.7, 14.8, 17.9 }));
            sequences.Add("S485", new List<double>(new double[] { 7.1, 36.9, 33, 27.3, 30.5, 25.8, 32.4 }));
            sequences.Add("S486", new List<double>(new double[] { 14.9, 26.8, 2.6, 32.1, 21.4, 0.9, 45 }));
            sequences.Add("S487", new List<double>(new double[] { 42.1, 38.3, 11.5, 45.8, 20.2, 21.9, 40 }));
            sequences.Add("S488", new List<double>(new double[] { 41.1, 24.8, 8.8, 5.9, 11.6, 6.5, 27.7 }));
            sequences.Add("S489", new List<double>(new double[] { 32.1, 4.3, 4.1, 7, 29.2, 46, 43.8 }));
            sequences.Add("S490", new List<double>(new double[] { 34.4, 28.8, 37.5, 29.8, 9.6, 25, 12.5 }));
            sequences.Add("S491", new List<double>(new double[] { 26.9, 41, 3.3, 22.4, 11.9, 30.5, 39.5 }));
            sequences.Add("S492", new List<double>(new double[] { 17, 15.7, 22, 28.4, 23.2, 35.1, 17.6 }));
            sequences.Add("S493", new List<double>(new double[] { 15.1, 8.5, 37.4, 9.6, 9.5, 35.3, 0.6 }));
            sequences.Add("S494", new List<double>(new double[] { 41.2, 12.1, 28.2, 47.7, 20.5, 3.7, 40.5 }));
            sequences.Add("S495", new List<double>(new double[] { 23, 4.5, 33.9, 6, 25.4, 26.5, 42.8 }));
            sequences.Add("S496", new List<double>(new double[] { 46.6, 38.4, 36.1, 25.6, 13.5, 32.6, 6.3 }));
            sequences.Add("S497", new List<double>(new double[] { 32.1, 18.4, 8.1, 35.2, 28.5, 36, 43.8 }));
            sequences.Add("S498", new List<double>(new double[] { 8.9, 4.8, 5.1, 37.1, 39.9, 16.5, 45.8 }));
            sequences.Add("S499", new List<double>(new double[] { 3, 1.8, 41.5, 8, 23.5, 46.2, 25.9 }));
            sequences.Add("S500", new List<double>(new double[] { 41.1, 0.3, 30.3, 10, 31.8, 19.1, 47.5 }));
            

             
            sequences.Add("S501", new List<double>(new double[] { 2.5, 28.8, 9.1, 34.8, 25.5, 3.3, 12.7 }));
            sequences.Add("S502", new List<double>(new double[] { 20.4, 22.4, 32.4, 7.6, 13.2, 42.2, 39.2 }));
            sequences.Add("S503", new List<double>(new double[] { 42.3, 40.9, 23.1, 6.2, 24.4, 0, 9.8 }));
            sequences.Add("S504", new List<double>(new double[] { 6.3, 38.2, 45.7, 27.4, 3.6, 2.1, 22.9 }));
            sequences.Add("S505", new List<double>(new double[] { 33.1, 12.4, 18.5, 5.6, 26.6, 44.6, 2.3 }));
            sequences.Add("S506", new List<double>(new double[] { 0.1, 46, 6.1, 4.9, 26, 3.4, 22.1 }));
            sequences.Add("S507", new List<double>(new double[] { 41.6, 3.1, 19.7, 32.4, 38.6, 42.8, 18.9 }));
            sequences.Add("S508", new List<double>(new double[] { 9.6, 17.7, 41.1, 33.6, 11.4, 34.7, 42.4 }));
            sequences.Add("S509", new List<double>(new double[] { 31.6, 18.7, 7.9, 16.4, 43.8, 39, 30.1 }));
            sequences.Add("S510", new List<double>(new double[] { 46.3, 23.8, 29.6, 28.7, 34.7, 24, 30.8 }));
            sequences.Add("S511", new List<double>(new double[] { 28.7, 29, 40.8, 35.4, 25.8, 6.7, 7.7 }));
            sequences.Add("S512", new List<double>(new double[] { 38.4, 10.3, 9.5, 17.2, 8.1, 13.1, 43.8 }));
            sequences.Add("S513", new List<double>(new double[] { 22.7, 31.6, 44.3, 38.9, 40.1, 6.6, 21.1 }));
            sequences.Add("S514", new List<double>(new double[] { 35.6, 28.6, 27.5, 23, 4.5, 46.7, 45.2 }));
            sequences.Add("S515", new List<double>(new double[] { 43.3, 20.8, 45.7, 21.5, 37.3, 32.1, 34.8 }));
            sequences.Add("S516", new List<double>(new double[] { 20.1, 0.2, 26.4, 21.2, 19.7, 25.8, 28 }));
            sequences.Add("S517", new List<double>(new double[] { 37.8, 13.1, 4.3, 36.8, 25, 21, 29.7 }));
            sequences.Add("S518", new List<double>(new double[] { 32, 41.2, 24.7, 7.1, 3.8, 3.8, 33.3 }));
            sequences.Add("S519", new List<double>(new double[] { 46.3, 31.4, 10.4, 46, 22.8, 20.2, 27.7 }));
            sequences.Add("S520", new List<double>(new double[] { 40.8, 7.3, 18.1, 19.9, 25.8, 0.6, 12.5 }));
            sequences.Add("S521", new List<double>(new double[] { 6, 31.3, 2.4, 39.2, 37.3, 43.2, 27.3 }));
            sequences.Add("S522", new List<double>(new double[] { 31.4, 35.5, 25.9, 35, 5.5, 42.3, 21.7 }));
            sequences.Add("S523", new List<double>(new double[] { 1.4, 19.4, 28.9, 23.4, 40.2, 14.3, 19.3 }));
            sequences.Add("S524", new List<double>(new double[] { 17.4, 1.5, 10.6, 24.5, 17.6, 16.8, 8.4 }));
               
            sequences.Add("S525", new List<double>(new double[] { 34.8, 15.2, 11.3, 38.6, 35.9, 0.4, 38.5 }));
            sequences.Add("S526", new List<double>(new double[] { 31.1, 1.5, 40.6, 36.9, 5.5, 16.4, 12.3 }));
            sequences.Add("S527", new List<double>(new double[] { 24.6, 13.6, 36.5, 6.2, 23.2, 42.3, 9 }));
            sequences.Add("S528", new List<double>(new double[] { 8.5, 10, 11.1, 41.6, 8.4, 30.7, 35.4 }));
            sequences.Add("S529", new List<double>(new double[] { 13, 43, 23.4, 39, 42.2, 16.8, 29.9 }));
            sequences.Add("S530", new List<double>(new double[] { 44, 8.2, 4.8, 21, 37.2, 26.4, 19.4 }));
            sequences.Add("S531", new List<double>(new double[] { 31.4, 27.5, 18.8, 24.5, 23, 40.5, 13.8 }));
            sequences.Add("S532", new List<double>(new double[] { 10.5, 11.4, 42.3, 36.5, 24.2, 15.5, 19 }));
            sequences.Add("S533", new List<double>(new double[] { 1.5, 5.2, 41.8, 39.5, 21.1, 4.3, 43.8 }));
            sequences.Add("S534", new List<double>(new double[] { 39.8, 34.2, 13.1, 10.3, 45.5, 44, 11.5 }));
            sequences.Add("S535", new List<double>(new double[] { 2.6, 42.3, 14.7, 35.9, 42.8, 17.9, 0.7 }));
            sequences.Add("S536", new List<double>(new double[] { 32.4, 35.6, 6.1, 43.5, 10.3, 32.1, 2.8 }));
            sequences.Add("S537", new List<double>(new double[] { 14.8, 6.5, 42.9, 8.3, 0.2, 40, 33.1 }));
            sequences.Add("S538", new List<double>(new double[] { 7.4, 38, 42.1, 46.3, 12, 4.8, 6.7 }));
            sequences.Add("S539", new List<double>(new double[] { 13.4, 5.9, 37.5, 40.9, 21.4, 24.1, 13.3 }));
            sequences.Add("S540", new List<double>(new double[] { 33.2, 2.7, 36.1, 0.3, 15.1, 9.2, 11.9 }));
           
            sequences.Add("S541", new List<double>(new double[] { 5.1, 22.6, 39.9, 19.5, 23.5, 45.3, 3.9 }));
            sequences.Add("S542", new List<double>(new double[] { 15.8, 28.7, 8.3, 35, 28.5, 6.4, 32 }));
            sequences.Add("S543", new List<double>(new double[] { 28, 7.1, 39.1, 0.9, 11.7, 38.5, 3.8 }));
            sequences.Add("S544", new List<double>(new double[] { 0.5, 10.7, 45.4, 8.7, 26.7, 44.7, 2.6 }));
            sequences.Add("S545", new List<double>(new double[] { 28.3, 13.4, 18.5, 30.8, 44.1, 46.7, 35.9 }));
            sequences.Add("S546", new List<double>(new double[] { 9.2, 21, 16.3, 1.9, 4.3, 16.2, 7 }));
            sequences.Add("S547", new List<double>(new double[] { 43.5, 47.9, 22.6, 11, 34.8, 46.6, 35.8 }));
            sequences.Add("S548", new List<double>(new double[] { 17.9, 2.3, 21.6, 23.9, 33.2, 27.7, 43.5 }));
            sequences.Add("S549", new List<double>(new double[] { 40.4, 36.8, 10.6, 21.4, 16.7, 37.2, 4.7 }));
            sequences.Add("S550", new List<double>(new double[] { 38.7, 36, 11.3, 10.6, 22.2, 30.9, 33.9 }));
            sequences.Add("S551", new List<double>(new double[] { 8.2, 7.4, 17.8, 41.8, 33.7, 37.7, 24.1 }));
            sequences.Add("S552", new List<double>(new double[] { 5.4, 18.6, 19, 27.9, 7, 9.5, 38.4 }));
            sequences.Add("S553", new List<double>(new double[] { 37.5, 13.8, 39.7, 1.5, 11.5, 21.3, 2.7 }));
            sequences.Add("S554", new List<double>(new double[] { 22.7, 20.2, 39.4, 9.2, 11.5, 11.9, 22 }));
            sequences.Add("S555", new List<double>(new double[] { 36.5, 30.8, 5, 21.7, 39.9, 18.5, 44.7 }));
            sequences.Add("S556", new List<double>(new double[] { 12.4, 21.4, 3.2, 38, 26.7, 36.3, 7.7 }));
            sequences.Add("S557", new List<double>(new double[] { 16.9, 43, 25.5, 22.9, 44.3, 42.5, 13.5 }));
            sequences.Add("S558", new List<double>(new double[] { 39.4, 17.8, 37.3, 45, 19.2, 26.7, 41 }));
            sequences.Add("S559", new List<double>(new double[] { 17.4, 2.7, 17.2, 38.9, 8, 2.7, 14.4 }));
            sequences.Add("S560", new List<double>(new double[] { 26.2, 0, 19.4, 1.4, 19.1, 33.5, 33.3 }));
           
  



            sequences.Add("S601", new List<double>(new double[] { 9.4, 30.4, 26.1, 47.1, 20.9, 17.1, 21 }));
            sequences.Add("S602", new List<double>(new double[] { 6.1, 24.9, 19.8, 37.2, 18.6, 22.3, 40.2 }));
            sequences.Add("S603", new List<double>(new double[] { 21.5, 7.5, 17.3, 21, 4.4, 36.7, 42.1 }));
            sequences.Add("S604", new List<double>(new double[] { 32, 12.6, 27.8, 3.1, 44.3, 17.4, 34.8 }));
            sequences.Add("S605", new List<double>(new double[] { 44.1, 37.5, 3.2, 17.7, 0.8, 31.4, 42.6 }));
            sequences.Add("S606", new List<double>(new double[] { 42.9, 31.2, 39.6, 38.9, 45.2, 45.1, 20.5 }));
            sequences.Add("S607", new List<double>(new double[] { 36.3, 32.8, 21.1, 47.4, 35.9, 13.3, 39 }));
            sequences.Add("S608", new List<double>(new double[] { 27.7, 45.7, 5.7, 30.1, 46.9, 19.2, 35.8 }));
            sequences.Add("S609", new List<double>(new double[] { 1.5, 32, 43.2, 46.3, 14.6, 39.4, 7.9 }));
            sequences.Add("S610", new List<double>(new double[] { 3.9, 25.7, 8.2, 45.7, 40, 6.9, 19.7 }));
            sequences.Add("S611", new List<double>(new double[] { 10.5, 23.6, 17.8, 39.3, 35.4, 33, 18.4 }));
            sequences.Add("S612", new List<double>(new double[] { 6.5, 36.5, 26.3, 35.1, 41, 46.6, 7.9 }));
            sequences.Add("S613", new List<double>(new double[] { 9.1, 15.9, 31.8, 24.4, 30.7, 43.6, 40.8 }));
            sequences.Add("S614", new List<double>(new double[] { 5.6, 11.6, 2.6, 0.5, 1.7, 14.3, 9.4 }));
            sequences.Add("S615", new List<double>(new double[] { 23.9, 35.7, 16.2, 0.7, 42.3, 0.1, 33.3 }));
            sequences.Add("S616", new List<double>(new double[] { 35.6, 15.7, 41.1, 0.5, 5.1, 1.6, 20.5 }));
            sequences.Add("S617", new List<double>(new double[] { 38.5, 12.8, 6.4, 14.2, 14.7, 2.1, 43.9 }));
            sequences.Add("S618", new List<double>(new double[] { 41.8, 33.2, 40.6, 6, 6.7, 46, 46.5 }));
            sequences.Add("S619", new List<double>(new double[] { 16.2, 24.5, 5.1, 7.7, 8.5, 33.6, 30.7 }));
            sequences.Add("S620", new List<double>(new double[] { 3.8, 2.9, 41.1, 36.4, 45, 5.2, 11.8 }));
            
            sequences.Add("S621", new List<double>(new double[] { 0.1, 37.6, 46.7, 23.1, 41.3, 25.7, 39.4 }));
           
            sequences.Add("S622", new List<double>(new double[] { 31.6, 31.1, 17.7, 42.1, 17.3, 31.1, 6.1 }));
          
            sequences.Add("S623", new List<double>(new double[] { 33.9, 18.1, 46.8, 11.7, 37.9, 17.1, 6.6 }));
          
            sequences.Add("S624", new List<double>(new double[] { 42.1, 43.5, 44.2, 16.6, 22.9, 40.5, 6.6 }));
              
            sequences.Add("S625", new List<double>(new double[] { 23.2, 31.5, 29.9, 20.1, 7.7, 1.3, 33.3 }));
             
            sequences.Add("S626", new List<double>(new double[] { 33.3, 27.4, 42.7, 26.7, 11.5, 47.6, 6.6 }));
          
            sequences.Add("S627", new List<double>(new double[] { 12.4, 11.2, 31.2, 31.3, 2.2, 42.3, 6.6 }));
            sequences.Add("S628", new List<double>(new double[] { 18.2, 45.1, 4.5, 35.5, 7.4, 36.8, 6.6 }));
             
            sequences.Add("S629", new List<double>(new double[] { 14.2, 46.4, 24.8, 26.4, 44.2, 9.9, 15.4 }));
            sequences.Add("S630", new List<double>(new double[] { 4.2, 28.7, 47.4, 31.3, 14.4, 3.1, 7.3 }));
            sequences.Add("S631", new List<double>(new double[] { 12.2, 43.3, 16.9, 41.3, 33.7, 1.4, 23.1 }));
          
            sequences.Add("S632", new List<double>(new double[] { 14.2, 46.4, 24.8, 26.4, 44.2, 9.9, 15.4 }));
            sequences.Add("S633", new List<double>(new double[] { 21.5, 28.3, 34.3, 11.7, 28.8, 42.1, 24.8 }));
            sequences.Add("S634", new List<double>(new double[] { 22.8, 29.7, 35.6, 41.5, 42.3, 32.5, 33.6 }));
            sequences.Add("S635", new List<double>(new double[] { 12.4, 17.5, 17.5, 25.9, 17.7, 44.9, 11.9 }));
            sequences.Add("S636", new List<double>(new double[] { 27.8, 22.5, 17.9, 22.9, 39.9, 40.3, 29.4 }));
              
            sequences.Add("S637", new List<double>(new double[] { 9.2, 47.1, 23.6, 17.6, 11, 6.4, 39.2 }));
            sequences.Add("S638", new List<double>(new double[] { 15.7, 46.1, 13.4, 37.9, 45.8, 12.6, 30.7 }));
            sequences.Add("S639", new List<double>(new double[] { 3.2, 41, 13, 40.6, 15.4, 29.6, 18.1 }));
            sequences.Add("S640", new List<double>(new double[] { 32.8, 41.2, 43.1, 29.6, 27.3, 37.5, 31.4 }));
           
            sequences.Add("S641", new List<double>(new double[] { 2, 4.5, 1.7, 0.7, 3.8, 25.3, 34.3 }));
            sequences.Add("S642", new List<double>(new double[] { 1.9, 36.3, 34.9, 13.3, 8, 46.8, 21.9 }));
            sequences.Add("S643", new List<double>(new double[] { 1.3, 13.8, 12.7, 39.8, 8.3, 46.3, 32.4 }));
            sequences.Add("S644", new List<double>(new double[] { 23.4, 36.7, 22.9, 13.9, 47.5, 11.2, 26.3 }));
            sequences.Add("S645", new List<double>(new double[] { 44.3, 30.6, 0, 11.4, 10.4, 44.8, 27.8 }));
             
            sequences.Add("S646", new List<double>(new double[] { 21.1, 40.1, 38.1, 39.5, 37.2, 33.8, 26.4 }));
            sequences.Add("S647", new List<double>(new double[] { 20.9, 31.7, 32.5, 44.2, 16.4, 2.5, 14.2 }));
            sequences.Add("S648", new List<double>(new double[] { 5.6, 6.9, 39.8, 7, 31.3, 22.7, 42.8 }));
            sequences.Add("S649", new List<double>(new double[] { 24.4, 33.6, 34.3, 26.9, 7.3, 46.9, 21.5 }));
            sequences.Add("S650", new List<double>(new double[] { 25.6, 18.5, 29.9, 43.4, 41.2, 44.2, 11 }));
            sequences.Add("S651", new List<double>(new double[] { 41.2, 38, 47.3, 6.1, 10.1, 31.2, 47 }));
            sequences.Add("S652", new List<double>(new double[] { 26.1, 37.3, 42.7, 9.7, 47.8, 26.7, 37.9 }));
            sequences.Add("S653", new List<double>(new double[] { 17.3, 30.3, 44.1, 18.5, 26.5, 29.3, 4.2 }));
            sequences.Add("S654", new List<double>(new double[] { 31.1, 18.5, 24, 1.9, 37, 7.7, 6.1 }));
             
            sequences.Add("S655", new List<double>(new double[] { 26.4, 27.1, 22.7, 1.5, 15, 19.7, 2.7 }));
            sequences.Add("S656", new List<double>(new double[] { 18.1, 16.9, 25.6, 41.2, 10, 3.7, 40.9 }));
            sequences.Add("S657", new List<double>(new double[] { 28.7, 41, 28.5, 12.4, 48, 13.2, 36.5 }));
            sequences.Add("S658", new List<double>(new double[] { 27.7, 11.8, 4.5, 6.7, 42.6, 2.2, 38.1 }));
            sequences.Add("S659", new List<double>(new double[] { 47.4, 3.5, 22.2, 26.9, 13.7, 10.5, 22.1 }));
            sequences.Add("S660", new List<double>(new double[] { 47.9, 29, 37.3, 22.3, 1.3, 31.1, 4.6 }));
            sequences.Add("S661", new List<double>(new double[] { 13, 44.2, 42.4, 24.6, 35.4, 4.6, 11.7 }));
            sequences.Add("S662", new List<double>(new double[] { 35.8, 21.5, 19.1, 32.2, 35.7, 33.6, 11.7 }));
            sequences.Add("S663", new List<double>(new double[] { 24.4, 20.3, 0.9, 6.6, 0.6, 8.3, 11.8 }));
            sequences.Add("S664", new List<double>(new double[] { 8.9, 6.1, 12.5, 30.6, 46.5, 35.9, 7.5 }));
            sequences.Add("S665", new List<double>(new double[] { 3.9, 14.9, 24.1, 43, 14, 36.1, 13.8 }));
            
            sequences.Add("S666", new List<double>(new double[] { 38, 30.5, 33.4, 36.9, 14.9, 22.2, 2.7 }));
            sequences.Add("S667", new List<double>(new double[] { 25.6, 15.8, 38.5, 1.2, 2.4, 39.1, 22.7 }));
            sequences.Add("S668", new List<double>(new double[] { 38.4, 30.3, 34.7, 41.4, 26.7, 10.3, 41.8 }));
            sequences.Add("S669", new List<double>(new double[] { 6.2, 12.2, 37.1, 0.4, 36.1, 27.8, 24.9 }));
            sequences.Add("S670", new List<double>(new double[] { 42.4, 22.8, 5, 23, 1.7, 42.6, 41.8 }));
            sequences.Add("S671", new List<double>(new double[] { 46.6, 27.2, 27.2, 1.9, 41.3, 16, 0.5 }));
            sequences.Add("S672", new List<double>(new double[] { 2.4, 7.3, 28.4, 2.6, 38.6, 22, 46 }));
            sequences.Add("S673", new List<double>(new double[] { 16.6, 12, 21.1, 13.2, 35.6, 20.6, 17.2 }));
            sequences.Add("S674", new List<double>(new double[] { 14.1, 1.5, 43.4, 1.1, 34, 12, 5.2 }));
            sequences.Add("S675", new List<double>(new double[] { 32.1, 4.9, 42.7, 7.4, 24.7, 18.7, 35.8 }));
            sequences.Add("S676", new List<double>(new double[] { 34, 15.8, 24.2, 9.3, 0.2, 20.7, 30.2 }));
            sequences.Add("S677", new List<double>(new double[] { 14.1, 48, 31.7, 2.8, 12.1, 43, 14.5 }));
            sequences.Add("S678", new List<double>(new double[] { 17.3, 39.2, 11.8, 33.4, 28.5, 15.4, 42.8 }));
            sequences.Add("S679", new List<double>(new double[] { 21.4, 5.4, 43.9, 40.2, 35.9, 11, 40.4 }));
            sequences.Add("S680", new List<double>(new double[] { 26.3, 14.3, 40, 41.8, 27.1, 10.2, 12.4 }));
            
            sequences.Add("S681", new List<double>(new double[] { 2.6, 19.2, 26.4, 32, 12.8, 6.2, 2.7 }));
            sequences.Add("S682", new List<double>(new double[] { 19.5, 16.8, 47.9, 39.8, 28, 25.2, 48 }));
            sequences.Add("S683", new List<double>(new double[] { 18.4, 14.2, 45.7, 8.8, 35.2, 41.5, 34.2 }));
            sequences.Add("S684", new List<double>(new double[] { 9.6, 21.4, 5.7, 23, 21.8, 34, 34.6 }));
            sequences.Add("S685", new List<double>(new double[] { 4.2, 20.2, 38.5, 11.8, 8.1, 9.3, 45.8 }));
            sequences.Add("S686", new List<double>(new double[] { 42.5, 20.8, 1.2, 45.6, 26.1, 40.3, 28.5 }));
            sequences.Add("S687", new List<double>(new double[] { 12, 39.4, 22.6, 9.1, 14, 11.7, 18.3 }));
            sequences.Add("S688", new List<double>(new double[] { 10.8, 1.9, 47.5, 43.9, 18.8, 4.9, 11.9 }));
            sequences.Add("S689", new List<double>(new double[] { 0.1, 3, 30.5, 32.7, 14.4, 24.8, 36.2 }));
            sequences.Add("S690", new List<double>(new double[] { 23.6, 19.8, 12.2, 21.8, 18.4, 2.4, 10.3 }));
           
            sequences.Add("S691", new List<double>(new double[] { 25.8, 5.2, 24.1, 17.9, 20.6, 33.6, 47.3 }));
            sequences.Add("S692", new List<double>(new double[] { 34.2, 40.6, 24.4, 47.7, 45.3, 12, 28.3 }));
            sequences.Add("S693", new List<double>(new double[] { 17.2, 1.1, 46.1, 29.5, 9.9, 6.6, 18.2 }));
            sequences.Add("S694", new List<double>(new double[] { 41.8, 41.1, 38.9, 23.5, 18.4, 45.3, 5.9 }));
            sequences.Add("S695", new List<double>(new double[] { 18.2, 31.7, 4.8, 26.3, 35.8, 43.3, 23.2 }));
            sequences.Add("S696", new List<double>(new double[] { 16.1, 24.8, 10.7, 27.2, 8.1, 10.3, 7.3 }));
            sequences.Add("S697", new List<double>(new double[] { 16.3, 20.7, 37.7, 11, 15.6, 0.4, 7 }));
            sequences.Add("S698", new List<double>(new double[] { 32.5, 26.6, 1.3, 43.8, 21.8, 35.9, 11.7 }));
            sequences.Add("S699", new List<double>(new double[] { 1.3, 16.8, 40.4, 24, 27.6, 43.2, 3.5 }));
            sequences.Add("S700", new List<double>(new double[] { 17.3, 19.8, 9.2, 17.8, 20.6, 20, 41.5 }));
           


            sequences.Add("S701", new List<double>(new double[] { 23, 40.2, 42.7, 17.9, 13.2, 35.1, 5.9 }));
            sequences.Add("S702", new List<double>(new double[] { 46.7, 13.5, 25.9, 45.9, 43.7, 20.2, 15.4 }));
            sequences.Add("S703", new List<double>(new double[] { 7.5, 28.3, 19.3, 31.8, 10.4, 26.5, 35.9 }));
            sequences.Add("S704", new List<double>(new double[] { 10.8, 5.6, 42.5, 9.6, 4.7, 11.6, 9.4 }));
            sequences.Add("S705", new List<double>(new double[] { 19.1, 2.4, 32.7, 35.6, 1.1, 11.6, 39.8 }));
          
            sequences.Add("S706", new List<double>(new double[] { 7.3, 22.5, 13.7, 1.1, 45, 36.3, 12.5 }));
            sequences.Add("S707", new List<double>(new double[] { 5.9, 17.2, 34.3, 37.3, 9.8, 17, 1 }));
            sequences.Add("S708", new List<double>(new double[] { 40.7, 13.2, 43.2, 11.2, 25.4, 15.3, 9.4 }));
            sequences.Add("S709", new List<double>(new double[] { 33.1, 16.4, 0.1, 18.1, 0.3, 44.3, 4.1 }));
            sequences.Add("S710", new List<double>(new double[] { 0, 43.3, 5.7, 40.2, 31.2, 29.2, 0.7 }));
            sequences.Add("S711", new List<double>(new double[] { 13.4, 17.9, 27.3, 35.3, 35.5, 42.9, 6.3 }));
            sequences.Add("S712", new List<double>(new double[] { 6, 6.4, 35.6, 45.9, 21.1, 0.9, 18.8 }));
            sequences.Add("S713", new List<double>(new double[] { 16.9, 20.1, 42.6, 4.9, 13.2, 7.8, 44.5 }));
            sequences.Add("S714", new List<double>(new double[] { 12.1, 38.6, 3.4, 15.9, 21.7, 42, 17.6 }));
            sequences.Add("S715", new List<double>(new double[] { 2.6, 15.8, 3.5, 42.4, 16.6, 32.5, 31.1 }));
            sequences.Add("S716", new List<double>(new double[] { 11.1, 46.3, 8.5, 36.7, 13.5, 46.3, 38.3 }));
            sequences.Add("S717", new List<double>(new double[] { 9, 5.7, 21.7, 39, 24.5, 42.7, 32.8 }));
            sequences.Add("S718", new List<double>(new double[] { 7.5, 32.1, 46.1, 0.2, 13.4, 0.3, 43.1 }));
            sequences.Add("S719", new List<double>(new double[] { 17.8, 42.4, 20.1, 36.5, 36.5, 11.5, 41.3 }));
            sequences.Add("S720", new List<double>(new double[] { 18.8, 16.2, 29.9, 34.9, 9.7, 4.7, 42.3 }));
            sequences.Add("S721", new List<double>(new double[] { 3.1, 43.3, 5.2, 32.4, 12.2, 29.3, 40.4 }));
            sequences.Add("S722", new List<double>(new double[] { 45.2, 37.5, 13.9, 31.6, 28.7, 37.9, 43.6 }));
            sequences.Add("S723", new List<double>(new double[] { 24.3, 19.6, 31.9, 20.9, 38.2, 11.4, 1.2 }));
            sequences.Add("S724", new List<double>(new double[] { 33.5, 37.6, 34.1, 35.4, 43.5, 33, 30.4 }));
            sequences.Add("S725", new List<double>(new double[] { 1.1, 22.5, 26.7, 11.6, 8.7, 29.4, 5.8 }));
            sequences.Add("S726", new List<double>(new double[] { 34.9, 34.9, 1.8, 28.9, 29.6, 25.1, 14.6 }));
            sequences.Add("S727", new List<double>(new double[] { 13.1, 13, 1.6, 34.1, 34.2, 39.8, 24.3 }));
            sequences.Add("S728", new List<double>(new double[] { 40, 12.3, 14.9, 30.4, 19.3, 38.4, 33.5 }));
            sequences.Add("S729", new List<double>(new double[] { 45, 21.8, 3.1, 14.7, 25.7, 41.3, 16.4 }));
            sequences.Add("S730", new List<double>(new double[] { 20.3, 9.7, 35.6, 26.6, 35.4, 41.5, 6.1 }));
          
            sequences.Add("S731", new List<double>(new double[] { 20.3, 9.7, 35.6, 26.6, 35.4, 41.5, 6.1 }));
            
            sequences.Add("S732", new List<double>(new double[] { 45.1, 21.8, 3.2, 14.7, 25.7, 41.3, 16.4 }));
               
            sequences.Add("S733", new List<double>(new double[] { 24.4, 31.7, 11.5, 27.1, 11.6, 37.7, 6.1 }));
           
           sequences.Add("S734", new List<double>(new double[] { 10.7, 13.1, 0.4, 20.2, 1, 4.2, 46.8 }));
           sequences.Add("S735", new List<double>(new double[] { 15.5, 14.3, 19.8, 44.9, 4.1, 26.3, 8.3 }));
            sequences.Add("S736", new List<double>(new double[] { 10.5, 22.5, 39.6, 1.2, 21.6, 20.3, 3.9 }));
            sequences.Add("S737", new List<double>(new double[] { 42.1, 41.3, 27.1, 27.6, 30.8, 4.5, 40.3 }));
            sequences.Add("S738", new List<double>(new double[] { 46.8, 29.3, 25.3, 30.4, 40.8, 12.7, 11.2 }));
              
            sequences.Add("S739", new List<double>(new double[] { 18.3, 43.8, 26.1, 40.5, 36.7, 39.6, 26.9 }));
            sequences.Add("S740", new List<double>(new double[] { 33.4, 13.4, 39.8, 11.5, 12, 34, 7.2 }));
            sequences.Add("S741", new List<double>(new double[] { 18.1, 16, 1.3, 28.8, 20.3, 15, 35.2 }));
            sequences.Add("S742", new List<double>(new double[] { 0.9, 26.8, 4, 11.5, 44.6, 1.5, 37.9 }));
            sequences.Add("S743", new List<double>(new double[] { 14.9, 3.1, 7.9, 45.6, 5.7, 42.3, 44.9 }));
            sequences.Add("S744", new List<double>(new double[] { 45.4, 34.5, 33.3, 0.3, 0.3, 40.5, 7.7 }));
            sequences.Add("S745", new List<double>(new double[] { 34.5, 40.9, 28.2, 10.1, 24.1, 35.5, 42.4 }));
           
            sequences.Add("S746", new List<double>(new double[] { 13.1, 33.3, 47.2, 36.8, 24.6, 28.9, 5.1 }));
            sequences.Add("S747", new List<double>(new double[] { 27.2, 8.6, 1.4, 47.3, 32.7, 34.2, 40.9 }));
            sequences.Add("S748", new List<double>(new double[] { 33.6, 26.4, 19.1, 31.9, 4.8, 29.9, 40.3 }));
            
            sequences.Add("S749", new List<double>(new double[] { 27.2, 8.6, 1.4, 47.3, 32.7, 34.2, 40.9 }));
           
            sequences.Add("S750", new List<double>(new double[] { 13.1, 33.3, 47.2, 37.8, 21.6, 28.9, 5.1 }));
           
            sequences.Add("S751", new List<double>(new double[] { 30, 21.9, 6.5, 46.8, 0.5, 6.9, 41.2 }));
            sequences.Add("S752", new List<double>(new double[] { 27.8, 17.9, 27.7, 6.8, 3.5, 29.3, 0.9 }));
            sequences.Add("S753", new List<double>(new double[] { 2.1, 2.3, 25, 21.6, 9.7, 23.9, 38.3 }));
            sequences.Add("S754", new List<double>(new double[] { 7.9, 34.4, 17.6, 14.1, 43, 22.4, 10.4 }));
            sequences.Add("S755", new List<double>(new double[] { 20.6, 41.5, 43.5, 38.2, 39.5, 19, 12.2 }));
              
            sequences.Add("S756", new List<double>(new double[] { 27.5, 1.4, 40.5, 34.2, 12.2, 6.7, 13.3 }));
            sequences.Add("S757", new List<double>(new double[] { 33.3, 34.9, 36.6, 45.3, 7.5, 30.3, 20.9 }));
            sequences.Add("S758", new List<double>(new double[] { 35.2, 39.1, 8.3, 14.5, 17.4, 18, 12.9 }));
            sequences.Add("S759", new List<double>(new double[] { 31.2, 16.6, 46.1, 42.8, 25.2, 27.3, 14.2 }));
            sequences.Add("S760", new List<double>(new double[] { 40.3, 2, 38.3, 34.1, 35.8, 25.5, 23.7 }));
           
            sequences.Add("S761", new List<double>(new double[] { 34.8, 36.4, 40, 36.7, 5.8, 11.2, 32.8 }));
            sequences.Add("S762", new List<double>(new double[] { 14.2, 7.7, 35, 28.1, 37.1, 8.5, 33.8 }));
            sequences.Add("S763", new List<double>(new double[] { 35.8, 19.9, 28.1, 7.4, 38.7, 28.8, 40.7 }));
            sequences.Add("S764", new List<double>(new double[] { 22.6, 26.4, 12.6, 45.9, 41.1, 37, 16 }));
            sequences.Add("S765", new List<double>(new double[] { 40.2, 21.2, 9.1, 37.3, 23, 19.8, 6.1 }));
            sequences.Add("S766", new List<double>(new double[] { 24.7, 29, 29.1, 14.1, 24.8, 1.9, 32.2 }));
            sequences.Add("S767", new List<double>(new double[] { 41.7, 45.2, 45.2, 35.8, 44.3, 35.8, 14.6 }));
            sequences.Add("S768", new List<double>(new double[] { 26.8, 17.4, 25.2, 42.1, 40.2, 24.7, 30.2 }));
            sequences.Add("S769", new List<double>(new double[] { 27.3, 22.3, 1, 41.8, 3.5, 7.9, 17.3 }));
            sequences.Add("S770", new List<double>(new double[] { 1.2, 4.1, 11.2, 35.5, 3.5, 46.7, 6.5 }));
           
            sequences.Add("S771", new List<double>(new double[] { 5.2, 21.8, 32.2, 43.1, 4.7, 1.7, 45 }));
            sequences.Add("S772", new List<double>(new double[] { 34.5, 9.1, 9.3, 9, 26.8, 36.6, 28.2 }));
            sequences.Add("S773", new List<double>(new double[] { 13.9, 46.2, 22.5, 3.5, 24.1, 23.7, 5.4 }));
            sequences.Add("S774", new List<double>(new double[] { 34.4, 23.7, 30.5, 16.3, 16.5, 13.3, 10.1 }));
            sequences.Add("S775", new List<double>(new double[] { 6.5, 37.4, 39.3, 1.6, 9.1, 7.4, 9.2 }));
            sequences.Add("S776", new List<double>(new double[] { 26.1, 9.7, 20.9, 42.9, 6.5, 34.5, 21.5 }));
            sequences.Add("S777", new List<double>(new double[] { 46.7, 15.2, 8, 44.4, 11.6, 38.1, 16.9 }));
            sequences.Add("S778", new List<double>(new double[] { 18.3, 30.6, 45.5, 46.5, 16.2, 5.6, 8.9 }));
            sequences.Add("S779", new List<double>(new double[] { 2.1, 41.9, 15.7, 31.9, 44.3, 46.5, 15.7 }));
            sequences.Add("S780", new List<double>(new double[] { 35.8, 3.3, 18.2, 25.1, 31.7, 35.5, 30.6 }));
           
            sequences.Add("S781", new List<double>(new double[] { 12.6, 7.5, 3.7, 1.6, 26.6, 29.4, 45.5 }));
            sequences.Add("S782", new List<double>(new double[] { 29.5, 1.3, 44.2, 21.8, 39.3, 21.4, 35.2 }));
            sequences.Add("S783", new List<double>(new double[] { 47.2, 19.2, 24, 45.5, 35.4, 26.3, 47.4 }));
            sequences.Add("S784", new List<double>(new double[] { 22.5, 41.2, 45.1, 13.6, 35.1, 25.4, 0.8 }));
            sequences.Add("S785", new List<double>(new double[] { 2.2, 11.6, 11.6, 40.3, 29.6, 17.1, 19.7 }));
            sequences.Add("S786", new List<double>(new double[] { 23.5, 0.9, 34.9, 0.5, 6.1, 38.3, 3.2 }));
            sequences.Add("S787", new List<double>(new double[] { 0.4, 14.8, 10.3, 14.6, 43.9, 39.6, 41 }));
            sequences.Add("S788", new List<double>(new double[] { 44.7, 25.8, 27.4, 3.6, 0.2, 24.9, 47.5 }));
            sequences.Add("S789", new List<double>(new double[] { 33.3, 2.8, 46.6, 33.2, 18.2, 6.5, 24.7 }));
            sequences.Add("S790", new List<double>(new double[] { 1.2, 36.2, 7.9, 30.5, 44.1, 29.6, 9.9 }));
            
            sequences.Add("S791", new List<double>(new double[] { 2.6, 23.2, 7.6, 28.7, 18.8, 12.9, 27 }));
            sequences.Add("S792", new List<double>(new double[] { 40.4, 37.8, 20.9, 35.1, 2.2, 23.5, 19.4 }));
            sequences.Add("S793", new List<double>(new double[] { 41.1, 23.8, 30.6, 32, 19.8, 15.2, 13.2 }));
            sequences.Add("S794", new List<double>(new double[] { 31.7, 18.6, 46.7, 47.3, 38.4, 23.5, 24.8 }));
            sequences.Add("S795", new List<double>(new double[] { 31.5, 30.7, 27.2, 45.3, 27.3, 1.2, 42.6 }));
            sequences.Add("S796", new List<double>(new double[] { 4.5, 6.9, 45, 10.7, 3.4, 15.4, 32.7 }));
            sequences.Add("S797", new List<double>(new double[] { 34.7, 45.4, 22, 39.9, 38, 14.7, 22.8 }));
            sequences.Add("S798", new List<double>(new double[] { 36.4, 16.3, 8, 24.6, 36.7, 16.2, 4.5 }));
            sequences.Add("S799", new List<double>(new double[] { 15.9, 42.2, 46.5, 5.7, 32, 31.1, 39.7 }));
            
          

            sequences.Add("S800", new List<double>(new double[] { 44, 24.8, 8.6, 27.5, 20.8, 5.6, 23.3 }));
            sequences.Add("S801", new List<double>(new double[] { 42, 33.2, 22.8, 48, 9.7, 23, 22 }));
            sequences.Add("S802", new List<double>(new double[] { 46.1, 19.3, 10, 24.8, 14.5, 18.7, 11 }));
            sequences.Add("S803", new List<double>(new double[] { 23.8, 41.8, 13.3, 25.8, 39.4, 39.8, 28.7 }));
            sequences.Add("S804", new List<double>(new double[] { 16.9, 26.4, 44.7, 0.6, 43.9, 9, 30.5 }));
            
            sequences.Add("S805", new List<double>(new double[] { 45.7, 46.6, 45.1, 29.6, 20.9, 10.4, 7.6 }));
            sequences.Add("S806", new List<double>(new double[] { 20.2, 8.7, 12.5, 34, 28.4, 32.5, 0.2 }));
            sequences.Add("S807", new List<double>(new double[] { 35.7, 32.8, 23.3, 1.2, 13.2, 12.1, 10.1 }));
            sequences.Add("S808", new List<double>(new double[] { 0.2, 15.6, 33.2, 2.6, 41.5, 32.8, 25.1 }));
               
            sequences.Add("S809", new List<double>(new double[] { 1.9, 41.9, 27.7, 46.8, 34.4, 15, 38.8 }));
            
            sequences.Add("S810", new List<double>(new double[] { 10.1, 44, 21, 32, 10.2, 9.8, 39.2 }));
            sequences.Add("S811", new List<double>(new double[] { 24.5, 11.6, 27.9, 33.2, 47.9, 19.7, 16.9 }));
            sequences.Add("S812", new List<double>(new double[] { 4.3, 41.7, 25, 34.7, 13.6, 40, 36.1 }));
            sequences.Add("S813", new List<double>(new double[] { 8, 12.7, 17.1, 38.2, 11.7, 26, 5.9 }));
            sequences.Add("S814", new List<double>(new double[] { 22, 0.2, 41.9, 37.2, 31.2, 45.8, 4 }));
            sequences.Add("S815", new List<double>(new double[] { 20.5, 35, 4.9, 19.7, 21.7, 16.4, 39 }));
            sequences.Add("S816", new List<double>(new double[] { 29.4, 22.9, 25.7, 13.3, 30.6, 21.4, 36.1 }));
            sequences.Add("S817", new List<double>(new double[] { 5.4, 45.6, 18.7, 27.5, 40.8, 5.3, 2.2 }));
            sequences.Add("S818", new List<double>(new double[] { 13.5, 26.6, 19.4, 8.6, 15.6, 23.6, 24.2 }));
            sequences.Add("S819", new List<double>(new double[] { 37.9, 47.8, 10.7, 5.2, 19.9, 6.5, 0.1 }));
            sequences.Add("S820", new List<double>(new double[] { 14.9, 33.7, 20.9, 9.8, 16.2, 6.2, 6 }));
            sequences.Add("S821", new List<double>(new double[] { 42.2, 11.6, 18.3, 11.5, 17.9, 25.9, 7.6 }));
            sequences.Add("S822", new List<double>(new double[] { 47.8, 40.8, 21.6, 1.9, 22.6, 26.9, 47.3 }));
            sequences.Add("S823", new List<double>(new double[] { 24, 7, 15.4, 46.1, 28.3, 7.6, 29.7 }));
            sequences.Add("S824", new List<double>(new double[] { 16, 15.2, 24.3, 2.7, 17.4, 3.1, 2.7 }));
            sequences.Add("S825", new List<double>(new double[] { 44.3, 32.1, 21.3, 0.3, 41.1, 14.7, 43.2 }));
            sequences.Add("S826", new List<double>(new double[] { 3.8, 2.8, 34.7, 11.6, 23.7, 19.9, 25.7 }));
            sequences.Add("S827", new List<double>(new double[] { 25.9, 19.2, 21.2, 38.3, 39.9, 37.2, 47.8 }));
            sequences.Add("S828", new List<double>(new double[] { 29.7, 37.7, 42, 37.8, 1.8, 43.7, 33.3 }));
            sequences.Add("S829", new List<double>(new double[] { 13.5, 40.8, 34.8, 47.2, 17.6, 15.5, 44.1 }));
            sequences.Add("S830", new List<double>(new double[] { 3.8, 40.8, 38.4, 19.8, 34, 15, 21.8 }));
            sequences.Add("S831", new List<double>(new double[] { 12, 27.7, 21.5, 23.7, 34.3, 34.5, 0.4 }));
            sequences.Add("S832", new List<double>(new double[] { 35.6, 18.6, 4.8, 20.2, 29.4, 18.9, 20.7 }));
            sequences.Add("S833", new List<double>(new double[] { 3.9, 9.3, 16.9, 30.4, 47, 36.4, 6.6 }));
            sequences.Add("S834", new List<double>(new double[] { 31.4, 46.5, 4, 22.2, 23.5, 11.1, 23.2 }));
            sequences.Add("S835", new List<double>(new double[] { 19.7, 46.1, 37.9, 5.9, 30.9, 44.1, 9 }));
            sequences.Add("S836", new List<double>(new double[] { 15.1, 18.4, 12.4, 43.5, 47.2, 11.6, 23.4 }));
            sequences.Add("S837", new List<double>(new double[] { 6.2, 8.3, 37.3, 16.9, 3.7, 45.9, 24.7 }));
            sequences.Add("S838", new List<double>(new double[] { 30, 30.8, 17.4, 24.8, 40.7, 19.9, 34.2 }));
            sequences.Add("S839", new List<double>(new double[] { 32.9, 18.6, 27, 39.7, 15.9, 47.5, 36 }));
            sequences.Add("S840", new List<double>(new double[] { 32.1, 22.7, 36.9, 8.8, 30.1, 35.2, 29.8 }));
            sequences.Add("S841", new List<double>(new double[] { 24.2, 30.3, 35.5, 14.2, 9.8, 39.1, 11.1 }));
            sequences.Add("S842", new List<double>(new double[] { 46.7, 1.2, 31, 37.4, 5.1, 22.1, 28 }));
            sequences.Add("S843", new List<double>(new double[] { 19.8, 26.7, 6.3, 2.6, 24.4, 17.3, 8.6 }));
            sequences.Add("S844", new List<double>(new double[] { 3.4, 15, 6.8, 46.7, 11.1, 20.1, 29.6 }));
            sequences.Add("S845", new List<double>(new double[] { 19.9, 44.1, 17.1, 8.2, 24.1, 34.6, 34.5 }));
            sequences.Add("S846", new List<double>(new double[] { 43.6, 40.9, 28, 21.5, 35.8, 10.1, 37.7 }));
            sequences.Add("S847", new List<double>(new double[] { 33.6, 30.8, 3, 16.4, 40.3, 34.7, 40.6 }));
            sequences.Add("S848", new List<double>(new double[] { 38.9, 45, 1.9, 26.7, 1, 23.6, 19.7 }));
            sequences.Add("S849", new List<double>(new double[] { 21.3, 32.6, 8.7, 34.5, 38.8, 15.4, 21.4 }));
            sequences.Add("S850", new List<double>(new double[] { 2.3, 0.5, 18.1, 28, 18.5, 25.9, 5.3 }));
            sequences.Add("S851", new List<double>(new double[] { 40.9, 15.3, 5.1, 4.6, 34.4, 5.4, 15.3 }));
            sequences.Add("S852", new List<double>(new double[] { 10, 26.8, 11.9, 28.6, 8.8, 29.9, 4.6 }));
            sequences.Add("S853", new List<double>(new double[] { 32, 29.4, 3.6, 46.9, 48, 26, 25 }));
            sequences.Add("S854", new List<double>(new double[] { 47, 33.1, 42.1, 12.9, 36.1, 20.4, 16.4 }));
            sequences.Add("S855", new List<double>(new double[] { 36.8, 8, 47.9, 45, 33.2, 26.6, 7.9 }));
            sequences.Add("S856", new List<double>(new double[] { 0.8, 46.7, 12, 20.6, 45.8, 16.7, 15.3 }));
            sequences.Add("S857", new List<double>(new double[] { 36.4, 21.2, 3.4, 45, 30.7, 22.5, 17.7 }));
            sequences.Add("S858", new List<double>(new double[] { 36.1, 18.7, 0.6, 1.1, 2.1, 39.5, 17.2 }));
            sequences.Add("S859", new List<double>(new double[] { 23.8, 43.6, 44.2, 25.6, 38.2, 10.5, 16.1 }));
            sequences.Add("S860", new List<double>(new double[] { 43.2, 23.8, 38.3, 44, 22.6, 20, 42 }));
            sequences.Add("S861", new List<double>(new double[] { 10.5, 26.9, 28.3, 33, 20.6, 25.8, 16.9 }));
            sequences.Add("S862", new List<double>(new double[] { 19.3, 10.8, 40.7, 11.7, 10.1, 0.6, 24 }));
            sequences.Add("S863", new List<double>(new double[] { 15, 38.2, 16.7, 47.1, 30.7, 32.4, 22.7 }));
            sequences.Add("S864", new List<double>(new double[] { 4.2, 25, 37.6, 33, 25.7, 23.1, 30.8 }));
            sequences.Add("S865", new List<double>(new double[] { 2, 36.8, 19.3, 15.1, 41.2, 30.2, 46.1 }));
            sequences.Add("S866", new List<double>(new double[] { 7.4, 46.8, 28.7, 33.9, 38.3, 29.2, 41.9 }));
            sequences.Add("S867", new List<double>(new double[] { 4.5, 38.6, 7.5, 1.3, 4.7, 15.1, 46.4 }));
            sequences.Add("S868", new List<double>(new double[] { 2.9, 15.1, 4.4, 13.9, 35.5, 44, 26.6 }));
            sequences.Add("S869", new List<double>(new double[] { 12.1, 43.7, 16.9, 0.7, 23.5, 5, 43 }));
            sequences.Add("S870", new List<double>(new double[] { 11.1, 1.4, 28.8, 12.6, 7.4, 45.2, 0.7 }));
            sequences.Add("S871", new List<double>(new double[] { 24.4, 41.2, 25.3, 41.7, 40.3, 38.5, 9.9 }));
            sequences.Add("S872", new List<double>(new double[] { 28.8, 18.6, 24.8, 30.6, 2.4, 19.3, 44.6 }));
            sequences.Add("S873", new List<double>(new double[] { 27.9, 45.8, 13.8, 36.6, 29.4, 12.8, 4.9 }));
            sequences.Add("S874", new List<double>(new double[] { 20.8, 9.4, 39.5, 25.1, 4.9, 11.4, 5.4 }));
            sequences.Add("S875", new List<double>(new double[] { 7.3, 47.5, 11.2, 47.3, 26.7, 8.2, 30 }));
            sequences.Add("S876", new List<double>(new double[] { 38, 10.5, 34.5, 45.6, 31, 30.2, 41.5 }));
            sequences.Add("S877", new List<double>(new double[] { 36.2, 12.9, 5.7, 26.9, 27.7, 25.1, 19.9 }));
            sequences.Add("S878", new List<double>(new double[] { 17.6, 7.6, 42.4, 46.7, 45.2, 4.9, 3.3 }));
            sequences.Add("S879", new List<double>(new double[] { 41.3, 47.4, 8.8, 16, 28.4, 14.4, 22.3 }));
            sequences.Add("S880", new List<double>(new double[] { 20.3, 42, 5.7, 43.6, 23.3, 26.2, 33.9 }));
            sequences.Add("S881", new List<double>(new double[] { 18.1, 12.7, 43.5, 41.3, 4.3, 19.2, 34.8 }));
            sequences.Add("S882", new List<double>(new double[] { 40.1, 32.1, 42.5, 34.5, 13.7, 10.3, 34.9 }));
            sequences.Add("S883", new List<double>(new double[] { 8, 34.4, 11.5, 46, 9.9, 28.1, 34 }));
            sequences.Add("S884", new List<double>(new double[] { 25.8, 37, 17.5, 24.4, 47.7, 33.6, 43.7 }));
            sequences.Add("S885", new List<double>(new double[] { 5.8, 15.5, 34.8, 13.2, 25.3, 5.1, 22.1 }));
            sequences.Add("S886", new List<double>(new double[] { 42.5, 2.5, 10.1, 32.2, 26.3, 26.5, 31.7 }));
            sequences.Add("S887", new List<double>(new double[] { 11.1, 45.2, 1, 39.5, 43.9, 32.8, 0.2 }));
            sequences.Add("S888", new List<double>(new double[] { 28.2, 0.1, 37.2, 2.2, 4, 43.4, 40 }));
            sequences.Add("S889", new List<double>(new double[] { 15.8, 39.4, 38.1, 20.1, 30.4, 2.7, 19.2 }));
            sequences.Add("S890", new List<double>(new double[] { 38.3, 37.2, 0.9, 44.3, 35, 28.8, 41.2 }));
            sequences.Add("S891", new List<double>(new double[] { 34.6, 4.4, 33.3, 45.7, 1.5, 6.8, 1.2 }));
            sequences.Add("S892", new List<double>(new double[] { 31.5, 0.2, 1.3, 46, 16.8, 37.1, 37.7 }));
            sequences.Add("S893", new List<double>(new double[] { 23.4, 40.5, 19.3, 44.4, 27.4, 23.2, 9.3 }));
            sequences.Add("S894", new List<double>(new double[] { 16.5, 48, 45.3, 34.3, 32.9, 18.9, 41.9 }));
            sequences.Add("S895", new List<double>(new double[] { 40.4, 28.7, 0.2, 24.7, 37.2, 20.5, 4.7 }));
            sequences.Add("S896", new List<double>(new double[] { 31, 15.8, 45.4, 1.3, 47.8, 46.1, 17.2 }));
            sequences.Add("S897", new List<double>(new double[] { 30.2, 1.1, 15.6, 30.4, 25, 25.4, 29.9 }));
            sequences.Add("S898", new List<double>(new double[] { 29.1, 35.9, 41, 47.8, 5.2, 42, 17.2 }));
            sequences.Add("S899", new List<double>(new double[] { 14.3, 36.8, 39.8, 9.1, 20.5, 26.1, 5.9 }));
              */
                      sequences.Add("S906", new List<double>(new double[] { 5.6, 37, 3.9, 8, 31.4, 33.5, 20.3 }));
            sequences.Add("S922", new List<double>(new double[] { 34.4, 20.6, 18.7, 11, 10.7, 22.9, 25.9 }));
            sequences.Add("S941", new List<double>(new double[] { 42.9, 17.7, 40.1, 39.4, 20.5, 0.4, 4.1 }));
            sequences.Add("S974", new List<double>(new double[] { 14.9, 36.9, 29.2, 11.9, 40.3, 19.3, 40.4 }));
            sequences.Add("S991", new List<double>(new double[] { 2.3, 36.9, 47.6, 18.7, 23.7, 14.4, 46.1 }));

            
            
            /*
            
            sequences.Add("S900", new List<double>(new double[] { 42.9, 1.5, 35.5, 42.5, 41, 2.3, 26.9 }));
            sequences.Add("S901", new List<double>(new double[] { 40.6, 0.7, 41.5, 12.8, 45.4, 37.4, 6.2 }));
            sequences.Add("S902", new List<double>(new double[] { 5.8, 1.3, 23.4, 23.7, 27.4, 19.8, 21.1 }));
            sequences.Add("S903", new List<double>(new double[] { 40, 24.6, 6.1, 10.1, 38.9, 38.5, 37.8 }));
            sequences.Add("S904", new List<double>(new double[] { 18.1, 12.8, 47.6, 4.6, 17.1, 3.8, 17.9 }));
            sequences.Add("S905", new List<double>(new double[] { 43.7, 19.5, 26.3, 27.8, 30.1, 16.4, 45.9 }));
            sequences.Add("S906", new List<double>(new double[] { 5.6, 37, 3.9, 8, 31.4, 33.5, 20.3 }));
            sequences.Add("S907", new List<double>(new double[] { 2.6, 11.5, 24, 11.5, 24.3, 32.2, 40.9 }));
            sequences.Add("S908", new List<double>(new double[] { 16.8, 5.9, 21.1, 39.7, 0.8, 4.1, 28.8 }));
            sequences.Add("S909", new List<double>(new double[] { 12.8, 41.7, 44.5, 20, 8.1, 41.4, 37 }));
            sequences.Add("S910", new List<double>(new double[] { 44.2, 46.4, 32.1, 13.5, 22.4, 41.7, 3.6 }));
            sequences.Add("S911", new List<double>(new double[] { 41.6, 10.3, 1.4, 21.3, 46, 15.3, 19.6 }));
            sequences.Add("S912", new List<double>(new double[] { 33.5, 3.3, 0, 16.6, 12.1, 19.1, 18.5 }));
            sequences.Add("S913", new List<double>(new double[] { 47.9, 25.9, 6.4, 18.3, 11.9, 13.7, 25.7 }));
            sequences.Add("S914", new List<double>(new double[] { 20, 6, 35.7, 4.9, 32.9, 6, 5.2 }));
            sequences.Add("S915", new List<double>(new double[] { 33.5, 38.1, 46.9, 45.6, 3.8, 10.9, 34.3 }));
            sequences.Add("S916", new List<double>(new double[] { 43.3, 38, 18.8, 0.8, 33.7, 20.2, 14.4 }));
            sequences.Add("S917", new List<double>(new double[] { 7.5, 6.6, 26.4, 45.9, 47.9, 28.9, 9.4 }));
            sequences.Add("S918", new List<double>(new double[] { 7, 1.4, 0, 15.4, 10.4, 12.2, 39.9 }));
            sequences.Add("S919", new List<double>(new double[] { 26.8, 13.4, 16, 42.3, 27.9, 17.7, 30.9 }));
            sequences.Add("S920", new List<double>(new double[] { 42, 26, 20.5, 28.4, 17.6, 21.5, 15 }));
            sequences.Add("S921", new List<double>(new double[] { 5.2, 45.7, 28.5, 6.3, 0.5, 44.1, 11.8 }));
            sequences.Add("S922", new List<double>(new double[] { 34.4, 20.6, 18.7, 11, 10.7, 22.9, 25.9 }));
            sequences.Add("S923", new List<double>(new double[] { 45.8, 22.2, 39.8, 15.8, 3.8, 12, 4.1 }));
            sequences.Add("S924", new List<double>(new double[] { 11.1, 47.8, 12.4, 20.4, 22.9, 23.9, 0.3 }));
            sequences.Add("S925", new List<double>(new double[] { 4.1, 0.7, 33.7, 43.7, 29.3, 17.3, 32.9 }));
            sequences.Add("S926", new List<double>(new double[] { 21.2, 14.7, 14.5, 46.9, 23.8, 1, 40.9 }));
            sequences.Add("S927", new List<double>(new double[] { 26.6, 39.5, 30.8, 20.6, 37.5, 18.9, 26.6 }));
            sequences.Add("S928", new List<double>(new double[] { 32.8, 19.8, 6.5, 18, 10.1, 9.8, 1.7 }));
            sequences.Add("S929", new List<double>(new double[] { 37.1, 11.7, 23.6, 34.4, 43.8, 45.1, 11.6 }));
            sequences.Add("S930", new List<double>(new double[] { 32.8, 43.3, 12.5, 5.4, 19.4, 22, 22.8 }));
            sequences.Add("S931", new List<double>(new double[] { 8.1, 42.8, 5.4, 22.2, 46.6, 28.4, 24.9 }));
            sequences.Add("S932", new List<double>(new double[] { 11.4, 18.1, 36, 30.9, 28.9, 32.3, 1.1 }));
            sequences.Add("S933", new List<double>(new double[] { 28, 13.7, 41.3, 26.2, 41, 16.5, 35.5 }));
            sequences.Add("S934", new List<double>(new double[] { 11.8, 43.6, 2.2, 32.3, 14.6, 25.6, 23.7 }));
            sequences.Add("S935", new List<double>(new double[] { 1.3, 47.7, 39, 42.3, 1.4, 13.9, 2.2 }));
            sequences.Add("S936", new List<double>(new double[] { 47.7, 39.7, 30.4, 31.6, 27.8, 18.9, 37 }));
            sequences.Add("S937", new List<double>(new double[] { 22.7, 7, 13.8, 42.8, 32.6, 8, 35.8 }));
            sequences.Add("S938", new List<double>(new double[] { 1.5, 33.6, 29.7, 16.6, 38.7, 4.5, 13.5 }));
            sequences.Add("S939", new List<double>(new double[] { 1.8, 32, 12.6, 20.9, 19.6, 33, 11.2 }));
            sequences.Add("S940", new List<double>(new double[] { 21.7, 25.8, 37.4, 39.3, 45.9, 10.9, 35.8 }));
            sequences.Add("S941", new List<double>(new double[] { 42.9, 17.7, 40.1, 39.4, 20.5, 0.4, 4.1 }));
            sequences.Add("S942", new List<double>(new double[] { 41.1, 15.9, 47.6, 25.5, 12.7, 22.3, 11.6 }));
            sequences.Add("S943", new List<double>(new double[] { 2.4, 22, 45, 8, 35.2, 35.7, 31 }));
            sequences.Add("S944", new List<double>(new double[] { 16.2, 45.4, 37.6, 10.9, 40, 14.6, 28.1 }));
            sequences.Add("S945", new List<double>(new double[] { 18.8, 29.2, 15.1, 17.1, 9.6, 46.2, 47 }));
            sequences.Add("S946", new List<double>(new double[] { 13.7, 36.7, 24.7, 32.1, 45.7, 40.2, 25.7 }));
            sequences.Add("S947", new List<double>(new double[] { 31.9, 16.5, 8.6, 29.9, 34.5, 30.5, 29.6 }));
            sequences.Add("S948", new List<double>(new double[] { 13.8, 39.9, 42.5, 15.2, 0.7, 27.8, 25.7 }));
            sequences.Add("S949", new List<double>(new double[] { 11.3, 2.4, 34, 24.1, 45.7, 11.1, 19.4 }));
            sequences.Add("S950", new List<double>(new double[] { 3.4, 5.3, 30.9, 32.2, 8.3, 6.1, 19.1 }));
            sequences.Add("S951", new List<double>(new double[] { 27.2, 30.1, 31.9, 40.6, 20.3, 14.3, 44.8 }));
            sequences.Add("S952", new List<double>(new double[] { 45.9, 29.7, 30, 11.7, 34.1, 19.8, 39.6 }));
            sequences.Add("S953", new List<double>(new double[] { 20.6, 29.3, 46.4, 46.8, 28.7, 24.5, 16 }));
            sequences.Add("S954", new List<double>(new double[] { 3.4, 43.4, 8.1, 17.3, 44.7, 33.6, 11.2 }));
            sequences.Add("S955", new List<double>(new double[] { 9.9, 31.6, 3.8, 31.5, 29.8, 32.8, 29.4 }));
            sequences.Add("S956", new List<double>(new double[] { 0.3, 14.9, 37, 34.2, 5.2, 36.1, 23.3 }));
            sequences.Add("S957", new List<double>(new double[] { 35.1, 39.4, 14, 45.2, 17.5, 39.4, 34.5 }));
            sequences.Add("S958", new List<double>(new double[] { 15.3, 38.6, 14.3, 8.1, 14.7, 33.9, 47.5 }));
            sequences.Add("S959", new List<double>(new double[] { 38.2, 37.9, 7.2, 41.1, 26.4, 5.5, 23.4 }));
            sequences.Add("S960", new List<double>(new double[] { 5.6, 10.7, 25.2, 32.2, 9.2, 15.3, 11.9 }));
            sequences.Add("S961", new List<double>(new double[] { 1.9, 5.9, 37, 34.2, 33.2, 34.6, 1.3 }));
            sequences.Add("S962", new List<double>(new double[] { 18.3, 32.2, 35.5, 32.9, 24.8, 15.6, 14 }));
            sequences.Add("S963", new List<double>(new double[] { 8.7, 30.5, 19.9, 31.8, 15.4, 37.7, 10.9 }));
            sequences.Add("S964", new List<double>(new double[] { 35.5, 2.6, 11.5, 18.6, 21, 17.3, 35.5 }));
            sequences.Add("S965", new List<double>(new double[] { 35.6, 27.5, 21.4, 31.1, 12.2, 9, 38.4 }));
            sequences.Add("S966", new List<double>(new double[] { 47, 46.9, 22.1, 24, 43.5, 31.8, 42.6 }));
            sequences.Add("S967", new List<double>(new double[] { 6.6, 35, 13.4, 23.9, 47.6, 15.2, 32.9 }));
            sequences.Add("S968", new List<double>(new double[] { 27.8, 38.3, 34.1, 41.6, 22.2, 41, 10.7 }));
            sequences.Add("S969", new List<double>(new double[] { 29, 6.7, 2, 33.3, 43.6, 44.1, 24.6 }));
            sequences.Add("S970", new List<double>(new double[] { 3.2, 9, 3.6, 42.4, 27.8, 15.2, 14.7 }));
            sequences.Add("S971", new List<double>(new double[] { 10.6, 23.5, 20, 8.4, 28.4, 12, 18.8 }));
            sequences.Add("S972", new List<double>(new double[] { 40, 45.6, 11.4, 32.8, 4.5, 3.5, 15.6 }));
            sequences.Add("S973", new List<double>(new double[] { 40.5, 37.3, 9.1, 13.8, 1.6, 47.5, 6.2 }));
            sequences.Add("S974", new List<double>(new double[] { 14.9, 36.9, 29.2, 11.9, 40.3, 19.3, 40.4 }));
            sequences.Add("S975", new List<double>(new double[] { 38, 15.2, 3, 37.9, 3.6, 31.4, 25.4 }));
            sequences.Add("S976", new List<double>(new double[] { 15.7, 5.7, 31.2, 6.5, 40.1, 0, 47.4 }));
            sequences.Add("S977", new List<double>(new double[] { 31.4, 41.1, 18.5, 19.3, 38.4, 42.8, 43.1 }));
            sequences.Add("S978", new List<double>(new double[] { 47.6, 47.5, 27.7, 46, 37, 23.2, 31.9 }));
            sequences.Add("S979", new List<double>(new double[] { 25.7, 29.5, 8.5, 3.7, 16, 20.4, 8.9 }));
            sequences.Add("S980", new List<double>(new double[] { 39, 27.9, 2.9, 34.8, 21, 19.2, 2.5 }));
            sequences.Add("S981", new List<double>(new double[] { 6.7, 20.9, 44.9, 14.7, 8.8, 19.5, 23.8 }));
            sequences.Add("S982", new List<double>(new double[] { 47.6, 3.1, 43.3, 15.3, 22.5, 14.9, 16.9 }));
            sequences.Add("S983", new List<double>(new double[] { 36.3, 40.2, 4.4, 21.6, 11.7, 31.8, 45.6 }));
            sequences.Add("S984", new List<double>(new double[] { 45.8, 30.9, 24, 37.1, 4.6, 39.3, 44.5 }));
            sequences.Add("S985", new List<double>(new double[] { 4.3, 43.1, 14.1, 7.5, 11.2, 25.3, 7.5 }));
            sequences.Add("S986", new List<double>(new double[] { 32.6, 36, 40.8, 16.6, 10.2, 11, 3.5 }));
            sequences.Add("S987", new List<double>(new double[] { 16.2, 39.9, 42.9, 23.7, 36.2, 29.9, 15.9 }));
            sequences.Add("S988", new List<double>(new double[] { 47.1, 40.3, 9.4, 45.6, 47.8, 22, 41.5 }));
            sequences.Add("S989", new List<double>(new double[] { 47.6, 16.1, 46.9, 0.4, 43.7, 29, 13 }));
            sequences.Add("S990", new List<double>(new double[] { 37.3, 13.1, 8.5, 43.7, 7.7, 8.2, 25.7 }));
            sequences.Add("S991", new List<double>(new double[] { 2.3, 36.9, 47.6, 18.7, 23.7, 14.4, 46.1 }));
            sequences.Add("S992", new List<double>(new double[] { 3.2, 0.8, 12.8, 15.9, 34.8, 40.1, 14.3 }));
            sequences.Add("S993", new List<double>(new double[] { 9.8, 6.8, 36.3, 21, 43.1, 4.7, 4.9 }));
            sequences.Add("S994", new List<double>(new double[] { 24.2, 21.8, 23.1, 33.7, 38.4, 5.1, 6.1 }));
            sequences.Add("S995", new List<double>(new double[] { 41.1, 31.6, 40.6, 7, 16.6, 31.3, 46.4 }));
            sequences.Add("S996", new List<double>(new double[] { 34.5, 29.6, 0.5, 36.2, 43.7, 34.9, 26.5 }));
            sequences.Add("S997", new List<double>(new double[] { 28.8, 43.5, 47.7, 23.2, 38.3, 12.5, 16.8 }));
            sequences.Add("S998", new List<double>(new double[] { 25.3, 29.4, 20, 18.4, 11.4, 19.6, 33.1 }));
            sequences.Add("S999", new List<double>(new double[] { 47.9, 35.5, 44.3, 43.8, 33.1, 38.2, 20.9 }));
            sequences.Add("S1000", new List<double>(new double[] { 13.8, 32.5, 45.4, 32.7, 30.1, 23.1, 45.2 }));
            */
/*
            sequences.Add("S1001", new List<double>(new double[] { 23.7, 40.7, 23.5, 36.5, 30.7, 29.2, 23.4 }));
            sequences.Add("S1002", new List<double>(new double[] { 2.7, 16.1, 4.1, 17.3, 46.9, 43.6, 6.5 }));
            sequences.Add("S1003", new List<double>(new double[] { 31.6, 21.8, 30.5, 35.1, 25.8, 18, 10.3 }));
            sequences.Add("S1004", new List<double>(new double[] { 31.3, 28, 5.3, 45, 20.1, 11.2, 41.7 }));
            sequences.Add("S1005", new List<double>(new double[] { 24.3, 30, 12.6, 39.9, 6.2, 9.3, 0.9 }));
            sequences.Add("S1006", new List<double>(new double[] { 36.8, 27.7, 29.9, 28.5, 40.3, 9.6, 16.2 }));
            sequences.Add("S1007", new List<double>(new double[] { 30.5, 44.5, 20.6, 24.8, 1, 18.1, 21.1 }));
            sequences.Add("S1008", new List<double>(new double[] { 20.5, 17, 29.3, 24, 34.8, 14.5, 19.4 }));
            sequences.Add("S1009", new List<double>(new double[] { 34.8, 39.1, 30.9, 12.8, 40.4, 12.1, 16.3 }));
            sequences.Add("S1010", new List<double>(new double[] { 42.2, 32.7, 47.2, 40.5, 29.3, 17.8, 12.2 }));
            sequences.Add("S1011", new List<double>(new double[] { 21.4, 25.1, 44.5, 17.3, 4.9, 20.8, 0.1 }));
            sequences.Add("S1012", new List<double>(new double[] { 35.3, 43.4, 18, 45.4, 47.6, 32.2, 25.5 }));
            sequences.Add("S1013", new List<double>(new double[] { 7.1, 35.4, 25.5, 22.7, 2.1, 45.3, 19.7 }));
            sequences.Add("S1014", new List<double>(new double[] { 6.1, 5.5, 14.5, 22.7, 19.6, 4, 37.1 }));
            sequences.Add("S1015", new List<double>(new double[] { 0.8, 25.8, 45.2, 45.4, 27.9, 40.2, 47.7 }));
            sequences.Add("S1016", new List<double>(new double[] { 29, 36.3, 34.3, 25.9, 26, 6, 12.3 }));
            sequences.Add("S1017", new List<double>(new double[] { 13.4, 40.9, 15.3, 10, 36.8, 35.9, 11.3 }));
            sequences.Add("S1018", new List<double>(new double[] { 43.7, 25, 0.6, 22.7, 37, 13.5, 7.3 }));
            sequences.Add("S1019", new List<double>(new double[] { 32.3, 35.4, 32.7, 2.1, 14.4, 44, 4.5 }));
            sequences.Add("S1020", new List<double>(new double[] { 36.8, 15.3, 18.9, 24.3, 20.7, 19.7, 29.2 }));
            sequences.Add("S1021", new List<double>(new double[] { 35.2, 28.8, 21.5, 30.8, 12.7, 25.7, 15.9 }));
            sequences.Add("S1022", new List<double>(new double[] { 45.4, 29.4, 29.6, 31.8, 19.5, 8, 2.1 }));
            sequences.Add("S1023", new List<double>(new double[] { 31.5, 34.3, 11.1, 26.6, 31.6, 30.2, 44.4 }));
            sequences.Add("S1024", new List<double>(new double[] { 34.5, 16.8, 6.5, 9, 18.3, 21.6, 42.3 }));
            sequences.Add("S1025", new List<double>(new double[] { 42.1, 21.4, 33.3, 37.9, 35.1, 36.8, 31.6 }));
            sequences.Add("S1026", new List<double>(new double[] { 42.8, 1.9, 3.8, 8.6, 28.7, 18.9, 9.1 }));
            sequences.Add("S1027", new List<double>(new double[] { 24.4, 27.6, 17.2, 11.1, 14.7, 46.2, 32.2 }));
            sequences.Add("S1028", new List<double>(new double[] { 18.6, 43.6, 39.7, 30.9, 9.4, 29.6, 1 }));
            sequences.Add("S1029", new List<double>(new double[] { 16.4, 37, 31.8, 14.2, 45.2, 40, 30.5 }));
            sequences.Add("S1030", new List<double>(new double[] { 41.8, 32.3, 34.3, 14.1, 1.3, 43.1, 1.1 }));
            sequences.Add("S1031", new List<double>(new double[] { 39.5, 38.3, 26.7, 24.5, 33.5, 19.7, 32.7 }));
            sequences.Add("S1032", new List<double>(new double[] { 28.1, 24.7, 0.5, 14, 32, 5.9, 0.4 }));
            sequences.Add("S1033", new List<double>(new double[] { 17.8, 28.8, 8.8, 46.3, 7, 22.1, 41.1 }));
            sequences.Add("S1034", new List<double>(new double[] { 3, 28.7, 12.9, 39.5, 14.9, 43.2, 35.5 }));
            sequences.Add("S1035", new List<double>(new double[] { 15.1, 27.3, 41.7, 22, 9.5, 3.1, 32.1 }));
            sequences.Add("S1036", new List<double>(new double[] { 0.1, 17.8, 45.6, 25, 18.7, 27.1, 41.4 }));
            sequences.Add("S1037", new List<double>(new double[] { 20.4, 36.5, 40.5, 0.3, 24.9, 30.7, 21.2 }));
            sequences.Add("S1038", new List<double>(new double[] { 29, 22, 2.4, 24.7, 41.5, 15.9, 16.4 }));
            sequences.Add("S1039", new List<double>(new double[] { 28.2, 41.4, 34.8, 40.1, 14, 16, 14 }));
            sequences.Add("S1040", new List<double>(new double[] { 0.8, 4.8, 3.5, 33.6, 34.7, 28.2, 23.7 }));
            
*/









            const int batchSize = 5;
            const int delayBetweenBatches = 2000; // 2 seconds delay
            int currentIndex = 0;

            while (currentIndex < sequences.Count)
            {


                // Initialize the multi-sequence learning experiment.
                MultiSequenceLearning experiment = new MultiSequenceLearning();

                // Run the experiment to build the prediction engine.
                var predictor = experiment.Run(sequences);

                currentIndex += batchSize;

                // Force garbage collection to free up memory
                GC.Collect();
                GC.WaitForPendingFinalizers();

                // Delay before processing the next batch
                if (currentIndex < sequences.Count) // Only delay if there are more sequences to process
                {
                    Console.WriteLine("Waiting for the next batch...");
                    await Task.Delay(delayBetweenBatches);
                }
            }

            Console.WriteLine("All sequences processed.");
            
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