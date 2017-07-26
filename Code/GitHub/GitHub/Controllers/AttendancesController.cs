using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Attend([FromBody]int gigId)
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
            var user = await userManager.FindByEmailAsync(att);
            if (_context.Attendances.Any(a => a.AttendeeId == user.Id && a.GigId == gigId))
                return BadRequest("The Attendance already exists.");

            var attendance = new Attendance()
            {
                GigId = gigId,
                AttendeeId = user.Id
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }
    }
}
