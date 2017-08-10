using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GitHub.Core.Models;

namespace GitHub.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(u => u.UserId == userId && !u.IsRead)
                .Select(u => u.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
        }
    }
}