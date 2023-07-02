using AttendanceControlSystem.Entity;
using AttendanceControlSystem.Models.TeacherModel;
using AutoMapper;

namespace AttendanceControlSystem.AutoMapperProfiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile() 
        {
            CreateMap<User, TeacherModel>()
               .ForMember(s => s.Id, m => m.MapFrom(x => x.Id))
               .ForMember(s => s.UserName, m => m.MapFrom(x => x.UserName));
        }
    }
}
