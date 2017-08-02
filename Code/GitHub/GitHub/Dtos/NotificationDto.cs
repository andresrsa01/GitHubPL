using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GitHub.Models;

namespace GitHub.Dtos
{
    public class NotificationDto
    {
       
        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }

        public string OriginalVenue { get; private set; }
    
        public GigDto Gig { get; set; }

    }
}