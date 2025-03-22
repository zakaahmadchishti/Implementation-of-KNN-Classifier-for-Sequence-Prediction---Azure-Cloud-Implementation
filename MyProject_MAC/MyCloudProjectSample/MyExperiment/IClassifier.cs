using System.Collections.Generic;

public interface IClassifier<TIN, TOUT>
{
    /// <summary>
    /// Trains the classifier with the given input data and corresponding output labels.
    /// </summary>
    /// <param name="input">The input training data.</param>
    /// <param name="output">The output labels corresponding to the training data.</param>
    public void Learn(TIN input, Cell[] output);

    /// <summary>
    /// Predicts the input value based on the classifier's learned data using predictive cells.
    /// </summary>
    /// <param name="predictiveCells">The predictive cells to classify.</param>
    /// <returns>The predicted input value.</returns>
    TIN GetPredictedInputValue(Cell[] predictiveCells);

    /// <summary>
    /// Predicts multiple input values based on the classifier's learned data.
    /// </summary>
    /// <param name="cellIndices">Indices of the cells to classify.</param>
    /// <param name="howMany">The number of predicted values to return.</param>
    /// <returns>A list of predicted input values and their corresponding labels with confidence scores.</returns>
    List<ClassifierResult<TIN>> GetPredictedInputValues(int[] cellIndices, short howMany = 1);
}

/// <summary>
/// Represents a cell with features and a label, used for classification.
/// </summary>


/// <summary>
/// Represents a result from the classifier.
/// </summary>

