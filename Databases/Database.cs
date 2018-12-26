using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytik_Altfragengenerator.Databases
{
    class Database
    {
        Random Random = new Random();
        public Dictionary<string, double> GravimetricSampleDatabase { get; set; }
        public Dictionary<string, string> FinalSampleComposition { get; set; }

        public Database()
        {
            GravimetricSampleDatabase = new Dictionary<string, double>();
            FinalSampleComposition = new Dictionary<string, string>();

            GravimetricSampleDatabase.Add("Chrom", 0.2049664959);
            GravimetricSampleDatabase.Add("Blei", 0.6831519947);
            GravimetricSampleDatabase.Add("Eisen", 0.6992481203);

            FinalSampleComposition.Add("Eisen", "Eisen-(III)-Oxid");
            FinalSampleComposition.Add("Blei", "Bleisulfat");
            FinalSampleComposition.Add("Chrom", "Bariumchromat");
        }

        public string GetRandomSample()
        {
            return GravimetricSampleDatabase.Keys.ToArray()[Random.Next(0, GravimetricSampleDatabase.Count)];;
        }
    }
}
