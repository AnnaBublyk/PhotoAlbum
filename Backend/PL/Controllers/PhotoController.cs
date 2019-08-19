using BLL.DTO;
using BLL.Interface;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace PL.Controllers
{
    /// <summary>Class PhotoController.
    /// Implements the <see cref="System.Web.Http.ApiController"/></summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class PhotoController : ApiController
    {
        IPhotoService myPhotoService;
        /// <summary>Initializes a new instance of the <see cref="T:PL.Controllers.PhotoController"/> class.</summary>
        /// <param name="_myPhotoService">My photo service.</param>
        public PhotoController(IPhotoService _myPhotoService)
        {
            myPhotoService = _myPhotoService;
        }

        /// <summary>Deletes the photo.</summary>
        /// <param name="photo">The photo.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        [Route("api/photos/delete")]
        public IHttpActionResult DeletePhoto( PhotoDTO photo)
        {
            //var photo = myPhotoService.GetAll();
            if (photo != null)
            {
                myPhotoService.Delete(photo);
                return Ok();
            }
            else
                return new StatusCodeResult(HttpStatusCode.MethodNotAllowed, this);

        }

        /// <summary>Adds the like.</summary>
        /// <param name="photo">The photo.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        [Route("api/photo/like")]
        public IHttpActionResult AddLike(PhotoDTO photo)
        {
            if (photo !=null)
            {
                myPhotoService.AddLike(photo);
                photo = myPhotoService.GetById(photo.PhotoId);
                return Ok(photo);
            }
            else
                return new StatusCodeResult(HttpStatusCode.MethodNotAllowed, this);
        }
        /// <summary>Removes the like.</summary>
        /// <param name="photo">The photo.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        [Route("api/photo/unlike")]
        public IHttpActionResult RemoveLike(PhotoDTO photo)
        {
            if (photo != null)
            {
                myPhotoService.RemoveLike(photo);
                photo = myPhotoService.GetById(photo.PhotoId);
                return Ok(photo);
            }
            else
                return new StatusCodeResult(HttpStatusCode.MethodNotAllowed, this);
        }

        /// <summary>Searches the photo.</summary>
        /// <param name="req">The req.</param>
        /// <returns>IQueryable&lt;PhotoDTO&gt;.</returns>
        [Route("api/photos/get")]
        [HttpPost]
        public IQueryable<PhotoDTO> SearchPhoto([FromBody] dynamic req)
        {
            int[] masTag = req.masTag.ToObject<int[]>();
            bool isOld = req.isOld;
            string profileId = req.profileId;
            var photos = myPhotoService.SeachPhoto(masTag, isOld, profileId );
            return photos;
        }

        /// <summary>Uploads the photo.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        [Route("api/photo/add/{id}")]
        public IHttpActionResult Upload(string id)
        {
            var file = HttpContext.Current.Request.Files[0];
            string path = AppDomain.CurrentDomain.BaseDirectory + "Data/";
            string filename = Path.GetFileName(file.FileName);
            string fullPath= Path.Combine(path, filename);
            if (filename != null)
                file.SaveAs(Path.Combine(path, filename));
            var photoId= myPhotoService.UploadToDb(path, filename, id);
            return Ok(photoId);
        }

        /// <summary>Adds the tags to photo.</summary>
        /// <param name="req">The req.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        [Route("api/photo/tags/add")]
        public IHttpActionResult AddTags([FromBody] dynamic req)
        {
            int[] masTag = req.Tags.ToObject<int[]>();
            int photoId = req.PhotoId;
            myPhotoService.AddTagToDb(photoId, masTag);
            return Ok();
        }
    }
}
