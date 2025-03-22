using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    public interface IClassifier<TIN, TOUT>
    {
        void Learn(TIN input, Cell[] output);
        TIN GetPredictedInputValue(Cell[] predictiveCells);
        List<ClassifierResult<TIN>> GetPredictedInputValues(int[] cellIndices, short howMany = 1);
    }
}