using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            float maxi = -1000;
            float mini = 100000;
            float current = 0;
            List<float> result = new List<float>();
            for(int i=0; i<InputSignal.Samples.Count; i++)
            {
                current = InputSignal.Samples[i]; 
                if(current > maxi)
                    maxi = current;
                if (current < mini)
                    mini = current;
            }
            for(int i=0; i<InputSignal.Samples.Count;i++)
            {
                float x = InputSignal.Samples[i];
                float y = (x-mini)/(maxi-mini);
                result.Add(((InputMaxRange-InputMinRange)*(y))+InputMinRange);
            }
            OutputNormalizedSignal = new Signal(result,InputSignal.SamplesIndices, InputSignal.Periodic);
        }
    }
}
