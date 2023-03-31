using System;


namespace Logistic.ConsoleClient.Models
{
    public class Cargo : IRecord<Guid>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public double Volume { get; set; }
        public int Weight { get; set; }
        
        public Cargo()
        {
            Id = Guid.NewGuid();
        }
        public string GetInformation()
        {
            return $"Code cargo:  characteristics: volume: {Volume}, weight: {Weight}";
        }
    }

}
