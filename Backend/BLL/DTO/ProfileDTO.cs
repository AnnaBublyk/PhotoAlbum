using DAL.DataModel;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class ProfileDTO
    {
        public string ProfileId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlocked { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public virtual ICollection<Photo> PhotosDTO { get; set; }
    }
}
