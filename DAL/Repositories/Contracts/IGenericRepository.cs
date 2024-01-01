using DAL.Models;
using DAL.ViewModel.Employee;

namespace DAL.Repositories.Contracts
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        public Task<List<TModel>> List();
        public Task<List<EmployeeListViewModel>> EmployeeGetList();
        public List<Employee> EmployeeGetListV2();
        public void Add(TModel model);
        public void Edit(TModel model);
        public Task<Department> GetDepartmentByID(int DepartmentID);
        public string RemoveDepartmentByID(int DepartmentID);
        public string RemoveEmployeeByID(int EmployeeID);
        public string RemoveChildByID(int ChildID);
        public List<Department> DepartmentGetList();
        public List<Child> ChildList(int EmployeeID);

        public Child ChildGetInformation(int ChildID);

    }
}
