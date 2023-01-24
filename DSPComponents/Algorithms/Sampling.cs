using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        public Signal testing { get; set; }
        public Signal FIRsignal { get; set; }



        public override void Run()
        {
            FIR lowFilter = new FIR();
            lowFilter.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
            lowFilter.InputFS = 8000;
            lowFilter.InputStopBandAttenuation = 50;
            lowFilter.InputCutOffFrequency = 1500;
            lowFilter.InputTransitionBand = 500;
            List<float> up = new List<float>();
            List<float> down = new List<float>();
            List<float> indcies = new List<float>();



            if (M == 0 && L != 0)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    up.Add(InputSignal.Samples[i]);
                    for (int z = 1; z < L; z++)
                    {
                        up.Add(0);

                    }
                }
                testing = new Signal(up, false);
                lowFilter.InputTimeDomainSignal = testing;
                lowFilter.Run();
                OutputSignal = lowFilter.OutputYn;
            }
            else if (M != 0 && L == 0)
            {
                lowFilter.InputTimeDomainSignal = InputSignal;
                lowFilter.Run();
                FIRsignal = lowFilter.OutputYn;
                for (int i = 0; i < FIRsignal.Samples.Count; i += M)
                {
                    down.Add(FIRsignal.Samples[i]);

                }
                OutputSignal = new Signal(down,countIndcies(down), false);
            }
            else if (M != 0 && L != 0)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    up.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L - 1; j++)
                    {
                        up.Add(0);

                    }
                }
                testing = new Signal(up, false);
                lowFilter.InputTimeDomainSignal = testing;
                lowFilter.Run();
                FIRsignal = lowFilter.OutputYn;
                for (int i = 0; i < FIRsignal.Samples.Count; i += M)
                {
                    down.Add(FIRsignal.Samples[i]);

                }
                OutputSignal = new Signal(down, countIndcies(down), false);
            }
            else
            {
                Console.WriteLine("Error Found");
            }


            // throw new NotImplementedException();

        }
        public List<int> countIndcies(List<float>l1)
        {
            List<int> l = new List<int>();
            for(int i=0; i<l1.Count; i++)
            {
                l.Add(i);
            }
            return l;
        }
    }

}