using AttendanceControlSystem.Entity;
using AttendanceControlSystem.Models.StudentModels;
using AutoMapper;

namespace AttendanceControlSystem.AutoMapperProfiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile() 
        {
            CreateMap<StudentModel, Student>()
                .ForMember(s => s.Id, m => m.MapFrom(x => x.Id))
                .ForMember(s => s.FirstName, m => m.MapFrom(x => x.FirstName))
                .ForMember(s => s.LastName, m => m.MapFrom(x => x.LastName))
                .ForMember(s => s.MiddleName, m => m.MapFrom(x => x.MiddleName))
                .ForMember(s => s.Course, m => m.MapFrom(x => x.Course))
                .ForMember(s => s.Group, m => m.MapFrom(x => x.Group))
                .ForMember(s => s.ImagePath, m => m.MapFrom(x => x.ImagePath));

            CreateMap<CreateStudentModel, Student>()
                .ForMember(s => s.FirstName, m => m.MapFrom(x => x.FirstName))
                .ForMember(s => s.LastName, m => m.MapFrom(x => x.LastName))
                .ForMember(s => s.MiddleName, m => m.MapFrom(x => x.MiddleName))
                .ForMember(s => s.Course, m => m.MapFrom(x => x.Course))
                .ForMember(s => s.Group, m => m.MapFrom(x => x.Group))
                .ForMember(s => s.ImagePath, m => m.MapFrom(x => x.ImagePath));
        }
    }
}
