﻿using System.Web.Http;
using GitHub.Core;
using Microsoft.AspNet.Identity;

namespace GitHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            if (gig == null || gig.IsCanceled)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();

            gig.Cancel();
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
