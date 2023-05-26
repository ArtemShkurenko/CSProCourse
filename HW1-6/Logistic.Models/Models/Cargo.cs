using System;
using System.Xml.Linq;


namespace Logistic.Models
{
    public class Cargo
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public double Volume { get; set; }
        public int Weight { get; set; }
        public Cargo()
        {
            Id = Guid.NewGuid();
        }
        public override string ToString()
        {
            return $"Info about cargo with ID: {Id} /// weight {Weight} kg /// volume {Volume} m3 ";
        }
    }

}
