using BLL.Services;
using BLL.Services.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using DAL.ViewModel.Department;

namespace UI.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _genericService;

        public DepartmentController(IDepartmentService genericService)
        {
            _genericService = genericService;
        }

        #region [List]
        /// <summary>
        /// To list the departments
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> List()
        {
            List<Department> departments = new List<Department>();
            List<DepartmentListViewModel> departmentListVM = new List<DepartmentListViewModel>();
            try
            {
                departments = await _genericService.List();
                departments.ForEach(d =>
                {
                    departmentListVM.Add(new DepartmentListViewModel
                    {
                        DepartmentId = d.DepartmentId,
                        DepartmentName = d.DepartmentName,
                        Budget = d.Budget,
                        DepartmentNumber = d.DepartmentNumber,
                    });
                });

                return View(departmentListVM);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("List");
            }
            finally
            {
                departments = null;
                departmentListVM = null;
            }
        }
        #endregion

        #region [View]
        /// <summary>
        /// To view department data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> View(int id)
        {
            Department department = new Department();
            DepartmentViewViewModel departmentViewVM = new DepartmentViewViewModel();
            try
            {
                department = await _genericService.GetDepartmentByID(id);

                departmentViewVM.DepartmentId = department.DepartmentId;
                departmentViewVM.DepartmentName = department.DepartmentName;
                departmentViewVM.DepartmentNumber = department.DepartmentNumber;
                departmentViewVM.Budget = department.Budget;

                return View(departmentViewVM);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("List", ViewBag.Error);
            }
        }
        #endregion

        #region [Add]
        /// <summary>
        /// To Add new department data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            DepartmentAddViewModel departmentVM = new DepartmentAddViewModel();
            try
            {
                return View(departmentVM);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("List", ViewBag.Error);
            }
            finally
            {
                departmentVM = null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DepartmentAddViewModel departmentViewModel)
        {
            Department department = new Department();
            try
            {
                department.DepartmentName = departmentViewModel.DepartmentName;
                department.DepartmentNumber = int.Parse(departmentViewModel.DepartmentNumber.ToString());
                department.Budget = departmentViewModel.Budget;
                if (department.DepartmentName == "")
                {
                    return Json(new { result= "Error Department Name is required" });
                }
                if(department.DepartmentNumber == 0)
                {
                    return Json(new { result = "Error Department Number is required" });
                }
                _genericService.Add(department);

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("List");
            }
        }
        #endregion

        #region [Edit]
        /// <summary>
        /// Edit Department data
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            Department department=new Department();
            DepartmentEditViewModel departmentEditVM = new DepartmentEditViewModel();
            try
            {
                department = await _genericService.GetDepartmentByID(id);

                departmentEditVM.DepartmentId = department.DepartmentId;
                departmentEditVM.DepartmentName = department.DepartmentName;
                departmentEditVM.DepartmentNumber = department.DepartmentNumber;
                departmentEditVM.Budget = department.Budget;

                return View(departmentEditVM);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("List", ViewBag.Error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] DepartmentEditViewModel departmentEditVM)
        {
            Department department = new Department();
            try
            {
                if (departmentEditVM == null)
                {
                    return Json(new { result = "Error While editing data" });
                }
                department.DepartmentId = departmentEditVM.DepartmentId;
                department.DepartmentName = departmentEditVM.DepartmentName;
                department.DepartmentNumber = int.Parse(departmentEditVM.DepartmentNumber.ToString());
                department.Budget = departmentEditVM.Budget;

                if (department.DepartmentName == "")
                {
                    return Json(new { result = "Error Department Name is required" });
                }
                if (department.DepartmentNumber == 0 )
                {
                    return Json(new { result = "Error Department Number is required" });
                }
                if (department.Budget == null)
                {
                    return Json(new { result = "Error Budget should be a number" });
                }
                
              

                _genericService.Edit(department);

                return RedirectToAction("List");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("List", ViewBag.Error);
            }
        }

        #endregion

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                string returnedValue=_genericService.RemoveDepartmentByID(id);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return Json(new { result = ex.Message });
            }
        }
    }
}
