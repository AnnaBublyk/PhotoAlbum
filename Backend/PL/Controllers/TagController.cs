using BLL.DTO;
using BLL.Interface;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace PL.Controllers
{
    /// <summary>
    ///   <para></para>
    ///   <para>Class TagController.
    /// Implements the <see cref="System.Web.Http.ApiController"/>
    /// </para>
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class TagController : ApiController
    {

        private IService<TagDTO> myTagService;
        /// <summary>Initializes a new instance of the <see cref="T:PL.Controllers.TagController"/> class.</summary>
        /// <param name="_myTagService">My tag service.</param>
        public TagController(IService<TagDTO> _myTagService)
        {
            myTagService = _myTagService;
        }

        /// <summary>Gets all tag.</summary>
        /// <returns>IQueryable&lt;TagDTO&gt;.</returns>
        [HttpGet]
        [Route("api/tags/get")]
        public IQueryable<TagDTO> GetAllTag()
        {
            var tags = myTagService.GetAll();
            return tags;
        }
        //  [Authorize]
        /// <summary>Adds the new tag.</summary>
        /// <param name="tag">The tag.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        [Route("api/tags/post")]
        public IHttpActionResult AddNewTag(TagDTO tag)
        {
            try
            {
                myTagService.AddNew(tag);
                return new StatusCodeResult(HttpStatusCode.Created, this);
            }
            catch (Exception)
            {

                return new StatusCodeResult(HttpStatusCode.MethodNotAllowed, this);
            }
        }
    }
}
