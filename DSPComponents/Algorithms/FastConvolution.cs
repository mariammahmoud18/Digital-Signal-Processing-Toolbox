using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {

            List<float> s1Phase = new List<float>();
            List<float> s1Amp = new List<float>();
            List<float> s2Phase = new List<float>();
            List<float> s2Amp = new List<float>();
            float s1Real;
            float s2Real;
            float s1Img;
            float s2Img;
            float realmultOp;
            float imgmultOp;
            List<float> multiOp = new List<float>();
            List<float> multiOp2 = new List<float>();
            Complex complex1;
            Complex  complex2;
            List <Complex> ans = new List<Complex>();   

            Signal output;

            int cnt = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
            for(int i=InputSignal1.Samples.Count;i<cnt; i++)
            {
                InputSignal1.Samples.Add(0);
            }
            for (int i = InputSignal2.Samples.Count; i < cnt; i++)
            {
                InputSignal2.Samples.Add(0);
            }
            InverseDiscreteFourierTransform inverse = new InverseDiscreteFourierTransform();
            DiscreteFourierTransform operation = new DiscreteFourierTransform();
            operation.InputTimeDomainSignal = InputSignal1;
            operation.Run();
            s1Amp = operation.OutputFreqDomainSignal.FrequenciesAmplitudes;
            s1Phase = operation.OutputFreqDomainSignal.FrequenciesPhaseShifts;
            operation.InputTimeDomainSignal = InputSignal2;
            operation.Run();
            s2Amp = operation.OutputFreqDomainSignal.FrequenciesAmplitudes;
            s2Phase = operation.OutputFreqDomainSignal.FrequenciesPhaseShifts;

            //getting real and imaginary num
            for(int i=0; i< cnt; i++)
            {
                s1Real = s1Amp[i] * (float)Math.Cos(s1Phase[i]);
                s2Real = s2Amp[i] * (float)Math.Cos(s2Phase[i]);
                s1Img = s1Amp[i] * (float)Math.Sin(s1Phase[i]);
                s2Img = s2Amp[i] * (float)Math.Sin(s2Phase[i]);
                complex1 = new Complex(s1Real, s1Img);
                complex2 = new Complex(s2Real, s2Img);
                ans.Add(Complex.Multiply(complex1, complex2));
            }
            for(int i=0; i<cnt; i++)
            {
                float realsq = (float)Math.Pow(ans[i].Real, 2);
                float imgsq = (float)Math.Pow(ans[i].Imaginary, 2);
                multiOp.Add((float)(Math.Sqrt(realsq + imgsq)));
                multiOp2.Add((float)Math.Atan2(ans[i].Imaginary, ans[i].Real));
            }

            output = new Signal(false, multiOp, multiOp, multiOp2 );
            inverse.InputFreqDomainSignal = output;
            inverse.Run();
            OutputConvolvedSignal = new Signal(inverse.OutputTimeDomainSignal.Samples, false);


        }
    }
}
