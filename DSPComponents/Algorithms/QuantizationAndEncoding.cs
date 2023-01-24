using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }

        public override void Run()
        {
            float resolution;
            float maxi = InputSignal.Samples.Max();
            float mini = InputSignal.Samples.Min();
            int EncodingBits;
            List<Tuple<float,float>>intervals = new List<Tuple<float, float>>();
            List<float> midpoints = new List<float>();
            List <float> quantizedSignal = new List<float>();
            OutputIntervalIndices = new List<int>();
            OutputSamplesError = new List<float>();
            OutputEncodedSignal = new List<string>();


            if (InputLevel > 0)
            {
                resolution = (maxi - mini) / InputLevel;
                EncodingBits =(int) Math.Log(InputLevel,2);
            }
            else
            {
                int levels = (int)Math.Pow(2,InputNumBits);
                resolution = (maxi - mini) / levels;
                EncodingBits = (int)Math.Log(levels,2);
            }

            
            for (float i=mini; i<maxi;)
            {
                
                intervals.Add(new Tuple<float, float>(i, i+resolution));
                i += resolution;
                

            }

            for (int i = 0; i < intervals.Count; i ++)
            {
                float mid = (intervals[i].Item1+intervals[i].Item2)/(float)2;
                midpoints.Add(mid);
            }
             //Indices & intervals
            for(int i=0; i<InputSignal.Samples.Count;i++)
            {
                float signal = InputSignal.Samples[i];
                for(int j=0; j<intervals.Count; j++)
                {
                    if (signal >= intervals[j].Item1 && signal <= intervals[j].Item2 + 0.001)
                    {
                        quantizedSignal.Add(midpoints[j]);
                        OutputIntervalIndices.Add(j+1);
                        break;
                    }
                    else
                        continue;

                }
            }
            OutputQuantizedSignal = new Signal(quantizedSignal, false);

        
            //error and encoding
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                OutputSamplesError.Add(OutputQuantizedSignal.Samples[i] - InputSignal.Samples[i]);
                int num = OutputIntervalIndices[i] - 1;
                OutputEncodedSignal.Add(Convert.ToString(num, 2).PadLeft(EncodingBits,'0'));


            }

        }
        }
    }


