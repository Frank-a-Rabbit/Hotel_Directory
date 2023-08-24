// this is a model for an Amenity
// it has an Id, a name, and a hotelId
// the hotelId is a foreign key to the Hotel model

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDirectory.Models
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        public Amenity()
        {
            Name = "";
        }
    }
}