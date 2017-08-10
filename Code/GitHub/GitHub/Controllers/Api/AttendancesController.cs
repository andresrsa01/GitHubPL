using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHub.Core;
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
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            var atten = _unitOfWork.Attendees.GetAttendance(dto.GigId, user.Id);
            if (atten != null)
                return BadRequest("The Attendance already exists.");


            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = user.Id
            };
            _unitOfWork.Attendees.Add(attendance);
            _unitOfWork.Complete();
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
                ? userManager.FindByEmail(att)
                : userManager.FindById(att);

            var atten = _unitOfWork.Attendees.GetAttendance(id, user.Id);
            if (atten != null)
                return NotFound();

            _unitOfWork.Attendees.Remove(attendance);
            _unitOfWork.Complete();
            return Ok(id);

        }
    }
}
