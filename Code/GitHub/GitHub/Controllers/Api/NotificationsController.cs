using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using AutoMapper;
using GitHub.Dtos;
using GitHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {

        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<NotificationDto> GetNewNotifications()
        {

            string att = null;
            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    att = claimsIdentity.Claims.First().Value;
                }
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var userId = User.Identity.AuthenticationType != "ApplicationCookie"
                ? userManager.FindByEmail(att)
                : userManager.FindById(att);

            var notifications = _context.UserNotifications
                .Where(u => u.UserId == userId.Id && !u.IsRead)
                .Select(u => u.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }
    }
}
