using System.ComponentModel.DataAnnotations;

namespace app.web.Models.Entities
{
    public class Place
    {
        [Key]
        public uint Id { get; set; }

        public string Name { get; set; }
    }
}