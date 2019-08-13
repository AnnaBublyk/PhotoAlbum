using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataModel
{
    public class User : IdentityUser
    {

        public virtual Profile Profile { get; set; }

    }
}
