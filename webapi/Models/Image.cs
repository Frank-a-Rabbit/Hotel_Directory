// this is a model for an Image
// it has an Id, a url, and a hotelId
// the hotelId is a foreign key to the Hotel model
//

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDirectory.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        public Image()
        {
            Url = "";
        }
    }
}