//The unit test is written by Global Variables

using KNNImplementation;
using System;

namespace KNNUnitTest
{
    [TestClass]
    public class UnitTest
    {
        Classifierleaning classifierleaning = new Classifierleaning();
        KNNClassifier kNNClassifier = new KNNClassifier();
        List<List<double>> trainingFeatures = new List<List<double>>();
        List<string> trainingLabels = new List<string>();
        List<List<double>> unknownFeature = new List<List<double>>();
        int K;

        [TestInitialize]
        public void Initialize()
        {
            classifierleaning = new Classifierleaning();
            kNNClassifier = new KNNClassifier();
            trainingFeatures = new List<List<double>>()
            {
                new List<double> { 8326, 9130, 9478, 9662, 9706, 9732, 9753, 9854, 9955, 10107, 10165, 10294, 10379, 10442, 10499, 10698, 10800, 10849, 10857, 10924 },
                new List<double> { 8870, 9787, 9970, 10025, 10070, 10085, 10136, 10197, 10208, 10241, 10315, 10352, 10468, 10740, 10906, 11002, 11142, 11159, 11204, 11475 },
                new List<double> { 9428, 10916, 11045, 11149, 11180, 11224, 11320, 11425, 11498, 11563, 11587, 11630, 11699, 11836, 11853, 11896, 11997, 12228, 12383, 12509 }
            };
            trainingLabels = new List<string> { "S1", "S2", "S3" };
            unknownFeature = new List<List<double>>()
            {
                new List<double> { 7588, 8649, 8891, 8965, 9370, 9661, 9933, 10044, 10123, 10151, 10220, 10292, 10344, 10354, 10387, 10460, 10761, 10778, 10843, 11105 },

            };
            K = 1;
            
        }
        [TestCleanup]
        public void Cleanup()
        {

        }
        [TestMethod]
        public void TestKNNClassifier_Kiswithinrange()
        {
            ///  Test Case to Handle when unknown feature or testing Feature is null 

            if (K <= trainingFeatures.Count)
            {
                
                // Act
                List<string> predictedLabels = kNNClassifier.Classifier(unknownFeature, trainingFeatures, trainingLabels, K);
                // Assert
                Assert.AreEqual("S1", predictedLabels[0]);
            }

            else
            {
              
                /// Act & Assert
                Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                {
                    kNNClassifier.Classifier(unknownFeature, trainingFeatures, trainingLabels, K);
                });

            }

        }

        [TestMethod]
        public void TestKNNClassifier_unknownfeatureortestingfeatureisnull()
        {
            /// Test Case to Handle when unknown feature or testing Feature is null 
            if (unknownFeature == null)
            {
                /// Act & Assert
                Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                {
                    kNNClassifier.Classifier(unknownFeature, trainingFeatures, trainingLabels, K);
                });
            }
            else
            {
                // Act
                List<string> predictedLabels = kNNClassifier.Classifier(unknownFeature, trainingFeatures, trainingLabels, K);
                // Assert
                Assert.AreEqual("S1", predictedLabels[0]);
            }
        }

        [TestMethod]
        public void TestKNNClassifier_trainingFeatureisnull()
        {
            // Test Case to Handle when trainingFeature is null 

            if (trainingFeatures == null)
            {
                /// Act & Assert
                Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                {
                    kNNClassifier.Classifier(unknownFeature, trainingFeatures, trainingLabels, K);
                });
            }
            else
            {
                // Act
                List<string> predictedLabels = kNNClassifier.Classifier(unknownFeature, trainingFeatures, trainingLabels, K);
                // Assert
                Assert.AreEqual("S1", predictedLabels[0]);
            }

        }

        

    }
}