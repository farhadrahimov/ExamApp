using AutoMapper;
using ExamApp.Core.DTO;
using ExamApp.Core.Models;

namespace ExamApp.Api.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<ExamResultPostModel, ExamResults>();

            CreateMap<SubjectPostModel, Subjects>();

            CreateMap<StudentPostModel, Students>();
            CreateMap<StudentPutModel, Students>();
        }
    }
}
