using GitHub.Models;

namespace GitHub.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string gigArtistId);
    }
}