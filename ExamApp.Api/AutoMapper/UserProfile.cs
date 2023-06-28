using AutoMapper;
using ExamApp.Core.DTO;
using ExamApp.Core.Models;

namespace ExamApp.Api.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<ExamResultPostModel, ExamResult>();

            CreateMap<SubjectPostModel, Subject>();

            CreateMap<StudentPostModel, Student>();
            CreateMap<StudentPutModel, Student>();
        }
    }
}
