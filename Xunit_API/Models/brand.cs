using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Xunit_API.Models
{
    [Table("brand")]
    public class brand
    {
        [Key]
        public int BrandId { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int VehicleTypeId { get; set; }
        public int SortOrder { get; set; }
        public bool? IsActive { get; set; }

        [JsonIgnore]
        public VehicleType? vehicle_type { get; set; }
        [JsonIgnore]
        public ICollection<Model>? vehicle_model { get; set; }
    }
}
