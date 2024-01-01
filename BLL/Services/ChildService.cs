using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Contracts;

namespace BLL.Services
{
    public class ChildService : IChildService
    {
        private readonly IGenericRepository<Child> _childRepository;

        public ChildService(IGenericRepository<Child> childRepository)
        {
            _childRepository = childRepository;
        }

        #region [List]
        public List<Child> ChildList(int EmployeeID)
        {
            try
            {
                return _childRepository.ChildList(EmployeeID);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region [Add]
        public async void Add(Child child)
        {
            try
            {
                _childRepository.Add(child);
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error in adding Child", ex.InnerException);
            }
        }
        #endregion

        #region [ChildGetInformation]
        public Child ChildGetInformation(int ChildID)
        {
            Child child=new Child();
            try
            {
                child=_childRepository.ChildGetInformation(ChildID);
                return child;
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error in editing Child", ex.InnerException);
            }
        }

        #endregion

        #region [Edit]
        public void Edit(Child child)
        {
            try
            {
                _childRepository.Edit(child);
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error in editng child", ex.InnerException);
            }
        }


        #endregion

        #region [Delete]
        /// <summary>
        /// Delete Employee By ID
        /// </summary>
        /// <param name="ChildID"></param>
        /// <returns></returns>
        public string RemoveChildByID(int ChildID)
        {
            try
            {
                _childRepository.RemoveChildByID(ChildID);
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
