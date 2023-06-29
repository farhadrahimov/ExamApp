using ExamApp.Core.Models;
using ExamApp.Repository.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Repository.Repositories
{
    public interface IClassRepository
    {
        Task<IEnumerable<Classes>> GetAllAsync();
    }

    public class ClassRepository : IClassRepository
    {
        private readonly IClassQuery _classQuery;

        public ClassRepository(IClassQuery classQuery)
        {
            _classQuery = classQuery;
        }

        public async Task<IEnumerable<Classes>> GetAllAsync()
        {
            var result = await _classQuery.GetAllAsync();
            return result;
        }
    }
}
