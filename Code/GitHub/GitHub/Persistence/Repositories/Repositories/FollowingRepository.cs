using System.Linq;
using GitHub.Core.Models;
using GitHub.Core.Repositories;

namespace GitHub.Persistence.Repositories.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string userId, string gigArtistId)
        {
            return _context.Followings
                   .SingleOrDefault(f => f.FolloweeId == gigArtistId && f.FollowerId == userId);
        }
    }
}