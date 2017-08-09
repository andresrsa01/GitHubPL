using GitHub.Core.Models;

namespace GitHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string gigArtistId);
    }
}