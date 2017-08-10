using System.Collections.Generic;
using GitHub.Core.Models;

namespace GitHub.Core.Repositories
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetArtistsFollowedBy(string userId);
    }
}