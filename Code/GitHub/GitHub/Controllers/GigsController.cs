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
            var artistId = User.Identity.GetUserId();
            var artist = _context.Users.Single(u => u.Id == artistId );
            var genre = _context.Genres.Single(g => g.Id == vm.Genre);
            var gig = new Gig()
            {
                Artist = artist,
                DateTime = DateTime.Parse($"{vm.Date} {vm.Time}"),
                Genre = genre,
                Venue = vm.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}