using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            float count = 0;
            int size = InputSignal.Samples.Count;
            List <float>result = new List<float> ();
            for(int i=0; i<InputSignal.Samples.Count; i++)
            {
                count += InputSignal.Samples[i];
            }
            float mean = count / size;
            for(int i=0; i<size; i++)
            {
                if (mean > 0)
                    result.Add(InputSignal.Samples[i] - mean);
                else
                    result.Add(InputSignal.Samples[i] + mean);


            }
            OutputSignal = new Signal(result,InputSignal.SamplesIndices ,InputSignal.Periodic);
        }
    }
}
