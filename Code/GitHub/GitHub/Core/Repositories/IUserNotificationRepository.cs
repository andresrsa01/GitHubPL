using System.Collections.Generic;
using GitHub.Core.Models;

namespace GitHub.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        List<UserNotification> GetUserNotificationsUnRead(string userId);
    }
}