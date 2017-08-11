using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using GitHub.Core;
using GitHub.Core.Dtos;
using GitHub.Core.Models;
using GitHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GitHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController() : this(new UnitOfWork())
        {

        }

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task<IEnumerable<NotificationDto>> GetNewNotifications()
        //{
        //    string att = null;
        //    var userId = string.Empty;
        //    if (User.Identity.IsAuthenticated && User.Identity.AuthenticationType == "Bearer")
        //    {
        //        var claimsIdentity = User.Identity as ClaimsIdentity;
        //        if (claimsIdentity != null)
        //        {
        //            att = claimsIdentity.Claims.First().Value;
        //        }
        //        _context = new ApplicationDbContext();

        //        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        //        var user = await userManager.FindByEmailAsync(att);
        //        userId = user.Id;
        //    }
        //    else if (User.Identity.IsAuthenticated && User.Identity.AuthenticationType == "ApplicationCookie")
        //    {
        //        userId = User.Identity.GetUserId();
        //    }
        //    var notifications = _context.UserNotifications
        //        .Where(un => un.UserId == userId && !un.IsRead)
        //        .Select(un => un.Notification)
        //        .Include(un => un.Gig.Artist)
        //        .ToList();

        //    return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        //return notifications.Select(n => new NotificationDto()
        //{
        //    DateTime = n.DateTime,
        //    Gig = new GigDto()
        //    {
        //        Artist = new UserDto()
        //        {
        //            Id = n.Gig.Artist.Id,
        //            Name = n.Gig.Artist.Name
        //        },
        //        DateTime = n.Gig.DateTime,
        //        Id = n.Gig.Id,
        //        IsCanceled = n.Gig.IsCanceled,
        //        Venue = n.Gig.Venue,
        //        Genre = new GenreDto()
        //        {
        //            Id = n.Gig.Genre.Id,
        //            Name = n.Gig.Genre.Name
        //        }
        //    },
        //    OriginalVenue = n.OriginalVenue,
        //    OriginalDateTime = n.DateTime,
        //    Type = n.Type
        //});
        //}

        //[Route("Api/GetNewNotifications")]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {

            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.Notifications.GetNotifications(userId);
            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.UserNotifications.GetUserNotificationsUnRead(userId);
            notifications.ForEach(n => n.Read());
            _unitOfWork.Complete();

            return Ok();

        }
    }
}
