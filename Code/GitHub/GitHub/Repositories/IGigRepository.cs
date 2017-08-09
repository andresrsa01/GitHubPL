using System.Collections.Generic;
using GitHub.Models;

namespace GitHub.Repositories
{
    public interface IGigRepository
    {
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetUpComingGigsByArtist(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        IEnumerable<Gig> FutureUpcomingGigsNotCanceled();
        void Add(Gig gig);
    }
}