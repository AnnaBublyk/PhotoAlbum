using BLL.DTO;
using BLL.Interface;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace PL.Controllers
{
    /// <summary>Class ProfileController.
    /// Implements the <see cref="System.Web.Http.ApiController"/></summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ProfileController : ApiController
    {
        IProfileService myProfileService;
        /// <summary>Initializes a new instance of the <see cref="T:PL.Controllers.ProfileController"/> class.</summary>
        /// <param name="_myProfileService">My profile service.</param>
        public ProfileController(IProfileService _myProfileService)
        {
            myProfileService = _myProfileService;
        }

        /// <summary>Blocks the profile.</summary>
        /// <param name="profile">The profile.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        [Route("api/user/block")]
        public IHttpActionResult BlockProfile(ProfileDTO profile)
        {
           
            if (profile != null)
            {
                myProfileService.BlockProfile(profile);
                return Ok();
            }
            else
                return new StatusCodeResult(HttpStatusCode.MethodNotAllowed, this);
        }

        /// <summary>Unblocks the profile.</summary>
        /// <param name="profile">The profile.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        [Route("api/user/unblock")]
        public IHttpActionResult UnblockProfile(ProfileDTO profile)
        {

            if (profile != null)
            {
                myProfileService.UnblockProfile(profile);
                return Ok();
            }
            else
                return new StatusCodeResult(HttpStatusCode.MethodNotAllowed, this);
        }

        /// <summary>Finds the profile.</summary>
        /// <param name="prof">The prof.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        [Route("api/profile/info/get")]
        public IHttpActionResult FindProfile(ProfileDTO prof)
        {
            var profile = myProfileService.GetProfileByUserId(prof);
            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }
    }
}
