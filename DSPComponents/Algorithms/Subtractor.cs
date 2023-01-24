﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Subtractor : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputSignal { get; set; }

        /// <summary>
        /// To do: Subtract Signal2 from Signal1 
        /// i.e OutSig = Sig1 - Sig2 
        /// </summary>
        public override void Run()
        {
            
                
                List<float> result = new List<float>();
             
                for (int j = 0; j < InputSignal1.Samples.Count; j++)
                {
                    result.Add(InputSignal1.Samples[j] - InputSignal2.Samples[j]);
                }
                OutputSignal = new Signal(result, false);

            }
        }
    }
