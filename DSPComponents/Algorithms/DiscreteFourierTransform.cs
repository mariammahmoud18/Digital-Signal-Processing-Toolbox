using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;


namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            int k = InputTimeDomainSignal.Samples.Count;
            
            OutputFreqDomainSignal = new Signal(InputTimeDomainSignal.Samples, InputTimeDomainSignal.Periodic);
            OutputFreqDomainSignal.FrequenciesAmplitudes = new List<float>();
            OutputFreqDomainSignal.FrequenciesPhaseShifts = new List<float>();
            for (int i=0; i < k; i++)
            {
                
                Complex op = new Complex();
                for(int j=0; j<k; j++)
                {          
                   
                    op += InputTimeDomainSignal.Samples[j] *Complex.Exp(-Complex.ImaginaryOne * 2 * Math.PI * (j * i) / Convert.ToDouble(k));

                }

                float ampli = (float)Math.Sqrt((Math.Pow(op.Real, 2) + Math.Pow(op.Imaginary, 2)));
                OutputFreqDomainSignal.FrequenciesAmplitudes.Add(ampli);
                OutputFreqDomainSignal.FrequenciesPhaseShifts.Add((float)Math.Atan2(op.Imaginary, op.Real));
            }
            
           

        }
    }
}

