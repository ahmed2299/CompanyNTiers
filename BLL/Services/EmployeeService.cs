using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repositories.Contracts;
using DAL.ViewModel.Employee;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _genericRepository;

        private readonly IGenericRepository<Department> _genericRepositoryDepartment;

        public EmployeeService(IGenericRepository<Employee> genericRepository,IGenericRepository<Department> genericRepositoryDepartment) 
        {
            _genericRepository = genericRepository;
            _genericRepositoryDepartment = genericRepositoryDepartment;
        }

        #region [List]
        public async Task<List<Employee>> List()
        {
            try
            {
                return await _genericRepository.List();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region [EmployeeGetList]
        public async Task<List<EmployeeListViewModel>> EmployeeGetList()
        {
            try
            {
                return await _genericRepository.EmployeeGetList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region [EmployeeGetListV2]
        public List<Employee> EmployeeGetListV2()
        {
            try
            {
                return _genericRepository.EmployeeGetListV2();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region [GetEmployeeByID]

        public EmployeeViewViewModel GetEmployeeByID(int EmployeeID)
        {
            List<Employee> employees;
            try
            {
                employees = EmployeeGetListV2();

                var departmentID = employees
                    .Where(e => e.EmployeeId == EmployeeID)
                    .Select(e => e.DepartmentId)
                    .FirstOrDefault();

                var departmentName = GetDepartmentNameByID(departmentID);

                var employeeData = employees
                    .Where(e => e.EmployeeId == EmployeeID)
                    .Select(e => new EmployeeViewViewModel
                    {
                        EmployeeId = e.EmployeeId,
                        EmployeeName = e.EmployeeName,
                        DepartmentId = e.DepartmentId,
                        DepartmentName = departmentName,
                        IdentificationNumber = e.IdentificationNumber,
                        IsActive = e.IsActive,
                        IsManager = e.IsManager,
                        Code = e.Code,
                        HomeNumber = e.HomeNumber,
                        MobileNumber = e.MobileNumber,
                        Salary = e.Salary,
                        WorkNumber = e.WorkNumber
                        
                    }).FirstOrDefault();

                return employeeData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region [EmployeeGetListWithDepartmentName]
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> DepartmentNameGetList()
        {
            List<string> Departments;            
            try
            {
                Departments= _genericRepositoryDepartment.DepartmentGetList()
                    .Select(d=>d.DepartmentName).ToList();

                return Departments;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion

        #region [GetDepartmentNameByID]
        public string GetDepartmentNameByID(int DepartmentID)
        {
            Task<Department> departments = null;
            string departmentName = null;
            try
            {
                departmentName = _genericRepositoryDepartment.GetDepartmentByID(DepartmentID).Result.DepartmentName;
                return departmentName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region [ListEmployeeListViewModel]
        public List<EmployeeListViewModel> ListEmployeeListViewModel(List<int> employeeIDs, List<Employee> employee)
        {
            List<EmployeeListViewModel> employeeListVM = new List<EmployeeListViewModel>();
            string departmentName = null;
            try
            {
                foreach (var ID in employeeIDs)
                {
                    var departmentID = employee.Where(e => e.EmployeeId == ID)
                        .Select(e => e.DepartmentId).FirstOrDefault();
                    departmentName = GetDepartmentNameByID(departmentID);
                    var employeeListViewModel = employee
                        .Where(e => e.EmployeeId == ID)
                        .Select(e => new EmployeeListViewModel
                        {
                            EmployeeId = e.EmployeeId,
                            DepartmentName = departmentName,
                            DepartmentId = e.DepartmentId,
                            EmployeeName = e.EmployeeName,
                            IdentificationNumber = e.IdentificationNumber,
                            IsActive = e.IsActive,
                            IsManager = e.IsManager
                        }).FirstOrDefault();
                    employeeListVM.Add(employeeListViewModel);
                }
                return employeeListVM;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                departmentName = null;
                employeeListVM = null;
            }
        }

        #endregion

        #region [Add]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeVM"></param>
        public void Add(EmployeeAddViewModel employeeVM)
        {
            Employee employee=new Employee();
            int departmentID;
            try
            {

                departmentID = _genericRepositoryDepartment.DepartmentGetList()
                    .Where(d => d.DepartmentName == employeeVM.DepartmentName)
                    .Select(d => d.DepartmentId)
                    .FirstOrDefault();

                employee.EmployeeName = employeeVM.EmployeeName;
                employee.DepartmentId = departmentID;
                employee.Code = employeeVM.Code;
                employee.HomeNumber = employeeVM.HomeNumber;
                employee.MobileNumber = employeeVM.MobileNumber;
                employee.WorkNumber = employeeVM.WorkNumber;
                employee.Salary = employeeVM.Salary;
                employee.IsActive = employeeVM.IsActive;
                employee.IsManager = employeeVM.IsManager;
                employee.IdentificationNumber = employeeVM.IdentificationNumber;

                _genericRepository.Add(employee);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion

        #region [Edit]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public EmployeeEditViewModel EmployeeGetInformation(int EmployeeID)
        {
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel();
            try
            {
                /**************************************************/
                //To set in the dropdown list (DepartmentName)//
                /**************************************************/
                List<string> DepartmentNameList = DepartmentNameGetList();
                employeeEditViewModel.DepartmentIDListItem = new List<SelectListItem>();
                foreach (var item in DepartmentNameList)
                {
                    SelectListItem newLst = new SelectListItem()
                    {
                        Text = item.ToString(),
                        Value = item.ToString()
                    };
                    employeeEditViewModel.DepartmentIDListItem.Add(newLst);
                }
                /**************************************************/
                //To set the radio button yes and no//
                /**************************************************/
                employeeEditViewModel.RadioButtonListItem = new List<SelectListItem>()
                {
                    new SelectListItem() {Text="Yes",Value="true"},
                    new SelectListItem() {Text="No",Value="false"}
                };
                /**************************************************/
                var employee = EmployeeGetListV2()
                    .Where(e => e.EmployeeId == EmployeeID)
                    .Select(e => new EmployeeEditViewModel
                    {
                        EmployeeId = e.EmployeeId,
                        EmployeeName = e.EmployeeName,
                        DepartmentId = e.DepartmentId,
                        DepartmentName = GetDepartmentNameByID(e.DepartmentId),
                        IdentificationNumber = e.IdentificationNumber,
                        IsActive = e.IsActive,
                        IsManager = e.IsManager,
                        MobileNumber = e.MobileNumber,
                        HomeNumber = e.HomeNumber,
                        WorkNumber = e.WorkNumber,
                        Code = e.Code,
                        Salary = e.Salary,
                        DepartmentIDListItem = employeeEditViewModel.DepartmentIDListItem,
                        RadioButtonListItem = employeeEditViewModel.RadioButtonListItem
                    })
                    .FirstOrDefault();

                    return employee;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void Edit(EmployeeEditViewModel employeeVM)
        {
            Employee employee = new Employee();
            int departmentID;
            try
            {

                departmentID = _genericRepositoryDepartment.DepartmentGetList()
                    .Where(d => d.DepartmentName == employeeVM.DepartmentName)
                    .Select(d => d.DepartmentId)
                    .FirstOrDefault();

                employee.EmployeeId = employeeVM.EmployeeId;
                employee.EmployeeName = employeeVM.EmployeeName;
                employee.DepartmentId = departmentID;
                employee.Code = employeeVM.Code;
                employee.HomeNumber = employeeVM.HomeNumber;
                employee.MobileNumber = employeeVM.MobileNumber;
                employee.WorkNumber = employeeVM.WorkNumber;
                employee.Salary = employeeVM.Salary;
                employee.IsActive = employeeVM.IsActive;
                employee.IsManager = employeeVM.IsManager;
                employee.IdentificationNumber = employeeVM.IdentificationNumber;

                _genericRepository.Edit(employee);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region [Delete]
        /// <summary>
        /// Delete Employee By ID
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public string RemoveEmployeeByID(int EmployeeID)
        {
            try
            {
                _genericRepository.RemoveEmployeeByID(EmployeeID);
                return "Done";
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error while deleting Employee information", ex.InnerException);

            }
        }

        #endregion
    }
}
