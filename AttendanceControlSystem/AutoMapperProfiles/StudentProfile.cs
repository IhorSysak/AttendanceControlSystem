using AttendanceControlSystem.Entity;
using AttendanceControlSystem.Models.StudentModels;
using AttendanceControlSystem.Utility;
using AutoMapper;

namespace AttendanceControlSystem.AutoMapperProfiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile() 
        {
            CreateMap<StudentModel, Student>()
                .ForMember(s => s.Id, m => m.MapFrom(x => x.Id))
                .ForMember(s => s.FullName, m => m.MapFrom(x => x.FullName))
                .ForMember(s => s.Course, m => m.MapFrom(x => x.Course))
                .ForMember(s => s.Group, m => m.MapFrom(x => x.Group))
                .ForMember(s => s.Photo, m => m.MapFrom(x => ItemSerializer.Serialize(x.File)));

            CreateMap<CreateStudentModel, Student>()
                .ForMember(s => s.FullName, m => m.MapFrom(x => x.FullName))
                .ForMember(s => s.Course, m => m.MapFrom(x => x.Course))
                .ForMember(s => s.Group, m => m.MapFrom(x => x.Group))
                .ForMember(s => s.Photo, m => m.MapFrom(x => ItemSerializer.Serialize(x.File)));
        }
    }
}
