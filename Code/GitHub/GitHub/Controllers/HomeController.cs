using System.Linq;
using System.Web.Mvc;
using GitHub.Core;
using GitHub.Core.Models;
using GitHub.Core.ViewModels;
using GitHub.Persistence;
using GitHub.Persistence.Repositories;
using Microsoft.AspNet.Identity;

namespace GitHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs =_unitOfWork.Gigs.FutureUpcomingGigsNotCanceled();

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query) ||
                                g.Genre.Name.Contains(query) ||
                                g.Venue.Contains(query));
            }


            var userId = User.Identity.GetUserId();

            var attendances = _unitOfWork.Attendees.GetFutureAttendances(userId).ToLookup(a => a.GigId);

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