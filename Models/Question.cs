using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analytik_Altfragengenerator.ViewModels;

namespace Analytik_Altfragengenerator.Models
{
    class Question
    {
        Random Random = new Random();
        public string QuestionName { get; set; }
        public string QuestionCategory { get; set; }
        public string QuestionContent { get; set; }

        public double GetRandomNumber(double BaseValue, double Modifier, int DigitLimitation, bool Add)
        {
            string _tempReturnString = "";
            double _tempDouble = BaseValue * (Random.NextDouble() * Modifier);
            if (Add)
                _tempDouble = BaseValue + (Random.NextDouble() * Modifier);
            for (int i = 0; i <= DigitLimitation; i++)
            {
                _tempReturnString = _tempReturnString + _tempDouble.ToString()[i];
            }
            return Double.Parse(_tempReturnString);
        }

    }
}
