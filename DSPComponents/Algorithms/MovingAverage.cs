using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
 
        public override void Run()
        {
            List<float> avg = new List<float>();
            float count = 0;
            int itr = InputWindowSize;
            for(int i = 0; i <= InputSignal.Samples.Count-InputWindowSize;i++)
            {
               
                count = 0;
                for(int j=0; j<InputWindowSize; j++)
                {
                    
                    count += InputSignal.Samples[i + j]; 
                }

                avg.Add(count / InputWindowSize);
                
            }
            OutputAverageSignal = new Signal(avg, false);
        }
    }
}
