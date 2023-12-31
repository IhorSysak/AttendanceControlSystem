﻿using AttendanceControlSystem.Entity;
using AttendanceControlSystem.Models.UserModels;
using AutoMapper;

namespace AttendanceControlSystem.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserModel, User>()
               .ForMember(s => s.UserName, m => m.MapFrom(x => x.UserName));

            CreateMap<RegisterModel, User>()
              .ForMember(s => s.UserName, m => m.MapFrom(x => x.UserName))
              .ForMember(s => s.Role, m => m.MapFrom(x => x.Role));
        }
    }
}
