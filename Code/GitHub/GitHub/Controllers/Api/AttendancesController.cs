using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHub.Core.Dtos;
using GitHub.Core.Models;
using GitHub.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        //TODO tipo de autenticacion
        [HttpPost]
        public async Task<IHttpActionResult> Attend(AttendanceDto dto)
        {
            string att = null;
            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    att = claimsIdentity.Claims.First().Value;
                }
            }
            _context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var user = User.Identity.AuthenticationType != "ApplicationCookie"
                ? await userManager.FindByEmailAsync(att)
                : await userManager.FindByIdAsync(att);

            if (_context.Attendances.Any(a => a.AttendeeId == user.Id && a.GigId == dto.GigId))
                return BadRequest("The Attendance already exists.");

            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = user.Id
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            string att = null;
            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    att = claimsIdentity.Claims.First().Value;
                }
            }
            _context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var user = User.Identity.AuthenticationType != "ApplicationCookie"
                ?  userManager.FindByEmail(att)
                :  userManager.FindById(att);

            var attendance = _context.Attendances
                .SingleOrDefault(a => a.AttendeeId == user.Id && a.GigId == id);

            if (attendance == null)
                return NotFound();

            _context.Attendances.Remove(attendance);
            _context.SaveChanges();
            return Ok(id);

        }
    }
}
