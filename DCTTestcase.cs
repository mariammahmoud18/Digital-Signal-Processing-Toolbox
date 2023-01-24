﻿using DSPAlgorithms.Algorithms;
using DSPAlgorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPComponentsUnitTest
{
    [TestClass]
    public class DCTTestcase
    {
        [TestMethod]
        public void DCTtestcase()
        {
            DCT dct = new DCT();

            var expectedOutput = new Signal(new List<float>() { 500.000000000000f, -286.567797994563f, -8.41655611750505f, -31.8303817375762f, -19.6289452590712f, 341.012130701940f, 16.0468017556445f, -5.83672785237134f, 4.52076943613914f, -3.52615171587443f, 2.34726960034625f, -2.35651740529172f, 1.47663319735315f, -1.68377880322527f, 1.02532770297377f, -1.26167068150633f, 0.757050448078380f, -0.979541891586503f, 0.583144814439414f, -0.781690916840974f, 0.463396778763524f, -0.637597667098876f, 0.377160463472780f, -0.529402256148018f, 0.312860425920515f, -0.446088499477527f, 0.263562222831680f, -0.380562158116604f, 0.224890563819162f, -0.328087295056795f, 0.193966942769819f, -0.285404407327704f, 0.168831054658939f, -0.250210609984686f, 0.148108561893722f, -0.220841447571546f, 0.130811739866135f, -0.196069664120877f, 0.116215321761198f, -0.174974259353145f, 0.103776668771775f, -0.156853112232979f, 0.0930829984354837f, -0.141163359451901f, 0.0838156413062565f, -0.127479891590472f, 0.0757253046986081f, -0.115465937158948f, 0.0686146221911439f, -0.104851871865417f, 0.0623256289400231f, -0.0954197255077558f, 0.0567306312823711f, -0.0869917002426715f, 0.0517254558868390f, -0.0794215552865975f, 0.0472243933580754f, -0.0725880679968652f, 0.0431563657859132f, -0.0663900180204009f, 0.0394619900096538f, -0.0607423016542752f, 0.0360913043134819f, -0.0555728939269739f, 0.0330019919758728f, -0.0508204528622480f, 0.0301579807445327f, -0.0464324147222727f, 0.0275283294412200f, -0.0423634678499762f, 0.0250863357977694f, -0.0385743207718899f, 0.0228088161290136f, -0.0350307006888879f, 0.0206755194672735f, -0.0317025335562519f, 0.0186686476250986f, -0.0285632681665493f, 0.0167724592181640f, -0.0255893150446505f, 0.0149729405895357f, -0.0227595773106057f, 0.0132575302857142f, -0.0200550554881032f, 0.0116148865482267f, -0.0174585119308578f, 0.0100346894375039f, -0.0149541833730448f, 0.00850747085561535f, -0.0125275323075471f, 0.00702446701115135f, -0.0101650295879948f, 0.00557748885078901f, -0.00785396196800462f, 0.00415880674087554f, -0.00558225930110912f, 0.00276104626497662f, -0.00333833690211531f, 0.00137709244241012f, -0.00111094915171966f }, false);
            dct.InputSignal = new Signal(new List<float>() { 50.3844170297569f, 49.5528258147577f, 47.5503262094184f, 44.4508497187474f, 40.3553390593274f, 35.3892626146237f, 29.6995249869773f, 23.4508497187474f, 16.8217232520115f, 10.0000000000000f, 3.17827674798847f, -3.45084971874737f, -9.69952498697734f, -15.3892626146237f, -20.3553390593274f, -24.4508497187474f, -27.5503262094184f, -29.5528258147577f, -30.3844170297569f, -30f, -28.3844170297569f, -25.5528258147577f, -21.5503262094184f, -16.4508497187474f, -10.3553390593274f, -3.38926261462366f, 4.30047501302266f, 12.5491502812526f, 21.1782767479885f, 30.0000000000000f, 38.8217232520115f, 47.4508497187474f, 55.6995249869773f, 63.3892626146237f, 70.3553390593274f, 76.4508497187474f, 81.5503262094184f, 85.5528258147577f, 88.3844170297569f, 90f, 90.3844170297569f, 89.5528258147577f, 87.5503262094184f, 84.4508497187474f, 80.3553390593274f, 75.3892626146237f, 69.6995249869773f, 63.4508497187474f, 56.8217232520116f, 50.0000000000000f, 43.1782767479885f, 36.5491502812526f, 30.3004750130226f, 24.6107373853764f, 19.6446609406726f, 15.5491502812526f, 12.4496737905816f, 10.4471741852423f, 9.61558297024313f, 10f, 11.6155829702431f, 14.4471741852423f, 18.4496737905816f, 23.5491502812526f, 29.6446609406726f, 36.6107373853763f, 44.3004750130226f, 52.5491502812526f, 61.1782767479885f, 70.0000000000000f, 78.8217232520116f, 87.4508497187474f, 95.6995249869773f, 103.389262614624f, 110.355339059327f, 116.450849718747f, 121.550326209418f, 125.552825814758f, 128.384417029757f, 130f, 130.384417029757f, 129.552825814758f, 127.550326209418f, 124.450849718747f, 120.355339059327f, 115.389262614624f, 109.699524986977f, 103.450849718747f, 96.8217232520115f, 90.0000000000000f, 83.1782767479885f, 76.5491502812527f, 70.3004750130227f, 64.6107373853763f, 59.6446609406726f, 55.5491502812526f, 52.4496737905816f, 50.4471741852424f, 49.6155829702431f, 50f }, false);
            dct.Run();

            Assert.IsTrue(UnitTestUtitlities.SignalsSamplesAreEqual(expectedOutput.Samples, dct.OutputSignal.Samples));

        }
    }
}