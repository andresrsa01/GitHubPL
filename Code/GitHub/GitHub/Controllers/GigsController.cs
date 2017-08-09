using System.Linq;
using System.Web.Mvc;
using GitHub.Core;
using GitHub.Core.Models;
using GitHub.Core.ViewModels;
using GitHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GitHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        // GET: Gigs
        public ActionResult Create()
        {
            var vm = new GigFormViewModel()
            {
                Genres = _unitOfWork.Genres.GetGenres(),
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
                vm.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", vm);
            }
            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = vm.GetDateTime(),
                GenreId = vm.Genre,
                Venue = vm.Venue
            };
            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var vm = new GigsViewModel()
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _unitOfWork.Attendees.GetFutureAttendances(userId)
                    .ToLookup(a => a.GigId)
            };
            return View("Gigs", vm);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetUpComingGigsByArtist(userId);

            return View(gigs);
        }

        [Authorize]
        // GET: Gigs
        public ActionResult Edit(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();


            if (gig.ArtistId == User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var vm = new GigFormViewModel()
            {
                Id = gig.Id,
                Genres = _unitOfWork.Genres.GetGenres(),
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
                vm.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", vm);
            }
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(vm.Id);

            if (gig == null)
            {
                return HttpNotFound();
            }

            if (gig.ArtistId == User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            gig.Modify(vm.Venue, vm.GetDateTime(), vm.Genre);
            _unitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigsDetailsViewModel()
            {
                Gig = gig
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _unitOfWork.Attendees.GetAttendance(gig.Id, userId) != null;

                viewModel.IsFollowing = _unitOfWork.Followings.GetFollowing(userId, gig.ArtistId) != null;
            }
            return View("Details", viewModel);
        }

    }
}