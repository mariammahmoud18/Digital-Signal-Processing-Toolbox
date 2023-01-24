using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos: Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {
           
                float normalizedFrequency = AnalogFrequency / SamplingFrequency;
                float result;
            samples = new List<float>();
                if (type == "sin") {
                for (int i = 0; i < SamplingFrequency; i++)
                {
                    double inner = (normalizedFrequency * 2 * Math.PI*i) + PhaseShift;
                    result = (float)(A * Math.Sin(inner)); 
                    samples.Add(result);

                }
                }
                else if (type == "cos")
                {
                for (int i = 0; i < SamplingFrequency; i++)
                {
                    double inner = (normalizedFrequency * 2 * Math.PI * i) + PhaseShift;
                    result = (float)(A * Math.Cos(inner));
                    samples.Add(result);

                }
            }
            
            
        }
    }
}
