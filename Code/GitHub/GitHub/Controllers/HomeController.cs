using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using GitHub.Models;
using GitHub.ViewModels;

namespace GitHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcomingGigs = _context.Gigs
                .Include(gig => gig.Artist)
                .Include(g => g.Genre)
                .Where(gig => gig.DateTime > DateTime.Now);

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated
            };

            return View(viewModel);
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