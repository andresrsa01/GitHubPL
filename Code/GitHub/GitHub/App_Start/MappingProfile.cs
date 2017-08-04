using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GitHub.Dtos;
using GitHub.Models;

namespace GitHub.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}