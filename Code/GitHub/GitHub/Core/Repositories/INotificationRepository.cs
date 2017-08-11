using System.Collections.Generic;
using GitHub.Core.Models;

namespace GitHub.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNotifications(string userId);
    }
}