// The namespace KNNimplementation is written by GLobal Variable
using NeoCortexEntities.NeuroVisualizer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

/*
 The Neocortex API generates three sequences of numbers, categorizing them as Even, Odd, or decimal numbers, which serve as predicted cells for creating a 
comprehensive dataset for model training.

The process begins with the utilization of the 'LoadDataset(Datasetfilepath)' method from class 'Classifierleaning', which enables the model to ingest and 
comprehend the dataset from a specified JSON file path. Following this, the dataset is partitioned into a 70-30 ratio using the
'SplitDataset(sequenceDataEntries, out trainingfeatures, out testing features, out traininglabels, out testinglabels)' method from same class 
'Classifierleaning'.

The 70% of the data is allocated for training the Classifier model, allowing it to discern intricate patterns and relationships inherent in the dataset.
Meanwhile, the remaining 30% is reserved for assessing the model's performance.

During the testing phase, the K-Nearest Neighbors (KNN) Classifier employs the 'Classifier(testingFeatures, trainingFeatures, trainingLabels, k: 3)' 
method from class 'KNNClassifier' to predict the labels of the testing data.

To evaluate the model's accuracy, the predicted labels are compare with the actual labels extracted from the training dataset. This comparison is
accomplished through the 'CalculateAccuracy(predictedLabels, testingLabels)' method from Class 'KNNClassifier', which quantifies the accuracy of the model's 
predictions, providing valuable insights into its efficacy and performance.
    
 For an Example: 

 we have Sample Data in a Dataset which we split in training and testing data

 training data = [
  {
    "SequenceName": "S1",
    "SequenceData": [8039, 8738, 9334, 9558, 9604, 9697, 9772, 9841, 9851, 9922, 9963, 10023, 10121, 10197, 10373, 10459, 10594, 10629, 10664, 11124]
  },
{
    "SequenceName": "S2",
    "SequenceData": [9051, 9075, 9133, 9178, 9365, 9448, 9481, 9599, 9635, 9740, 10032, 10224, 10281, 10762, 10778, 10934, 11143, 11306, 11494, 11763]
  },
{
    "SequenceName": "S3",
    "SequenceData": [10808, 10834, 11053, 11085, 11434, 11471, 11479, 11553, 11597, 11634, 11720, 11743, 11766, 11812, 11872, 11897, 11909, 12094, 12332, 12504]
  }, ...
]


 testing Data = [
{
    "SequenceName": "S1",
    "SequenceData": [7665, 8260, 8304, 8495, 9285, 9366, 9388, 9603, 9641, 9707, 9774, 9819, 9837, 10020, 10096, 10149, 10263, 10313, 10873, 10914]
  }
]


 Here's the verdict: The model has predicted the testing data as Class S1, representing sequence S1 SDR's closet to testing data SDR'S.

 The output includes the label class of the testing data and the accuracy of the model. 

*/

namespace KNNImplementation
{
    /// <summary>
    /// KNN class is designed to perform classification tasks on sequences derived from the SDR (Sparse Distributed Representation) dataset
    /// </summary>

    public class KNNClassifier : IClassifierKNN
    {

        /// <summary>
        /// Determines the class label by majority voting among the k nearest neighbors.
        /// </summary>
        /// <param name="nearestNeighbors">An array of IndexAndDistance objects representing the k nearest neighbors.</param>
        /// <param name="trainingLabels">The training label contain the class labels of training data.</param>
        /// <param name="k">The number of nearest neighbors to consider.</param>
        /// <returns>The class label with the most votes among the nearest neighbors.</returns>

        private int Vote(IndexAndDistance[] nearestNeighbors, List<string> trainingLabels, int k)
        {
            Dictionary<string, int> votes = new Dictionary<string, int>();

            foreach (string label in trainingLabels)
            {
                votes[label] = 0;
            }

            for (int i = 0; i < k; ++i)
            {
                string neighborLabel = trainingLabels[nearestNeighbors[i].idx];
                votes[neighborLabel]++;
            }

            // Find the class label with the most votes
            string classWithMostVotes = votes.OrderByDescending(pair => pair.Value).First().Key;

            Debug.WriteLine("");
            Debug.WriteLine($"  Predicted class for test data: {(classWithMostVotes == "S1" ? " S1 = {2, 4, 6, 8, 10, 12, 14} (Even)  Sequence Match"
                : (classWithMostVotes == "S2" ? "S2 = {3, 5, 7, 9, 11, 13, 15} (Odd)  Sequence Match" :
                (classWithMostVotes == "S3" ? " S3 = { 4.5, 11.4, 12.8, 15.5, 16.6, 17.7 } (Neither Odd nor Even)  Sequence Match"
                : "MisMatch")))}");

