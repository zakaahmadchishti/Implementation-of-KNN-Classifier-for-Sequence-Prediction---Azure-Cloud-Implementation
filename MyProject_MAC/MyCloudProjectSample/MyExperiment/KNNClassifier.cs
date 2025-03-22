using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MyCloudProject.Common;


namespace KNNImplementation
{

public class KNNClassifier : IClassifier<List<List<double>>, List<string>>
    {
        private List<List<double>> _trainingFeatures;
        private List<string> _trainingLabels;
        private int _k;

        public KNNClassifier(int k)
        {
            _k = k;
        }

        public void Learn(List<List<double>> input, Cell[] output)
        {
            _trainingFeatures = input;
            _trainingLabels = output.Select(cell => cell.Label).ToList();
        }

        public List<List<double>> GetPredictedInputValue(Cell[] predictiveCells)
        {
            var calculateDistance = new DistanceCalculator();
            var predictedInputs = new List<List<double>>(); // Fixed initialization

            foreach (var predictiveCell in predictiveCells)
            {
                var nearestNeighbors = new IndexAndDistance[_trainingFeatures.Count];

                for (int i = 0; i < _trainingFeatures.Count; i++)
                {
                    double distance = calculateDistance.CalculateEuclideanDistance(predictiveCell.Features, _trainingFeatures[i]);
                    nearestNeighbors[i] = new IndexAndDistance { idx = i, dist = distance };
                }

                Array.Sort(nearestNeighbors);
                int resultIndex = Vote(nearestNeighbors, _trainingLabels, _k);

                // Wrap the result in a List<List<double>> as per the interface contract
                predictedInputs.Add(_trainingFeatures[resultIndex]);
            }

            return predictedInputs; // Fixed return type
        }

        public List<ClassifierResult<List<List<double>>>> GetPredictedInputValues(int[] cellIndices, short howMany = 1)
        {
            var predictedResults = new List<ClassifierResult<List<List<double>>>>();
            var calculateDistance = new DistanceCalculator();

            foreach (int cellIndex in cellIndices)
            {
                var nearestNeighbors = new IndexAndDistance[_trainingFeatures.Count];
                var predictiveCell = _trainingFeatures[cellIndex];

                for (int i = 0; i < _trainingFeatures.Count; i++)
                {
                    double distance = calculateDistance.CalculateEuclideanDistance(predictiveCell, _trainingFeatures[i]);
                    nearestNeighbors[i] = new IndexAndDistance { idx = i, dist = distance };
                }

                Array.Sort(nearestNeighbors);

                int resultIndex = Vote(nearestNeighbors, _trainingLabels, _k);
                var predictedLabel = _trainingLabels[resultIndex];

                // Wrap the predicted input in a ClassifierResult<List<List<double>>>
                predictedResults.Add(new ClassifierResult<List<List<double>>>
                {
                    PredictedInput = new List<List<double>> { _trainingFeatures[resultIndex] },
                    PredictedLabel = predictedLabel,
                    Confidence = CalculateConfidence(nearestNeighbors, _trainingLabels, _k)
                });
            }

            return predictedResults.Take(howMany).ToList(); // Return type adjusted to match IClassifier
        }

        private int Vote(IndexAndDistance[] nearestNeighbors, List<string> trainingLabels, int k)
        {
            var votes = new Dictionary<string, int>();

            for (int i = 0; i < k; i++)
            {
                string neighborLabel = trainingLabels[nearestNeighbors[i].idx];
                if (!votes.ContainsKey(neighborLabel))
                    votes[neighborLabel] = 0;
                votes[neighborLabel]++;
            }

            string classWithMostVotes = votes.OrderByDescending(pair => pair.Value).First().Key;
            return trainingLabels.IndexOf(classWithMostVotes);
        }

        private double CalculateConfidence(IndexAndDistance[] nearestNeighbors, List<string> trainingLabels, int k)
        {
            var votes = new Dictionary<string, int>();

            for (int i = 0; i < k; i++)
            {
                string neighborLabel = trainingLabels[nearestNeighbors[i].idx];
                if (!votes.ContainsKey(neighborLabel))
                    votes[neighborLabel] = 0;
                votes[neighborLabel]++;
            }

            string classWithMostVotes = votes.OrderByDescending(pair => pair.Value).First().Key;
            return (double)votes[classWithMostVotes] / k;
        }

        public double CalculateAccuracy(List<string> predictedLabels, List<string> actualLabels)
        {
            int correctPredictions = predictedLabels.Where((predictedLabel, index) => predictedLabel == actualLabels[index]).Count();
            return (double)correctPredictions / predictedLabels.Count * 100;
        }
    }
}

   
    public class ClassifierResult<TIN>
    {
        public TIN PredictedInput { get; set; }
        public string PredictedLabel { get; set; }
        public double Confidence { get; set; }
    }

    /// <summary>
    /// Represents a cell with features and a label, used for classification.
    /// </summary>
    public class Cell
    {
        public List<double> Features { get; set; }
        public string Label { get; set; }
    }

    /// <summary>
    /// Represents an entry containing the index and distance for a training feature, used for comparison in the KNN algorithm.
    /// </summary>
    public class IndexAndDistance : IComparable<IndexAndDistance>
    {
        public int idx;
        public double dist;

        public int CompareTo(IndexAndDistance other) => dist.CompareTo(other.dist);
    }

    /// <summary>
    /// Provides methods for calculating various distances between features, used in the KNN algorithm.
    /// </summary>
    public class DistanceCalculator
    {
        public double CalculateEuclideanDistance(List<double> testFeature, List<double> trainFeature)
        {
            ValidateFeatureData(testFeature, trainFeature);
            return Math.Sqrt(testFeature.Zip(trainFeature, (test, train) => Math.Pow(test - train, 2)).Sum());
        }

        private void ValidateFeatureData(List<double> testFeature, List<double> trainFeature)
        {
            if (testFeature == null || trainFeature == null)
                throw new ArgumentNullException("Both testFeature and trainFeature must not be null.");

            if (testFeature.Count != trainFeature.Count)
                throw new ArgumentException("testFeature and trainFeature must have the same length.");
        }
    }

    /// <summary>
    /// Represents a sequence data entry with sequence data and a name/label.
    /// </summary>
    public class SequenceDataEntry
    {
        /// <summary>
        /// Gets or sets the sequence data as a list of double values.
        /// </summary>
        public List<double> SequenceData { get; set; }

        /// <summary>
        /// Gets or sets the name or label of the sequence.
        /// </summary>
        public string SequenceName { get; set; }
    }


