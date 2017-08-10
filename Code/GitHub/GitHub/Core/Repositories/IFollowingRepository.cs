using GitHub.Core.Models;

namespace GitHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string gigArtistId);
        void Add(Following following);
        void Remove(Following following);
    }
}