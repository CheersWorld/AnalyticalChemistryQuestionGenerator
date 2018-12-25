using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytik_Altfragengenerator.Models
{
    class TitrationQuestion : StatisticsQuestion
    {

        //Getting the wanted Concentration out of a value is basically just staistics, so that's why this class inhertings from StatisticsQuestion

        public double ConcentrationTiter { get; set; }
        public double TiterFValue { get; set; }
        public double AliquotVolume { get; set; }
        public double TotalVolume { get; set; }
        public double UsedSolution { get; set; }

        public override void GenerateQuestion()
        {
            //This generates all the needed values

            ConcentrationTiter = GetRandomNumber(0.1, 0.1, 3, true);
            TiterFValue = GetRandomNumber(1, 0.1, 4, true);
            AliquotVolume = GetRandomNumber(50, 1, 3, false);
            TotalVolume = GetRandomNumber(250, 1, 4, false);
            UsedSolution = GetRandomNumber(AliquotVolume, 3, 3, true);

            QuestionContent = "Bei einer Säure-Base Titration wurden die folgenden Werte erzielt. Berechnen sie die Konzentration. Folgende Parameter wurden verwendet: \r\n" +
                "Konzentration des Titers: " + ConcentrationTiter + " mit f=" + TiterFValue + "\r\n" + 
                "Es wurde von einem Gesamtvolumen von " + TotalVolume + " ml ein Aliquot von " + AliquotVolume + " ml titriert. Verbrauch:: \r\n" + UsedSolution + " ml";
        }

        public override string GetSolution()
        {
            double _concentration = UsedSolution * ConcentrationTiter * TiterFValue * (1 / AliquotVolume);
            return "Die titrierte Lösung hatte eine Konzentration von: " + _concentration + " mol/L";
        }

       
    }
}
