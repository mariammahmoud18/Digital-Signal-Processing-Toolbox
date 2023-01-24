using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            float op;
            float mult;
            List<float> ans = new List<float>();
            float count = InputSignal.Samples.Count;
            for (int k=0; k<count; k++)
            {
                op = 0;
                if (k == 0)
                    mult = 1 /(float) Math.Sqrt(count);
                else
                    mult = ((float)Math.Sqrt(2 / count));

                for (int n=0; n<count; n++)
                {
                    op += InputSignal.Samples[n] * (float)Math.Cos(((float)Math.PI * ((2 * n) + 1) * (k))/ (2 * count));
                }
                ans.Add(op * mult );
            }
            OutputSignal = new Signal(ans, false);

        }
    }
}
