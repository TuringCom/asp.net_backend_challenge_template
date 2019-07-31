using System.Collections.Generic;
using System.Threading.Tasks;
using TuringBackend.Models;

namespace TuringBackend.Api.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
    }
}