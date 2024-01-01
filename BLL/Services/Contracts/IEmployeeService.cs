using DAL.Models;
using DAL.ViewModel.Employee;

namespace BLL.Services.Contracts
{
    public interface IEmployeeService
    {

        public Task<List<Employee>> List();
        public Task<List<EmployeeListViewModel>> EmployeeGetList();
        public List<Employee> EmployeeGetListV2();

        public string GetDepartmentNameByID(int DepartmentID);

        public List<EmployeeListViewModel> ListEmployeeListViewModel(List<int> employeeIDs,List<Employee> employee);

        public EmployeeViewViewModel GetEmployeeByID(int EmployeeID);

        public List<string> DepartmentNameGetList();

        public string RemoveEmployeeByID(int EmployeeID);

        public void Add(EmployeeAddViewModel EmployeeVM);

        public void Edit(EmployeeEditViewModel EmployeeVM);

        public EmployeeEditViewModel EmployeeGetInformation(int EmployeeID);
    }
}
