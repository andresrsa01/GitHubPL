using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GitHub.Models;
using Microsoft.AspNet.Identity;

namespace GitHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCanceled)
                return NotFound();

            gig.IsCanceled = true;

            var notification = new Notification(NotificationType.GigCanceled, gig);

            var attendees = _context.Attendances
                .Where(a => a.GigId == gig.Id)
                .Select(a => a.Attendee)
                .ToList();

            foreach (var attendee in attendees)
            {
                attendee.Notify(notification);
            }
            _context.SaveChanges();

            return Ok();
        }
    }
}
