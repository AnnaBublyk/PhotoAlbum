using DAL.DataModel;
using System.Collections.Generic;

namespace BLL.DTO
{
   public class TagDTO
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
