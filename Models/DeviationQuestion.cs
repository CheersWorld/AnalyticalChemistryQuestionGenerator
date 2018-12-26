using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Analytik_Altfragengenerator.Models
{
    class DeviationQuestion : Question
    {
        StatisticsQuestion[] Statistics { get; set; }

        public void GenerateQuestion()
        {
            StatisticsQuestion[] Statistics = new StatisticsQuestion[2];
            for (int i = 0; i < Statistics.Length; i++)
            {
                //Pseudo-Random numbers require at least 13 Milliseconds of waiting here to actually give you different results
                Statistics[i] = new StatisticsQuestion();
                Thread.Sleep(13);
                Statistics[i].Initialize();
                Statistics[i].RandomParameterCount += Int32.Parse(GetRandomNumber(1, 2, 1, true).ToString());
                Statistics[i].GenerateQuestion();
            }
            this.Statistics = Statistics;

            QuestionContent = "Zwei Labore bekamen beim Auswerten einer Analyse die folgenden Messwerte. Unterscheiden sie sich signifikant? \r\n";
            //Deciding how much work the user has to do
            if (GetRandomNumber(2, 1, 2, false) > 1)
            {
                for (int i = 0; i < Statistics.Length; i++)
                {
                    QuestionContent += "Standardabweichung Labor " + (i + 1) + 
                        ": " + ShortenString(Statistics[i].GetStandardDeviation().ToString(), 5) + 
                        ", Mittelwert: " + ShortenString(Statistics[i].GetMean().ToString(), 5) + 
                        " (n = " + Statistics[i].RandomParameterCount + " )\r\n";
                }
                } else
            {
                QuestionContent += "Die Messwerte der Labors sind: \r\n";
                foreach (StatisticsQuestion _statQuestion in Statistics)
                {
                    QuestionContent += "----------\r\n";
                    foreach (double _parameter in _statQuestion.RandomValues)
                    {
                        QuestionContent += _parameter + "\r\n";
                    }
                }
                QuestionContent += "----------\r\nWobei hier die oberen Werte für Labor 1, die unteren Werte für Labor 2 stehen.";
            }
        }
        public string GetSolution()
        {
            return "Aus Zeitgründen wird hier nur der F und T-Wert angezeigt, und nicht mit einer Tabelle verglichen.\r\n" +
                "F-Wert: " + GetFFactor() + "\r\n" +
                "T-Wert: " + GetTFactor();
            
        }

        public double GetTFactor()
        {
            return (Math.Abs(Statistics[0].GetMean() - Statistics[1].GetMean()) 
                / Math.Sqrt(((Statistics[0].RandomParameterCount - 1) * Math.Pow(Statistics[0].GetStandardDeviation(), 2) 
                + (Statistics[1].RandomParameterCount - 1) * Math.Pow(Statistics[1].GetStandardDeviation(), 2))
                /(Statistics[0].RandomParameterCount + Statistics[1].RandomParameterCount - 2)) 
                * Math.Sqrt((Statistics[0].RandomParameterCount * Statistics[1].RandomParameterCount)/(Statistics[0].RandomParameterCount + Statistics[1].RandomParameterCount)));
        }

        public double GetFFactor()
        {
            if (Statistics[0].GetStandardDeviation() > Statistics[1].GetStandardDeviation())
                return Math.Pow(Statistics[0].GetStandardDeviation(), 2) / Math.Pow(Statistics[1].GetStandardDeviation(), 2);
            return Math.Pow(Statistics[1].GetStandardDeviation(), 2) / Math.Pow(Statistics[0].GetStandardDeviation(), 2);
        }

    }
}
