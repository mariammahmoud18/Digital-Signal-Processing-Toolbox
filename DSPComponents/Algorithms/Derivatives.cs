using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        {
            List<float> firstdriv = new List<float>();
            List<float> seconddriv = new List<float>();
            for(int i=0; i< InputSignal.Samples.Count-1; i++)
            {
                if (i == 0)
                {
                    firstdriv.Add(InputSignal.Samples[i] - 0);
                    seconddriv.Add(InputSignal.Samples[i + 1] - (2 * InputSignal.Samples[i]));
                }
                else if (i == (InputSignal.Samples.Count - 1))
                {
                    seconddriv.Add(InputSignal.Samples[i - 1] - (2 * InputSignal.Samples[i]));
                    firstdriv.Add(InputSignal.Samples[i] - InputSignal.Samples[i - 1]);
                }
                else
                {
                    firstdriv.Add(InputSignal.Samples[i] - InputSignal.Samples[i - 1]);
                    seconddriv.Add(InputSignal.Samples[i + 1] + InputSignal.Samples[i - 1] - (2 * InputSignal.Samples[i]));
                }
                
            }
           
            FirstDerivative = new Signal(firstdriv, false);
            SecondDerivative = new Signal(seconddriv, false);
                
          
        }
    }
}
