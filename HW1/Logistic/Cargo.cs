using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.ConsoleClient
{
    internal class Cargo
    {
        public double Volume { get; set; }
        public int Weight { get; set; }
        public string Code { get; set; }
        public Cargo (string Code, int Weight,double Volume)
        {
            this.Volume = Volume;
            this.Weight = Weight;
            this.Code = Code;
        }
        public string GetInformation()
        {
            return $"Code cargo: {Code}, characteristics: volume: {this.Volume}, weight: {this.Weight}";
        }
    }

}
