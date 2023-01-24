
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            List<int> indcies = new List<int>();
            List<float> values = new List<float>();
            bool periodc = InputSignal.Periodic;

            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                if (!periodc)
                    indcies.Add(InputSignal.SamplesIndices[i] - ShiftingValue);
                else
                    indcies.Add(InputSignal.SamplesIndices[i] + ShiftingValue);

            }
            OutputShiftedSignal = new Signal(InputSignal.Samples, false);
            OutputShiftedSignal.SamplesIndices = indcies;
        }
    }
}