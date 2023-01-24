using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal s1Copy { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();
            double normalization = 0;
            double sum1 = 0;
            double sum2 = 0;
            List<float> ans1 = new List<float>();
            List <float> ans2 = new List<float>();
            if (s1Copy != null)
            {
                List<float> crossCorr = new List<float>();
                List<double> s1Samples = new List<double>();
                List<double> s2Samples = new List<double>();

                //normalization procedures
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    s1Samples.Add(InputSignal1.Samples[i]);
                    s2Samples.Add(s1Copy.Samples[i]);
                }

                for (int i = 0; i < s1Samples.Count; i++)
                {
                    sum1 += s1Samples[i] * s1Samples[i];
                    sum2 += s2Samples[i] * s2Samples[i];
                }
                normalization = Math.Sqrt(sum1 * sum2) / s1Samples.Count;

                List<float> s1Real = new List<float>();
                List<float> s2Real = new List<float>();
                List<float> s1Img = new List<float>();
                List<float> s2Img = new List<float>();
                float realmultOp;
                float imgmultOp;
                List<float> multiOp = new List<float>();
                List<float> multiOp2 = new List<float>();
                Signal output;

                //DFT
                InverseDiscreteFourierTransform inverse = new InverseDiscreteFourierTransform();
                DiscreteFourierTransform DFT = new DiscreteFourierTransform();
                DFT.InputTimeDomainSignal = s1Copy;
                DFT.Run();
                s1Copy.FrequenciesAmplitudes = DFT.OutputFreqDomainSignal.FrequenciesAmplitudes;
                s1Copy.FrequenciesPhaseShifts = DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts;
                DFT.InputTimeDomainSignal = InputSignal1;
                DFT.Run();
                InputSignal1.FrequenciesAmplitudes = DFT.OutputFreqDomainSignal.FrequenciesAmplitudes;
                InputSignal1.FrequenciesPhaseShifts = DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts;

                //covert phases and amplitudes into real and imaginary
                for (int i = 0; i < InputSignal1.FrequenciesAmplitudes.Count; i++)
                {
                    s1Real.Add(InputSignal1.FrequenciesAmplitudes[i] * (float)Math.Cos(InputSignal1.FrequenciesPhaseShifts[i]));
                    s2Real.Add(s1Copy.FrequenciesAmplitudes[i] * (float)Math.Cos(s1Copy.FrequenciesPhaseShifts[i]));
                    s1Img.Add(-1*(InputSignal1.FrequenciesAmplitudes[i] * (float)Math.Sin(InputSignal1.FrequenciesPhaseShifts[i])));
                    s2Img.Add(s1Copy.FrequenciesAmplitudes[i] * (float)Math.Sin(s1Copy.FrequenciesPhaseShifts[i]));
                }
                //multiplication processes + convert back again into phases and amplitudes
                for (int i = 0; i < s1Real.Count; i++)
                {
                    imgmultOp = (s1Img[i] * (s2Real[i])) + (s1Real[i] * s2Img[i]);
                    realmultOp = (s1Real[i] * s2Real[i]) + (-1 * (s1Img[i] * s2Img[i]));
                    // multiOp.Add(realmultOp);
                    //multiOp2.Add(imgmultOp);
                    float realsq = (float)Math.Pow(realmultOp, 2);
                    float imgsq = (float)Math.Pow(imgmultOp, 2);
                    multiOp.Add((float)(Math.Sqrt(realsq + imgsq)));
                    multiOp2.Add((float)Math.Atan2(imgmultOp, realmultOp));
                }

                //IDFT 
                output = new Signal(multiOp, false);
                output.Frequencies = multiOp;
                inverse.InputFreqDomainSignal = output;
                inverse.InputFreqDomainSignal.FrequenciesAmplitudes = multiOp;
                inverse.InputFreqDomainSignal.FrequenciesPhaseShifts = multiOp2;
                inverse.InputFreqDomainSignal.Frequencies = output.Frequencies;
                inverse.Run();
                for(int i=0; i< inverse.OutputTimeDomainSignal.Samples.Count;i++)
                {
                    ans1.Add(inverse.OutputTimeDomainSignal.Samples[i] / InputSignal1.Samples.Count);
                    ans2.Add((inverse.OutputTimeDomainSignal.Samples[i] /(float) normalization) / InputSignal1.Samples.Count);
                }
                OutputNonNormalizedCorrelation = ans1;
                OutputNormalizedCorrelation = ans2;

            }
            else
            {
               
                    List<float> crossCorr = new List<float>();
                    List<double> s1Samples = new List<double>();
                    List<double> s1SampleCopy = new List<double>();

                    //normalization procedures
                    for (int i = 0; i < InputSignal1.Samples.Count; i++)
                    {
                        s1Samples.Add(InputSignal1.Samples[i]);
                        s1SampleCopy.Add(InputSignal1.Samples[i]);
                    }

                    for (int i = 0; i < s1Samples.Count; i++)
                    {
                        sum1 += s1Samples[i] * s1Samples[i];
                        sum2 += s1SampleCopy[i] * s1SampleCopy[i];
                    }
                    normalization = Math.Sqrt(sum1 * sum2) / s1Samples.Count;

                    List<float> s1Real = new List<float>();
                    List<float> s2Real = new List<float>();
                    List<float> s1Img = new List<float>();
                    List<float> s2Img = new List<float>();
                    float realmultOp;
                    float imgmultOp;
                    List<float> multiOp = new List<float>();
                    List<float> multiOp2 = new List<float>();
                    Signal output;
                    Signal s1Copy = new Signal(InputSignal1.Samples, false);

                    //DFT
                    InverseDiscreteFourierTransform inverse = new InverseDiscreteFourierTransform();
                    DiscreteFourierTransform DFT = new DiscreteFourierTransform();
                    DFT.InputTimeDomainSignal = s1Copy;
                    DFT.Run();
                    s1Copy.FrequenciesAmplitudes = DFT.OutputFreqDomainSignal.FrequenciesAmplitudes;
                    s1Copy.FrequenciesPhaseShifts = DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts;
                    DFT.InputTimeDomainSignal = InputSignal1;
                    DFT.Run();
                    InputSignal1.FrequenciesAmplitudes = DFT.OutputFreqDomainSignal.FrequenciesAmplitudes;
                    InputSignal1.FrequenciesPhaseShifts = DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts;

                    //covert phases and amplitudes into real and imaginary
                    for (int i = 0; i < InputSignal1.FrequenciesAmplitudes.Count; i++)
                    {
                        s1Real.Add(InputSignal1.FrequenciesAmplitudes[i] * (float)Math.Cos(InputSignal1.FrequenciesPhaseShifts[i]));
                        s2Real.Add(s1Copy.FrequenciesAmplitudes[i] * (float)Math.Cos(s1Copy.FrequenciesPhaseShifts[i]));
                        s1Img.Add(-1 * (InputSignal1.FrequenciesAmplitudes[i] * (float)Math.Sin(InputSignal1.FrequenciesPhaseShifts[i])));
                        s2Img.Add(s1Copy.FrequenciesAmplitudes[i] * (float)Math.Sin(s1Copy.FrequenciesPhaseShifts[i]));
                    }
                    //multiplication processes + convert back again into phases and amplitudes
                    for (int i = 0; i < s1Real.Count; i++)
                    {
                        imgmultOp = (s1Img[i] * (s2Real[i])) + (s1Real[i] * s2Img[i]);
                        realmultOp = (s1Real[i] * s2Real[i]) + (-1 * (s1Img[i] * s2Img[i]));
                        float realsq = (float)Math.Pow(realmultOp, 2);
                        float imgsq = (float)Math.Pow(imgmultOp, 2);
                        multiOp.Add((float)(Math.Sqrt(realsq + imgsq)));
                        multiOp2.Add((float)Math.Atan2(imgmultOp, realmultOp));
                    }

                    //IDFT 
                    output = new Signal(multiOp, false);
                    output.Frequencies = multiOp;
                    inverse.InputFreqDomainSignal = output;
                    inverse.InputFreqDomainSignal.FrequenciesAmplitudes = multiOp;
                    inverse.InputFreqDomainSignal.FrequenciesPhaseShifts = multiOp2;
                    inverse.InputFreqDomainSignal.Frequencies = output.Frequencies;
                    inverse.Run();
                    for (int i = 0; i < inverse.OutputTimeDomainSignal.Samples.Count; i++)
                    {
                        ans1.Add(inverse.OutputTimeDomainSignal.Samples[i] / InputSignal1.Samples.Count);
                        ans2.Add((inverse.OutputTimeDomainSignal.Samples[i] / (float)normalization) / InputSignal1.Samples.Count);
                    }
                    OutputNonNormalizedCorrelation = ans1;
                    OutputNormalizedCorrelation = ans2;

                }
            }
        }
    }
