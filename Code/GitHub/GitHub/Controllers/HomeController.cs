using System.Linq;
using System.Web.Mvc;
using GitHub.Core.Models;
using GitHub.Core.ViewModels;
using GitHub.Persistence;
using GitHub.Persistence.Repositories.Repositories;
using Microsoft.AspNet.Identity;

namespace GitHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly AttendanceRepository _attendanceRepository;
        private readonly GigRepository _gigRepository;

        public HomeController()
        {
            var context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(context);
            _gigRepository = new GigRepository(context);
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs =_gigRepository.FutureUpcomingGigsNotCanceled();

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query) ||
                                g.Genre.Name.Contains(query) ||
                                g.Venue.Contains(query));
            }


            var userId = User.Identity.GetUserId();

            var attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}