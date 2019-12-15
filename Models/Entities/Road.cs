using System.ComponentModel.DataAnnotations;

namespace app.web.Models.Entities
{
    public class Road
    {
        [Key]
        public uint Id { get; set; }

        [Required]
        [Range(ModelConstants.MinRoadDistance, double.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double Distance { get; set; }

        [Required]
        public uint Place1Id { get; set; }

        [Required]
        public uint Place2Id { get; set; }

        public Place Place1 { get; set; }

        public Place Place2 { get; set; }
    }
}