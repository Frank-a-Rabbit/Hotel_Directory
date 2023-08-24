using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDirectory.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public string Description { get; set; }
        public string WebUrl { get; set; }

        public Hotel()
        {
            Name = "";
            Location = "";
            Images = new List<Image>();
            Amenities = new List<Amenity>();
            Description = "";
            WebUrl = "";
        }
    }
}