using BLL.Services.Contracts;
using DAL.Models;
using DAL.ViewModel.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        #region [List]
        /// <summary>
        /// To List Employee Data
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> List()
        {
            List<Employee> employee = new List<Employee>();
            List<EmployeeListViewModel> employeeListVM = new List<EmployeeListViewModel>();
            string departmentName = null;
            try
            {
                employee = await _employeeService.List();

                List<int> employeeIDs = employee.Select(e =>
                e.EmployeeId).ToList();

                employeeListVM = _employeeService.ListEmployeeListViewModel(employeeIDs, employee);
                return View(employeeListVM);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("List");
            }
        }
        #endregion

        #region [View]
        /// <summary>
        /// View Employee Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult View(int id)
        {
            EmployeeViewViewModel employeeViewVM = new EmployeeViewViewModel();
            try
            {
                employeeViewVM = _employeeService.GetEmployeeByID(id);
                return View(employeeViewVM);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            finally
            {

            }
        }

        #endregion

        #region [Add]
        /// <summary>
        /// Add New Employee
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            List<string> DepartmentNames = new List<string>();

            EmployeeAddViewModel employeeAddViewModel = new EmployeeAddViewModel();
            try
            {
                DepartmentNames = _employeeService.DepartmentNameGetList();

                /**************************************************/
                //To set in the dropdown list (DepartmentName)//
                /**************************************************/
                employeeAddViewModel.DepartmentIDListItem = new List<SelectListItem>();
                foreach (var item in DepartmentNames)
                {
                    SelectListItem newLst = new SelectListItem()
                    {
                        Text = item.ToString(),
                        Value = item.ToString()
                    };
                    employeeAddViewModel.DepartmentIDListItem.Add(newLst);
                }
                /**************************************************/
                //To set the radio button yes and no//
                /**************************************************/
                employeeAddViewModel.RadioButtonListItem = new List<SelectListItem>()
                {
                    new SelectListItem() {Text="Yes",Value="true"},
                    new SelectListItem() {Text="No",Value="false"}
                };
                /**************************************************/

                return View(employeeAddViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Add([FromBody] EmployeeAddViewModel employeeVM)
        {
            Employee employee = new Employee();
            try
            {
                _employeeService.Add(employeeVM);
                return Json(new { result = "Succeded" });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        #endregion

        #region [Edit]
        /// <summary>
        /// Edit Employee data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel();
            try
            {               
                employeeEditViewModel=_employeeService.EmployeeGetInformation(id);

                return View(employeeEditViewModel);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        public ActionResult Edit([FromBody] EmployeeEditViewModel employeeVM)
        {
            try
            {
                _employeeService.Edit(employeeVM);
                return Json(new { result = "Succeded" });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        #endregion

        #region [Delete]
        /// <summary>
        /// To delete Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            try
            {
                var employee= _employeeService.RemoveEmployeeByID(id);

                return RedirectToAction("List","Employee");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("List", "Employee");
            }
        }
        #endregion
    }
}
