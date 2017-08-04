using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GitHub.Models;
using GitHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GitHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new {query = viewModel.SearchTerm});
        }

        [Authorize]
        // GET: Gigs
        public ActionResult Create()
        {
            var vm = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
            };
            return View("GigForm", vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Genres = _context.Genres.ToList();
                return View("GigForm", vm);
            }
            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = vm.GetDateTime(),
                GenreId = vm.Genre,
                Venue = vm.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var vm = new GigsViewModel()
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending"
            };
            return View("Gigs", vm);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId
                && g.DateTime > DateTime.Now
                && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }

        [Authorize]
        // GET: Gigs
        public ActionResult Edit(int id)
        {

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
            var vm = new GigFormViewModel()
            {
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a Gig"
            };
            return View("GigForm", vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Genres = _context.Genres.ToList();
                return View("GigForm", vm);
            }
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
             .Include(g => g.Attendances.Select(e => e.Attendee))
             .Single(g => g.Id == vm.Id && g.ArtistId == userId);

            gig.Modify(vm.Venue, vm.GetDateTime(), vm.Genre);
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }


    }
}