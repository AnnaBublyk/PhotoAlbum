using DAL.DataModel;
using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class PhotoDTO
    {
        public int PhotoId { get; set; }
        public string ProfileId { get; set; }
        public string UserName { get; set; }
        public string PictureName { get; set; }
        public string PicturePath { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
