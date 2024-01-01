using BLL.Services;
using BLL.Services.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

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
            try
			{
                List<Department> departments = await _genericService.List();

                return View(departments);
			}
			catch (Exception)
			{
				throw;
			}
            finally
            {
                //departments.Clear();
            }

        }
        #endregion

        #region [Add]
        /// <summary>
        /// To Add new department data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            try
            {                
                return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
