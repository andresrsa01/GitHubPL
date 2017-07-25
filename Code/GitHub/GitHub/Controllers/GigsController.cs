using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GitHub.Models;
using GitHub.ViewModels;

namespace GitHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Gigs
        public ActionResult Create()
        {
            var vm = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };
            return View(vm);
        }
    }
}