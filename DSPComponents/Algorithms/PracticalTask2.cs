﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DSPAlgorithms.Algorithms
{
    public class PracticalTask2 : Algorithm
    {
        public String SignalPath { get; set; }
        public float Fs { get; set; }
        public float miniF { get; set; }
        public float maxF { get; set; }
        public float newFs { get; set; }
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            Signal InputSignal = LoadSignal(SignalPath);
            writeTimeSignal(InputSignal,InputSignal.Periodic,0);
            Signal resultedSignal1;
            Signal resultedSignal2=new Signal(InputSignal.Samples,false);
            Signal resultedSignal3;
            Signal resultedSignal4;
            Signal resultedSignal5;

            //////////////////////////////
            //FIR
            FIR filter = new FIR();
            filter.InputFilterType = FILTER_TYPES.BAND_PASS;
            filter.InputFS = Fs;
            filter.InputTimeDomainSignal = InputSignal;
            filter.InputStopBandAttenuation = 50;
            filter.InputTransitionBand = 500;
            filter.InputF1 = miniF; 
            filter.InputF2 = maxF;
            filter.Run();
            resultedSignal1 = filter.OutputYn;
            writeTimeSignal(resultedSignal1,resultedSignal1.Periodic,1);
            ////////////////////////////////////////////
            ///Resampling
            float neqFreq = 2 * maxF;
            bool flag = false;
            if (newFs >= neqFreq)///////Fs doesnt destory the signal
            {
                Sampling samp = new Sampling();
                samp.InputSignal = resultedSignal1;
                samp.L = L;
                samp.M = M;
                samp.Run();
                resultedSignal2 = samp.OutputSignal;
                writeTimeSignal(resultedSignal2,resultedSignal2.Periodic,2);
                flag = true;
            }
            else
            {
                Console.WriteLine("newFs is not valid");
            }
            /////////////////////////////////
            ///Remove DC
            DC_Component DC = new DC_Component();
            if (flag)
            { 
                DC.InputSignal = resultedSignal2; 
            }
            else
            {
                DC.InputSignal = resultedSignal1;
            }
            DC.Run();
            resultedSignal3 = DC.OutputSignal;
            writeTimeSignal(resultedSignal3,resultedSignal3.Periodic,3);



            //5
            Normalizer normalizerObj = new Normalizer();
            normalizerObj.InputMinRange = -1;
            normalizerObj.InputMaxRange = 1;
            normalizerObj.InputSignal = resultedSignal3;
            normalizerObj.Run();
            resultedSignal4 = normalizerObj.OutputNormalizedSignal;
            writeTimeSignal(resultedSignal4, resultedSignal4.Periodic,4);


            //6
            DiscreteFourierTransform DFT = new DiscreteFourierTransform();
            DFT.InputSamplingFrequency = Fs;
            DFT.InputTimeDomainSignal = resultedSignal4;
            DFT.Run();
            resultedSignal5 = DFT.OutputFreqDomainSignal;
            writeFrequecnySignal(resultedSignal5, resultedSignal5.Periodic,5);
            OutputFreqDomainSignal = new Signal(false,resultedSignal5.Frequencies, resultedSignal5.FrequenciesAmplitudes, resultedSignal5.FrequenciesPhaseShifts);


        }

        public Signal LoadSignal(string filePath)
        {
            Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(stream);

            var sigType = byte.Parse(sr.ReadLine());
            var isPeriodic = byte.Parse(sr.ReadLine());
            long N1 = long.Parse(sr.ReadLine());

            List<float> SigSamples = new List<float>(unchecked((int)N1));
            List<int> SigIndices = new List<int>(unchecked((int)N1));
            List<float> SigFreq = new List<float>(unchecked((int)N1));
            List<float> SigFreqAmp = new List<float>(unchecked((int)N1));
            List<float> SigPhaseShift = new List<float>(unchecked((int)N1));

            if (sigType == 1)
            {
                SigSamples = null;
                SigIndices = null;
            }

            for (int i = 0; i < N1; i++)
            {
                if (sigType == 0 || sigType == 2)
                {
                    var timeIndex_SampleAmplitude = sr.ReadLine().Split();
                    SigIndices.Add(int.Parse(timeIndex_SampleAmplitude[0]));
                    SigSamples.Add(float.Parse(timeIndex_SampleAmplitude[1]));
                }
                else
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            if (!sr.EndOfStream)
            {
                long N2 = long.Parse(sr.ReadLine());

                for (int i = 0; i < N2; i++)
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            stream.Close();
            return new Signal(SigSamples, SigIndices, isPeriodic == 1, SigFreq, SigFreqAmp, SigPhaseShift);
        }

        public void writeFrequecnySignal(Signal s1,bool periodic,int filenum)
        {
            string fullpath = "D:/4th year 1st term/DSP/fcisdsp-dsp.toolbox-78ddd969882b/fcisdsp-dsp.toolbox-78ddd969882b/DSPToolbox/DSPComponentsUnitTest/bin/Debug/TestingSignals/resultingSignal" + filenum+".ds";
            using (StreamWriter writer = new StreamWriter(fullpath))
            {
                writer.WriteLine(1); //frequency domain
                if(periodic)
                  writer.WriteLine(1);
                else
                  writer.WriteLine(0);

                writer.WriteLine(s1.Samples.Count);
                for (int i = 0; i < s1.Samples.Count; i++)
                {
                    writer.Write(Math.Round(s1.Samples[i], 1));
                    writer.Write(" ");
                    writer.Write(s1.FrequenciesAmplitudes[i]);
                    writer.Write(" ");
                    writer.WriteLine(s1.FrequenciesPhaseShifts[i]);

                }

            }
        }

        public void writeTimeSignal(Signal s1,bool periodic,int filenum)
        {
            string fullpath = "D:/4th year 1st term/DSP/fcisdsp-dsp.toolbox-78ddd969882b/fcisdsp-dsp.toolbox-78ddd969882b/DSPToolbox/DSPComponentsUnitTest/bin/Debug/TestingSignals/resultingSignal" + filenum+".ds";
            using (StreamWriter writer = new StreamWriter(fullpath))
            {
                writer.WriteLine(0); //time domain
                if (periodic)
                    writer.WriteLine(1);
                else
                    writer.WriteLine(0);
                writer.WriteLine(s1.Samples.Count);
                for (int i = 0; i < s1.Samples.Count; i++)
                {
                    writer.Write(s1.SamplesIndices[i]);
                    writer.Write(" ");
                    writer.WriteLine(s1.Samples[i]);

                }

            }

        }

    }
}