using System;


namespace Logistic.ConsoleClient.Models
{
    public class Warehouse : IRecord<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Cargo> Cargos { get; set; }
        public Warehouse()
        {
            Cargos = new List<Cargo>();
        }
    }
}
