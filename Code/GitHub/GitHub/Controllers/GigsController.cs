using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
        [Authorize]
        // GET: Gigs
        public ActionResult Create()
        {
            var vm = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel vm)
        {
            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = DateTime.Parse($"{vm.Date} {vm.Time}"),
                GenreId = vm.Genre,
                Venue = vm.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}