using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.DataModel
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
