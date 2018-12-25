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
            
            return "Der Mittelwert ist: " + ShortenString(GetMean().ToString(), 5) + "\r\nDie Standardabweichung ist: " + ShortenString(GetStandardDeviation().ToString(), 5);
        }

        //Stanard devation as a method is required for DevationQuestion. 
        public double GetStandardDeviation()
        {
            double _tempSum = 0;
            for (int i = 0; i < RandomValues.Length; i++)
            {
                _tempSum += (RandomValues[i] - GetMean()) * (RandomValues[i] - GetMean());
            }
            return Math.Sqrt(_tempSum / (RandomValues.Length - 1));
        }

        //Access to the mean value is required for DevationQuestion
        public double GetMean()
        {
            return RandomValues.Sum() / RandomValues.Length;
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

        public void Initialize()
        {
            //An inelegant design decision early on in the process of coding this makes this code necessary. I intended for this to be stored in an XML file, however that would break the scope of this project
            RandomParameterCount = 5;
            RandomParamterBaseValue = 200;
            BaseValueDeviation = 5;
            RandomParameterBaseValueMoifier = 10;
            GenerateQuestion();
        }
    }
}

