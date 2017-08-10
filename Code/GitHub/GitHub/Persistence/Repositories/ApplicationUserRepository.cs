using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GitHub.Core.Models;
using GitHub.Core.Repositories;

namespace GitHub.Persistence.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetArtistsFollowedBy(string userId)
        {
            return _context.Followings
               .Where(f => f.FollowerId == userId)
               .Select(f => f.Followee)
               .ToList();
        }

    }
}