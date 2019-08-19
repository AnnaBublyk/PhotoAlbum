using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.DataModel
{
    public class User : IdentityUser
    {

        public virtual Profile Profile { get; set; }

    }
}
