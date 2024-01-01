using DAL.Models;

namespace BLL.Services.Contracts
{
    public interface IDepartmentService
    {
        public Task<List<Department>> List();

        public void Add(Department department);

        public void Edit(Department department);

        public Task<Department> GetDepartmentByID(int DepartmentID);

        public string RemoveDepartmentByID(int DepartmentID);
    }
}
