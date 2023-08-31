using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Xunit_API.Models
{
    [Table("vehicletype")]
    public class VehicleType
    {
        [Key]
        public int VehicleTypeId { get; set; }
        public string vehicleType { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }

        [JsonIgnore]
        public ICollection<brand>? vehicle_brands { get; set; }
    }
}
