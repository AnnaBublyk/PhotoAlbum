using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataModel
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        [Required]
        public string ProfileId { get; set; }
        [Required]
        public int PhotoId { get; set; }
    }
}
