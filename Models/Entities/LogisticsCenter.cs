using System.ComponentModel.DataAnnotations;

namespace app.web.Models.Entities
{
    public class LogisticsCenter
    {
        [Key]
        public uint Id { get; set; }
        
        public uint PlaceId { get; set; }
        
        public Place Place { get; set; }
    }
}