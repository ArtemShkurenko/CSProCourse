using Logistic.Models;

namespace Logistic.WebAPI.Models
{
    public class VehicleModel
    {
        public string Name { get; set; }
        public VehicleType Type { get; set; }
        public int MaxCargoWeightKg { get; set; }
        public double MaxCargoVolume { get; set; }
    }
}
