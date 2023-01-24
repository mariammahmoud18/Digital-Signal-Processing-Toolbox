
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();
            double normalization = 0;
            double sum1 = 0;
            double sum2 = 0;
            double corrOp = 0;
            double firstElement;

            //auto
            if (InputSignal2 == null) 
            {
                List<float> autoCorr = new List<float>();
                List<double> s1Samples = new List<double>();
                List<double> s1Copy = new List<double>();
               


                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    s1Samples.Add(InputSignal1.Samples[i]);
                    s1Copy.Add(InputSignal1.Samples[i]);
                }

                //normalization
                for (int i = 0; i < s1Samples.Count; i++)
                {
                    sum1 += s1Samples[i] * s1Samples[i];
                    sum2 += s1Samples[i] * s1Samples[i];
                }
                normalization = Math.Sqrt(sum1*sum2)/ s1Samples.Count;
                //Non periodic auto corr
                if (!InputSignal1.Periodic)
                {
                    for (int i = 0; i < s1Copy.Count; i++)
                    {
                         corrOp = 0;
                        if (i != 0)
                        {
                             firstElement = 0;
                            for (int j = 0; j < s1Copy.Count - 1; j++)
                            {
                                s1Copy[j] = s1Copy[j + 1];
                                corrOp += s1Copy[j] * s1Samples[j];
                            }
                            s1Copy[s1Copy.Count - 1] = firstElement;
                        }
                        else
                        {
                            for (int j = 0; j < s1Copy.Count; j++)
                                corrOp += s1Copy[j] * s1Samples[j];
                        }
                        autoCorr.Add((float)corrOp / s1Copy.Count);
                    }
                }
                //Periodic auto corr
                else
                {
                    for (int i = 0; i < s1Copy.Count; i++)
                    {
                        corrOp = 0;
                        if (i != 0)
                        {
                             firstElement = s1Copy[0];
                            for (int j = 0; j < s1Copy.Count - 1; j++)
                            {
                                s1Copy[j] = s1Copy[j + 1];
                                corrOp += s1Copy[j] * s1Samples[j];
                            }
                            s1Copy[s1Copy.Count - 1] = firstElement;
                            corrOp += s1Copy[s1Copy.Count - 1] * s1Samples[s1Samples.Count - 1];
                        }
                        else
                        {
                            for (int j = 0; j < s1Copy.Count; j++)
                                corrOp += s1Copy[j] * s1Copy[j];
                        }
                        autoCorr.Add((float)corrOp / s1Copy.Count);
                    }

                }

                OutputNonNormalizedCorrelation = autoCorr;

                for (int i = 0; i < OutputNonNormalizedCorrelation.Count; i++) {

                    OutputNormalizedCorrelation.Add((float)(OutputNonNormalizedCorrelation[i] / normalization));
                        }
            }

             // Cross Correlation
            else 
            {
                List<float> crossCorr = new List<float>();
                List<double> s1Samples = new List<double>();
                List<double> s2Samples = new List<double>();
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    s1Samples.Add(InputSignal1.Samples[i]);
                    s2Samples.Add(InputSignal2.Samples[i]);
                }
               
                for (int i = 0; i < s1Samples.Count; i++)
                {
                    sum1 += s1Samples[i] * s1Samples[i];
                    sum2 += s2Samples[i] * s2Samples[i];
                }
                normalization = Math.Sqrt(sum1*sum2)/ s1Samples.Count;
                //cross non periodic
                if (!InputSignal1.Periodic)
                {
                    for (int i = 0; i < s2Samples.Count; i++)
                    {
                        corrOp = 0;
                        if (i != 0) // so shift 
                        {
                             firstElement = 0;
                            for (int j = 0; j < s2Samples.Count - 1; j++)
                            {
                                s2Samples[j] = s2Samples[j + 1];
                                corrOp += s2Samples[j] * s1Samples[j];
                            }
                            s2Samples[s2Samples.Count - 1] = firstElement;
                        }
                        else
                        {
                            for (int j = 0; j < s2Samples.Count; j++)
                                corrOp += s1Samples[j] * s2Samples[j];
                        }
                        crossCorr.Add((float)corrOp / s1Samples.Count);
                    }
                }

                else
                {
                    for (int i = 0; i < s2Samples.Count; i++)
                    {
                        corrOp = 0;
                        if (i != 0) // so shift 
                        {
                             firstElement = s2Samples[0];
                            for (int j = 0; j < s2Samples.Count - 1; j++)
                            {
                                s2Samples[j] = s2Samples[j + 1];
                                corrOp += s2Samples[j] * s1Samples[j];
                            }
                            s2Samples[s2Samples.Count - 1] = firstElement;
                            corrOp += s2Samples[s2Samples.Count - 1] * s1Samples[s1Samples.Count - 1];
                        }
                        else
                        {
                            for (int j = 0; j < s2Samples.Count; j++)
                                corrOp += s1Samples[j] * s2Samples[j];
                        }
                        crossCorr.Add((float)corrOp / s2Samples.Count);
                    }
                }

                OutputNonNormalizedCorrelation = crossCorr;

                for (int i = 0; i < OutputNonNormalizedCorrelation.Count; i++)
                { 
                    OutputNormalizedCorrelation.Add((float)(OutputNonNormalizedCorrelation[i] / normalization));
                }
            }

        }
    }
}