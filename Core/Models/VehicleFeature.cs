using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        public int VehicleId { get; set; }

        public int FeatureId { get; set; }

        public T Vehicle { get; set; }

        public Feature Feature { get; set; }
    }
}