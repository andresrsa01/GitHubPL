using System.Collections.Generic;
using System.Linq;
using GitHub.Core.Models;
using GitHub.Core.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

        public ApplicationUser ObtainUser(string authenticationType, string data)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var user = authenticationType != "ApplicationCookie"
                ? userManager.FindByEmail(data)
                : userManager.FindById(data);

            return user;
        }
    }
}