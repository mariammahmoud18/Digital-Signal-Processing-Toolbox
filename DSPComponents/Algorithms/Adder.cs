using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
           for(int i=0; i<InputSignals.Count-1; i=1+2)
            {
                Signal signal = InputSignals[i];
                Signal signal2 = InputSignals[i+1];
                List<float> result = new List<float>();
             
                for(int j = 0; j<signal.Samples.Count; j++)
                {
                    result.Add(signal.Samples[j] + signal2.Samples[j]);
                }
                OutputSignal = new Signal(result, false);

            }
        }
    }
}
