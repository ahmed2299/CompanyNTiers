using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Contracts;

namespace BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IGenericRepository<Department> _genericRepository;

        public DepartmentService(IGenericRepository<Department> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        #region [List]
        public async Task<List<Department>> List()
        {
            try
            {
                return await _genericRepository.List();
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error in listing the department data", ex.InnerException);
            }
        }
        #endregion

        #region [Add]
        public async void Add(Department department)
        {
            try
            {
                _genericRepository.Add(department);
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error in adding department", ex.InnerException);
            }
        }
        #endregion

        #region [Edit]
        public async void Edit(Department department)
        {
            try
            {
                _genericRepository.Edit(department);
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error in editng department", ex.InnerException);
            }
        }

        public Task<Department> GetDepartmentByID(int DepartmentID)
        {
            try
            {
               return _genericRepository.GetDepartmentByID(DepartmentID);
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error while getting department information", ex.InnerException);
            }
        }
        #endregion

        #region [Delete]
        public string RemoveDepartmentByID(int DepartmentID)
        {
            try
            {
                _genericRepository.RemoveDepartmentByID(DepartmentID);
                return "Done";
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error while deleting department information", ex.InnerException);

            }
        }
        #endregion

        #region [Delete]
        /// <summary>
        /// Delete Child By ID
        /// </summary>
        /// <param name="ChildID"></param>
        /// <returns></returns>
        public string RemoveChildByID(int ChildID)
        {
            try
            {
                _genericRepository.RemoveChildByID(ChildID);
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
