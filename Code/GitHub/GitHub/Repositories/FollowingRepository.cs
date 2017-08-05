using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GitHub.Models;

namespace GitHub.Repositories
{
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string userId, string gigArtistId)
        {
            return _context.Followings
                   .Single(f => f.FolloweeId == gigArtistId && f.FollowerId == userId);
        }
    }
}