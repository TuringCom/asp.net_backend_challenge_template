using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuringBackend.Models;
using TuringBackend.Models.Data;

namespace TuringBackend.Api.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly TuringBackendContext _dbContext;

        public DepartmentService(TuringBackendContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            var departments = await _dbContext
                .Department
                .ToListAsync();
            return departments;
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = await _dbContext.Department
                .FirstOrDefaultAsync(d => d.DepartmentId == id);
            return department;
        }
    }
}
