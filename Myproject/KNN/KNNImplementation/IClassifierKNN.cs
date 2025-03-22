/// The interface is written by Gloabl Variable
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
    public interface IClassifierKNN
    {

        public List<string> Classifier(List<List<double>> testingFeatures, List<List<double>> trainingFeatures, List<string> trainingLabels, int k);

        public List<SequenceDataEntry> LoadDataset(string jsonFilePath);


    }
}