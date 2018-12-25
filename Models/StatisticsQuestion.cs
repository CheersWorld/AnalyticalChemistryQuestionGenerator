using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytik_Altfragengenerator.Models
{
    class StatisticsQuestion : Question
    {
        //Getting to know means and stanard deviation here

        public int RandomParameterCount { get; set; }
        public double MeanValue { get; set; }
        public double BaseValueDeviation { get; set; }
        public double RandomParamterBaseValue { get; set; }
        public double RandomParameterBaseValueMoifier { get; set; } //Higher values means a possible different RandomBaseValue
        public double[] RandomValues { get; set; }

        public virtual string GetSolution()
        {

            double _tempMean = RandomValues.Sum() / RandomValues.Length;
            double _tempSum = 0;
            for (int i = 0; i < RandomValues.Length; i++)
            {
                _tempSum += (RandomValues[i] - _tempMean) * (RandomValues[i] - _tempMean);
            }
            _tempSum = _tempSum / (RandomValues.Length - 1);
            return "Der Mittelwert ist: " + _tempSum + "\r\nDie Standardabweichung ist: " + Math.Sqrt(_tempSum);
        }

        public virtual void GenerateQuestion()
        {
            QuestionContent = "Berechnen sie die Standardabweichung und den Mittelwert für die folgenden Werte: \r\n";
            //Generates a mean point for the whole thing
            double _tempMean = GetRandomNumber(RandomParamterBaseValue, RandomParameterBaseValueMoifier, 4, true);
            double[] _generatedValues = new double[RandomParameterCount];
            for (int i = 0; i < RandomParameterCount; i++)
            {
                _generatedValues[i] = GetRandomNumber(_tempMean, BaseValueDeviation, 4, true);
                QuestionContent =QuestionContent + _generatedValues[i] + "\r\n";
            }
            RandomValues = _generatedValues;
        }
    }
}

