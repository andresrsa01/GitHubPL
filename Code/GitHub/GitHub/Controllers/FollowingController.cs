using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GitHub.Dtos;
using GitHub.Models;
using Microsoft.AspNet.Identity;

namespace GitHub.Controllers
{
    public class FollowingController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingController( )
        {
            _context = new ApplicationDbContext();
        }


        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            if (_context.Attendances.Any(a => a.AttendeeId == user.Id && a.GigId == dto.GigId))
                return BadRequest("The Attendance already exists.");

        }
    }
}
