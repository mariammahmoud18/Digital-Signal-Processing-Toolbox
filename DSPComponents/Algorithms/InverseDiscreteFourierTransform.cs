using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;


namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            int k = InputFreqDomainSignal.Frequencies.Count;
            float real;
            float imaginary;
            float op1;
            float op2;
            float res;
            List<float>answer = new List<float>();
            float realsum;
            float imaginarysum;
            for (int i = 0; i < k; i++)
            {
                realsum = 0;
                imaginarysum = 0;
                for (int j = 0; j < k; j++)
                {
                    real = InputFreqDomainSignal.FrequenciesAmplitudes[j] *(float) Math.Cos(InputFreqDomainSignal.FrequenciesPhaseShifts[j]);
                    imaginary = InputFreqDomainSignal.FrequenciesAmplitudes[j] * (float)Math.Sin(InputFreqDomainSignal.FrequenciesPhaseShifts[j]);

                    op1 = (float)Math.Cos((i * 2 * (float)Math.PI * j) / k);
                    op2 = (float)Math.Sin((i * 2 * (float)Math.PI * j) / k);
                    imaginarysum += (imaginary * op1) +(real*op2);
                    if (op2 != 0 && imaginary != 0)
                        realsum += real * op1 + (-imaginary*op2);
                     
                    else
                        realsum += real * op1;
    
                }
                res = (realsum + imaginarysum)/k ;
                answer.Add(res);
                
            }
            OutputTimeDomainSignal = new Signal(answer, false);
        }
    }
}
