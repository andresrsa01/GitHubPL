using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GitHub.Core.Models;
using GitHub.Core.Repositories;

namespace GitHub.Persistence.Repositories.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Gig GetGig(int gigId)
        {
           return  _context.Gigs
                .Include(g=>g.Artist)
                .Include(g=>g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetUpComingGigsByArtist(string userId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == userId
                            && g.DateTime > DateTime.Now
                            && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
             .Include(g => g.Attendances.Select(e => e.Attendee))
             .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> FutureUpcomingGigsNotCanceled()
        {
            return _context.Gigs
                .Include(gig => gig.Artist)
                .Include(g => g.Genre)
                .Where(gig => gig.DateTime > DateTime.Now && !gig.IsCanceled)
                .ToList();
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}