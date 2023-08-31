using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Xunit_API.Models
{
    [Table("model")]
    public class Model
    {
        [Key]
        public int ModelId { get; set; }
        public int BrandId { get; set; }
        public string modelname { get; set; }
        public string Description { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsActive { get; set; }

        [JsonIgnore]
        public brand? vehicle_brands { get; set; }
    }
}
