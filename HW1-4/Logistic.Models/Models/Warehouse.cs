using System;


namespace Logistic.Models
{
    public class Warehouse : IRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Cargo> Cargos { get; set; }
        public Warehouse()
        {
            Cargos = new List<Cargo>();
        }
        public override string ToString()
        {
            return $"Info about warehouse with ID: {Id} name: {Name} ";
        }
    }
}
