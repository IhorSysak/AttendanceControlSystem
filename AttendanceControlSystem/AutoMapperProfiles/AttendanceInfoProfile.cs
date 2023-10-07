using AttendanceControlSystem.Entity;
using AttendanceControlSystem.Models.AttendanceInfoModels;
using AutoMapper;

namespace AttendanceControlSystem.AutoMapperProfiles
{
    public class AttendanceInfoProfile : Profile
    {
        public AttendanceInfoProfile() 
        {
            CreateMap<AttendanceInfoModel, AttendanceInfo>()
                .ForMember(a => a.Id, m => m.MapFrom(x => x.Id))
                .ForMember(a => a.Time, m => m.MapFrom(x => x.Time));

            CreateMap<CreateAttendanceInfoModel, AttendanceInfo>()
                .ForMember(a => a.Time, m => m.MapFrom(x => x.Time))
                .ForPath(a => a.Student.Id, m => m.MapFrom(x => x.Student.Id))
                .ForPath(a => a.Student.FullName, m => m.MapFrom(x => x.Student.FullName))
                .ForPath(a => a.Student.Course, m => m.MapFrom(x => x.Student.Course))
                .ForPath(a => a.Student.Group, m => m.MapFrom(x => x.Student.Group))
                .ForPath(a => a.Student.ImagePath, m => m.MapFrom(x => x.Student.ImagePath));
        }
    }
}