            Debug.WriteLine("");
            return trainingLabels.IndexOf(classWithMostVotes);
        }

        /// <summary>
        /// Classifies the unknown SDR based on the k-nearest neighbors in the training data using the KNN algorithm.
        /// </summary>
        /// <param name="testingFeatures">The Featurees of testing data containing testing SDR value to be classified.</param>
        /// <param name="trainingFeatures">The Features of training data containing training SDRs value.</param>
        /// <param name="trainingLabels">The training label contain the class labels of training data.</param>
        /// <param name="k">The number of nearest neighbors to consider in the classification.</param>
        /// <returns> The list of predicted labels for the testing features.</returns>

        public List<string> Classifier(List<List<double>> testingFeatures, List<List<double>> trainingFeatures, List<string> trainingLabels, int k)
        {
            Debug.WriteLine("");
            Debug.WriteLine("Starting KNN Classifier on Sparse Distribution Representation");
            Debug.WriteLine("");
            List<string> predictedLabels = new List<string>();
            CalculateDistance calculateDistance = new CalculateDistance();

            foreach (var testFeature in testingFeatures)
            {
                IndexAndDistance[] nearestNeighbors = new IndexAndDistance[trainingFeatures.Count];
                for (int i = 0; i < trainingFeatures.Count; i++)
                {
                    double distance = calculateDistance.CalculateEuclideanDistance(testFeature, trainingFeatures[i]);
                    //double distance = calculateDistance.CalculateManhattanDistance(testFeature, trainingFeatures[i]);
                    //double distance = calculateDistance.CalculateMinkowskiDistance(testFeature, trainingFeatures[i]);
                    nearestNeighbors[i] = new IndexAndDistance { idx = i, dist = distance };
                }

                Array.Sort(nearestNeighbors);

                // Display information for the k-nearest items
                Debug.WriteLine("   Nearest Features    /    Euclidean Distance      /     Class label   ");
                Debug.WriteLine("   ==================================================================   ");

                for (int i = 0; i < k; i++)
                {
                    int nearestIndex = nearestNeighbors[i].idx;
                    double nearestDistance = nearestNeighbors[i].dist;
                    string nearestClass = trainingLabels[nearestIndex];
                    Debug.WriteLine($"    ({trainingFeatures[nearestIndex][0]}, {trainingFeatures[nearestIndex][1]} )  :          " +
                        $"{nearestDistance}        :     {nearestClass}");
                }

                int result = Vote(nearestNeighbors, trainingLabels, k);
                predictedLabels.Add(trainingLabels[result]);
            }
            return predictedLabels;
        }

        /// <summary>
        /// Finding Accuracy of the CLassifier
        /// </summary>
        /// <param name="predictedLabels"></param>
        /// <param name="actualLabels"></param>
        /// <returns></returns> Retyrn the accuracy in Percentage 

        public void CalculateAccuracy(List<string> predictedLabels, List<string> actualLabels)
        {
            int correctPredictions = predictedLabels.Where((predictedLabel, index) => predictedLabel == actualLabels[index]).Count();
            double accuracy = (double)correctPredictions / predictedLabels.Count * 100;
            Debug.WriteLine($"Sequences Matching with accuray of : {accuracy}%");
            Debug.WriteLine("");

        }


