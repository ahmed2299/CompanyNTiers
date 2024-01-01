using BLL.Services;
using BLL.Services.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UI.Controllers
{
    public class ChildController : Controller
    {
        private readonly IChildService _childService;

        public ChildController(IChildService childService)
        {
            _childService = childService;
        }

        #region [List]
        /// <summary>
        /// To list child
        /// </summary>
        /// <returns></returns>
        public IActionResult List(int id)
        {
            List<Child> child = new List<Child>();
            try
            {
                child = _childService.ChildList(id);

                if (child.Count == 0)
                {
                    child.Add(new Child()
                    {
                        EmployeeId = id
                    });
                }

                return View(child);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        #endregion


        #region [Add]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public IActionResult Add(int EmployeeID)
        {
            Child ChildVM = new Child();
            try
            {
                ChildVM.EmployeeId = EmployeeID;
                return View(ChildVM);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                ChildVM = null;
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Child ChildVM)
        {
            try
            {
                _childService.Add(ChildVM);

                return Json(new { result = "success" });
            }
            catch (Exception ex)
            {
                string ErrorMessage;
                if (ex.InnerException != null)
                    ErrorMessage = ex.InnerException.Message;
                else
                    ErrorMessage = ex.Message;
                return Json(new { result = ErrorMessage });
            }
        }
        #endregion

        #region [Edit]

        public IActionResult Edit(int ChildID)
        {
            try
            {
                var ChildVM = _childService.ChildGetInformation(ChildID);
                return View(ChildVM);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit([FromBody] Child ChildVM)
        {
            try
            {

                _childService.Edit(ChildVM);

                return Json(new { result = "success" });
            }
            catch (Exception ex)
            {
                string ErrorMessage;
                if (ex.InnerException != null)
                    ErrorMessage = ex.InnerException.Message;
                else
                    ErrorMessage = ex.Message;
                return Json(new { result = ErrorMessage });
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
                var employee = _childService.RemoveChildByID(id);

                return RedirectToAction("List", "Employee");
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
