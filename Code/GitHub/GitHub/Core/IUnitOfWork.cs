using GitHub.Core.Repositories;

namespace GitHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IAttendanceRepository Attendees { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        void Complete();
    }
}