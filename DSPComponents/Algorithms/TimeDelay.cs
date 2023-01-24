﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
namespace DSPAlgorithms.Algorithms
{
    public class TimeDelay : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public float InputSamplingPeriod { get; set; }
        public float OutputTimeDelay { get; set; }
        public override void Run()
        {
            DirectCorrelation corr = new DirectCorrelation();
            List<float> corrlist = new List<float>();
            corr.InputSignal1 = InputSignal1;
            corr.InputSignal2 = InputSignal2;
            corr.Run();
            corrlist = corr.OutputNonNormalizedCorrelation;
            
            float maxi = 0;

            for (int i = 0; i < corrlist.Count; i++)
            {

                if (maxi < corrlist[i])
                    maxi = i;

            }

            OutputTimeDelay = maxi * InputSamplingPeriod;

        }
    }
}