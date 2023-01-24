using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        public override void Run()
        {
            //Calculate Coefficents num
            float deltaF = InputTransitionBand/InputFS;
            float N = 0;
            float startN;
            float cutOffFreq1;
            float cutOffFreq2;
            List<float>Hn = new List<float>();
            List<float> Hn2 = new List<float>();
            List<float>Wn = new List<float>();
            List <float>output = new List<float>();
            List<float> output2 = new List<float>();
            List<int>indcies = new List<int>();
            List<int> indcies2 = new List<int>();
            Signal HnCopy;
            string flag;
            DirectConvolution DC = new DirectConvolution();


            if (InputStopBandAttenuation >= 0 && InputStopBandAttenuation < 21)
            { N = (float)0.9 / deltaF;
                flag = "rect";
            }
            else if (InputStopBandAttenuation >= 21 && InputStopBandAttenuation < 44)
            { N = (float)3.1 / deltaF;
                flag = "Han";
            }
            else if (InputStopBandAttenuation >= 44 && InputStopBandAttenuation < 53)
            {
                flag = "Ham";
                N = (float)3.3 / deltaF;
            }
            else
            { N = (float)5.5 / deltaF;
                flag = "man";
            }

            N = (float)Math.Ceiling((double)N);
            if (N % 2 == 0)
                N += 1;
            startN = (float)Math.Floor((double)N/2);

            if (InputFilterType== FILTER_TYPES.LOW)
            {

                cutOffFreq1 = (float)(InputCutOffFrequency) + (InputTransitionBand / 2);
                cutOffFreq1 = cutOffFreq1 / InputFS;
                for (int i=0; i<=startN;i++)
                {
                    if (flag == "Han")
                        Wn.Add((float)0.5 + (float)(0.5 * Math.Cos((2 * Math.PI * i) / N)));
                    else if (flag == "Ham")
                        Wn.Add((float)0.54 + (float)(0.46 * Math.Cos((2 * Math.PI * i) / N)));
                    else if (flag == "man")
                        Wn.Add((float)0.42 + (float)(0.5 * Math.Cos((2 * Math.PI * i) / N - 1)) + (float)(0.08 * Math.Cos((4 * Math.PI * i) / N - 1)));
                    else
                        Wn.Add(1);
                    if (i==0)
                    {
                        Hn.Add(2 * cutOffFreq1);
                        
                    }
                    else
                        Hn.Add(2 * cutOffFreq1*(float)(Math.Sin(2*i* Math.PI * cutOffFreq1)/(i*2*Math.PI*cutOffFreq1)));
                    output.Add(Wn[i] * Hn[i]);

                }
               

                for(int i=(int)(-1*startN); i<=startN;i++)
                {
                    indcies.Add(i);
                    if (i < 0)
                    {
                        output2.Add(output[-1 * i]);
                    }
                    else
                    { 
                        output2.Add(output[i]);

                    }
                }
                
            }
            else if (InputFilterType == FILTER_TYPES.HIGH)
            {

                cutOffFreq1 = (float)(InputCutOffFrequency) - (InputTransitionBand / 2);
                cutOffFreq1 = cutOffFreq1 / InputFS;
                for (int i = 0; i <= startN; i++)
                {
                    float term1;
                    float term2;
                    term1 = (float)0.42 + (float)(0.5 * Math.Cos((2 * (float)Math.PI * i) / (N - 1)));
                    term2 = (float)(0.08 * Math.Cos((4 * Math.PI * i) / (N - 1)));
                    if (flag == "Han")
                        Wn.Add((float)0.5 + (float)(0.5 * Math.Cos((2 * Math.PI * i) / N)));
                    else if (flag == "Ham")
                        Wn.Add((float)0.54 + (float)(0.46 * Math.Cos((2 * Math.PI * i) / N)));
                    else if (flag == "man")
                        Wn.Add(term1+term2);
                    else
                        Wn.Add(1);
                    if (i == 0)
                    {
                        Hn.Add(1-(2 * cutOffFreq1));

                    }
                    else
                        Hn.Add(-2 * cutOffFreq1 * (float)(Math.Sin(2 * i * Math.PI * cutOffFreq1) / (i * 2 * Math.PI * cutOffFreq1)));
                    output.Add(Wn[i] * Hn[i]);

                }


                for (int i = (int)(-1 * startN); i <= startN; i++)
                {
                    indcies.Add(i);
                    if (i < 0)
                    {
                        output2.Add(output[-1 * i]);
                    }
                    else
                    {
                        output2.Add(output[i]);

                    }
                }

            }

            else if (InputFilterType == FILTER_TYPES.BAND_PASS)
            {
                cutOffFreq1 = (float)(InputF1 - (InputTransitionBand / 2));
                cutOffFreq1 = cutOffFreq1 / InputFS;
                cutOffFreq2 = (float)(InputF2 + (InputTransitionBand / 2));
                cutOffFreq2 = cutOffFreq2 / InputFS;

                for (int i = 0; i <= startN; i++)
                {
                    float term1;
                    float term2;
                    
                    term1 = (float)0.42 + (float)(0.5 * Math.Cos((2 * (float)Math.PI * i) / (N - 1)));
                    term2 = (float)(0.08 * Math.Cos((4 * Math.PI * i) / (N - 1)));
                    if (flag == "Han")
                        Wn.Add((float)0.5 + (float)(0.5 * Math.Cos((2 * Math.PI * i) / N)));
                    else if (flag == "Ham")
                        Wn.Add((float)0.54 + (float)(0.46 * Math.Cos((2 * Math.PI * i) / N)));
                    else if (flag == "man")
                        Wn.Add(term1+term2);
                    else
                        Wn.Add(1);
                    term1 = 2 * cutOffFreq2 * (float)(Math.Sin(2 * i * Math.PI * cutOffFreq2) / (2 * i * Math.PI * cutOffFreq2));
                    term2 = 2 * cutOffFreq1 * (float)(Math.Sin(2 * i * Math.PI * cutOffFreq1) / (2 * i * Math.PI * cutOffFreq1));
                    if (i == 0)
                    {
                        Hn.Add(2 * (cutOffFreq2-cutOffFreq1));

                    }
                    else
                        Hn.Add(term1-term2);
                    output.Add(Wn[i] * Hn[i]);

                }


                for (int i = (int)(-1 * startN); i <= startN; i++)
                {
                    indcies.Add(i);
                    if (i < 0)
                    {
                        output2.Add(output[-1 * i]);
                    }
                    else
                    {
                        output2.Add(output[i]);

                    }
                }
            }
            else
            {
                cutOffFreq1 = (float)(InputF1  + (InputTransitionBand / 2));
                cutOffFreq1 = cutOffFreq1 / InputFS;
                cutOffFreq2 = (float)(InputF2 - (InputTransitionBand / 2));
                cutOffFreq2 = cutOffFreq2 / InputFS;

                for (int i = 0; i <= startN; i++)
                {
                    float term1;
                    float term2;
                    term1 = (float)0.42 + (float)(0.5 * Math.Cos((2 * (float)Math.PI * i) / (N - 1)));
                    term2 = (float)(0.08 * Math.Cos((4 * Math.PI * i) / (N - 1)));
                    if (flag == "Han")
                        Wn.Add((float)0.5 + (float)(0.5 * Math.Cos((2 * Math.PI * i) / N)));
                    else if (flag == "Ham")
                        Wn.Add((float)0.54 + (float)(0.46 * Math.Cos((2 * Math.PI * i) / N)));
                    else if (flag == "man")
                        Wn.Add(term1 + term2);
                    else
                        Wn.Add(1);
                    term1 = 2 * cutOffFreq2 * (float)(Math.Sin(2 * i * Math.PI * cutOffFreq2) / (2 * i * Math.PI * cutOffFreq2));
                    term2 = 2 * cutOffFreq1 * (float)(Math.Sin(2 * i * Math.PI * cutOffFreq1) / (2 * i * Math.PI * cutOffFreq1));
                    if (i == 0)
                    {
                        Hn.Add(1-(2 * (cutOffFreq2 - cutOffFreq1)));

                    }
                    else
                        Hn.Add(term2 - term1);
                    output.Add(Wn[i] * Hn[i]);

                }


                for (int i = (int)(-1 * startN); i <= startN; i++)
                {
                    indcies.Add(i);
                    if (i < 0)
                    {
                        output2.Add(output[-1 * i]);
                    }
                    else
                    {
                        output2.Add(output[i]);

                    }
                }
            }
            HnCopy = new Signal(output2, indcies, false);
            DC.InputSignal1 = InputTimeDomainSignal;
            DC.InputSignal2 = HnCopy;
            DC.Run();
            OutputYn = DC.OutputConvolvedSignal;
            OutputHn = new Signal(output2, indcies,InputTimeDomainSignal.Periodic);
            OutputHn.Frequencies = InputTimeDomainSignal.Frequencies;
        }

    }
    }


