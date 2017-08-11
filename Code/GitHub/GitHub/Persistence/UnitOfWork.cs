using GitHub.Core;
using GitHub.Core.Repositories;
using GitHub.Persistence.Repositories;

namespace GitHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }

        public IAttendanceRepository Attendees { get; private set; }

        public IFollowingRepository Followings { get; private set; }

        public IGenreRepository Genres { get; private set; }

        public IApplicationUserRepository Users { get; private set; }

        public INotificationRepository Notifications { get; private set; }

        public IUserNotificationRepository UserNotifications { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Attendees = new AttendanceRepository(_context);
            Followings = new FollowingRepository(_context);
            Genres = new GenreRepository(_context);
            Users = new ApplicationUserRepository(_context);
            Notifications = new NotificationRepository(_context);
            UserNotifications = new UserNotificationRepository(_context);
        }

        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
            Gigs = new GigRepository(_context);
            Attendees = new AttendanceRepository(_context);
            Followings = new FollowingRepository(_context);
            Genres = new GenreRepository(_context);
            Users = new ApplicationUserRepository(_context);
            Notifications = new NotificationRepository(_context);
            UserNotifications = new UserNotificationRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }


    }
}