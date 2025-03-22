using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoCortexApi.Entities;
using System.Collections.Generic;
using NeoCortexApi.Classifiers;

namespace KNNImplementation
{
    public interface IClassifier
    {
        
        public List<string> Classifier(List<List<double>> testingFeatures, List<List<double>> trainingFeatures, List<string> trainingLabels, int k);

        public double CalculateEuclideanDistance(List<double> testFeature, List<double> trainFeature);

        public List<SequenceDataEntry> LoadDataset(string jsonFilePath);
        






    }
}
