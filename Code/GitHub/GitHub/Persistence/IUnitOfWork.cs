using GitHub.Repositories;

namespace GitHub.Persistence
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