        public List<SequenceDataEntry> LoadDataset(string jsonFilePath)
        {
            throw new NotImplementedException();

        }


    }

    /// <summary>
    /// Compares the instance to another based on distance
    /// </summary>
    /// <param name="other">The other IndexAndDistance instance to compare with.</param>
    /// <returns>

    public class IndexAndDistance : IComparable<IndexAndDistance>
    {
        /// <summary>
        /// Index of a training feature.
        /// </summary>
        public int idx;

        /// <summary>
        /// Distance to the testing Feature.
        /// </summary>
        public double dist;

        /// <summary>
        /// Compares this instance to another based on distance.
        /// </summary>
        public int CompareTo(IndexAndDistance other) => dist.CompareTo(other.dist);

    }

    /// <summary>
    /// Class to load dataset from the JSON file and split the dataset into training and testing data with respect to spliting ratio.
    /// </summary>
    public class Classifierleaning : IClassifierKNN
    {

        /// <summary>
        /// Loading Dataset from the JSON file 
        /// </summary>
        /// <param name="DatasetFilePath"></param>
        /// <returns></returns>
        public List<SequenceDataEntry> LoadDataset(string DatasetFilePath)
        {
            string jsonData = File.ReadAllText(DatasetFilePath);
            return JsonConvert.DeserializeObject<List<SequenceDataEntry>>(jsonData);
        }

        /// <summary>
        /// Spliting the dataset in training dataset and testing dataset based on Features and label or value and key
        /// </summary>
        /// <param name="sequenceDataList"></param>
        /// <param name="trainingFeatures"></param>
        /// <param name="trainingLabels"></param>
        /// <param name="testingFeatures"></param>
        /// <param name="testingLabels"></param>
        public static void SplitDataset(List<SequenceDataEntry> sequenceDataList, out List<List<double>> trainingFeatures,
            out List<string> trainingLabels, out List<List<double>> testingFeatures, out List<string> testingLabels, double Splitingratio)
        {
            trainingFeatures = new List<List<double>>();
            trainingLabels = new List<string>();
            testingFeatures = new List<List<double>>();
            testingLabels = new List<string>();
            Random rand = new Random();

            foreach (var entry in sequenceDataList)
            {
                string label = entry.SequenceName;
                List<double> features = entry.SequenceData;

                if (rand.NextDouble() < Splitingratio) // spliting ratio for training
                {
                    trainingFeatures.Add(new List<double>(features));
                    trainingLabels.Add(label);
                }
                else // remaining out of 1 is for testing
                {
                    testingFeatures.Add(new List<double>(features));
                    testingLabels.Add(label);
                }
            }


        }

        public List<string> Classifier(List<List<double>> testingFeatures, List<List<double>> trainingFeatures, List<string> trainingLabels, int k)
        {
            throw new NotImplementedException();
        }

    }

    /// <summary>
    /// Class Represents an entry in the dataset, containing a sequence name and its associated data.
    /// </summary>
    public class SequenceDataEntry
    {
        public required string SequenceName { get; set; }
        public required List<double> SequenceData { get; set; }
    }

    /// <summary>
    /// CLass contain three type of distance calculation method between two features point.
    /// </summary>
    public class CalculateDistance
    {

        /// <summary>
        /// Calculates the Euclidean distance between two Features.
        /// </summary>
        /// <param name="testFeature">Feature of test Data.</param>
        /// <param name="trainFeature">Feature of train Data.</param>
        /// <returns>The Euclidean distance between the two Feature of training data and testing data.</returns>
        public double CalculateEuclideanDistance(List<double> testFeature, List<double> trainFeature)
        {
            if (testFeature == null || trainFeature == null)
                throw new ArgumentNullException("Both testData and trainData must not be null.");

            if (testFeature.Count != trainFeature.Count)
                throw new ArgumentException("testData and trainData must have the same length.");

            double sumOfSquaredDifferences = 0.0;

            for (int i = 0; i < testFeature.Count; ++i)
            {
                double difference = testFeature[i] - trainFeature[i];
                sumOfSquaredDifferences += difference * difference;
            }

            return Math.Sqrt(sumOfSquaredDifferences);
        }

        /// <summary>
        /// Calculates the Manhattan distance between two Features.
        /// </summary>
        /// <param name="testFeature">Feature of test Data.</param>
        /// <param name="trainFeature">Feature of train Data.</param>
        /// <returns>The Euclidean distance between the two Feature of training data and testing data.</returns>
        public double CalculateManhattanDistance(List<double> testFeature, List<double> trainFeature)
        {
            if (testFeature == null || trainFeature == null)
                throw new ArgumentNullException("Both testData and trainData must not be null.");

            if (testFeature.Count != trainFeature.Count)
                throw new ArgumentException("testData and trainData must have the same length.");

            double sumOfAbsdiff = 0.0;

            for (int i = 0; i < testFeature.Count; ++i)
            {
                sumOfAbsdiff += Math.Abs(testFeature[i] - trainFeature[i]);
            }

            return Math.Sqrt(sumOfAbsdiff);
        }

        /// <summary>
        /// Calculates the Minkowski distance between two Feature.
        /// </summary>
        /// <param name="testFeature">Feature of test Data.</param>
        /// <param name="trainFeature">Feature of train Data.</param>
        /// <returns>The Euclidean distance between the two Feature of training data and testing data.</returns>
        public double CalculateMinkowskiDistance(List<double> testFeature, List<double> trainFeature, int p)
        {
            if (testFeature == null || trainFeature == null)
                throw new ArgumentNullException("Both testData and trainData must not be null.");

            if (testFeature.Count != trainFeature.Count)
                throw new ArgumentException("testData and trainData must have the same length.");

            double sumofpowdiff = 0.0;

            for (int i = 0; i < testFeature.Count; ++i)
            {
                double difference = Math.Abs(testFeature[i] - trainFeature[i]);
                sumofpowdiff += Math.Pow(difference, p);
            }

            return Math.Sqrt(sumofpowdiff);
        }


    }



}