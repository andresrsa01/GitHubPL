using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitHub.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; private set; }


        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            User = user;
            Notification = notification;
        }

        protected UserNotification()
        {

        }

        public void Read()
        {
            IsRead = true;
        }
    }
}