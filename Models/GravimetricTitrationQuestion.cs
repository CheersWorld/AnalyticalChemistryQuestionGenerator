using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Analytik_Altfragengenerator.Databases;

namespace Analytik_Altfragengenerator.Models
{
    class GravimetricTitrationQuestion : TitrationQuestion
    {
        public double SampleWeight { get; set; }
        public string Sample { get; set; }
        private Database Database = new Database();
        public double[] Results { get; set; }

        public override void GenerateQuestion()
        {
            //Based on the basic titration, this one requires a bit of a different way of calculating the results

            base.GenerateQuestion();
            TotalVolume = AliquotVolume * 4 + GetRandomNumber(5, 2, 3, true);
            SampleWeight = GetRandomNumber(3, 2, 3, true);
            Sample = Database.GetRandomSample();
            QuestionContent = "Bei einer Fällungstitration zur Bestimmung von " + Sample + " wurde " + Database.FinalSampleComposition[Sample] + " erhalten.\r\n" +
                "Es wurden " + SampleWeight + " g der Probe von einem Gesamtvolumen von " + TotalVolume + " ml gelöst und ein Aliquot von " + AliquotVolume + "  ml titriert. \r\n" +
                "Geben sie den " + Sample + "-gehalt der Probe (in Massen-%) mit einem 95%-igen Streubereich an.\r\n" +
                "Erhaltene Auswagen (g):\r\n" + GetWeights();
        }

        //This should have been a public double[] from the very beginning
        public string GetWeights()
        {
            double[] _tempResults = new double[4];
            string _tempString = "";
            double _sampleContent = GetRandomNumber(0, 1, 3, true);
            for(int i = 0; i < 4; i ++)
            {
                //Weirdly enough you don't have to wait for 13 ms in between generations here
                _tempResults[i] = Double.Parse(ShortenString((SampleWeight * (AliquotVolume / TotalVolume) * _sampleContent + GetRandomNumber(0, 0.1, 3, true)).ToString(), 5));
                _tempString += _tempResults[i] + "\r\n";
            }
            Results = _tempResults;
            return _tempString;
        }

        //This returns the % of Element contained in the Sample
        public override string GetSolution()
        {
            double[] _percent = new double[4];
            for(int i = 0; i < Results.Length; i++)
            {
                _percent[i] = Results[i] * Database.GravimetricSampleDatabase[Sample] * (TotalVolume / AliquotVolume) * (1 / SampleWeight) * 100;
            }

            double _tempMean = _percent.Sum() / _percent.Length;
            double _standardDeviaion = 0;

            //This is duplicate code from StatisticsQuestion. This could be fixed by passing the desired array in the method parameters, however that would require quite a bit of restructuring.

            for (int i = 0; i < _percent.Length; i++)
            {
                _standardDeviaion += (_percent[i] - _tempMean) * (_percent[i] - _tempMean);
            }
            _standardDeviaion = Math.Sqrt(_standardDeviaion / (_percent.Length - 1)) * 2.776;

            return "Die Probe enthält " + ShortenString(_tempMean.ToString(), 5) + "±" + ShortenString(_standardDeviaion.ToString(), 4) + " % " + Sample;
        }
    }
}
