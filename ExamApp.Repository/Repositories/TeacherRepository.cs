using ExamApp.Core.Models;
using ExamApp.Repository.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Repository.Repositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teachers>> GetAllAsync();
    }

    public class TeacherRepository : ITeacherRepository
    {
        private readonly ITeacherQuery _teacherQuery;

        public TeacherRepository(ITeacherQuery teacherQuery)
        {
            _teacherQuery = teacherQuery;
        }

        public async Task<IEnumerable<Teachers>> GetAllAsync()
        {
            var result = await _teacherQuery.GetAllAsync();
            return result;
        }
    }
}
