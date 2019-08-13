using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataModel
{
    public class Profile
    {
        public Profile()
        {
            Photos = new HashSet<Photo>();
        }

        [Key]
        [ForeignKey("User")]
        public string ProfileId { get; set; }

        [Required]
        [MaxLength(60), MinLength(3)]
        public string UserName { get; set; }

        [MaxLength(50), MinLength(3)]
        public string FirstName { get; set; }

        [MaxLength(50), MinLength(3)]
        public string LastName { get; set; }
        public bool IsBlocked { get; set; } = false;
        public string Role { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }

        public virtual User User { get; set; }

    }
}
