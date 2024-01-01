using DAL.Models;
using DAL.Repositories.Contracts;
using DAL.ViewModel.Employee;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly CompanyContext _context;

        public GenericRepository(CompanyContext context)
        {
            _context = context;
        }

        #region [List]
        public async Task<List<TModel>> List()
        {
            try
            {
                return await _context.Set<TModel>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error", ex.InnerException);
            }
        }


        public async Task<List<EmployeeListViewModel>> EmployeeGetList()
        {
            try
            {
                return await _context.Set<EmployeeListViewModel>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error", ex.InnerException);
            }
        }


        public List<Employee> EmployeeGetListV2()
        {
            try
            {
                return _context.Set<Employee>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error", ex.InnerException);
            }
        }

        public List<Department> DepartmentGetList()
        {
            try
            {
                return _context.Set<Department>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error", ex.InnerException);
            }
        }

        public List<Child> ChildList(int EmployeeID)
        {
            try
            {
                return _context.Set<Child>().ToList().Where(c => c.EmployeeId == EmployeeID).ToList();
                //return _context.Set<Child>().ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region [Add]
        public async void Add(TModel model)
        {
            try
            {
                _context.AddAsync(model);
                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error some data are incorrect while adding", ex.InnerException);
            }
        }

        #endregion

        #region [Edit]
        public async void Edit(TModel model)
        {
            try
            {
                _context.Update(model);
                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error some data are incorrect while editing", ex.InnerException);
            }
        }

        public Task<Department> GetDepartmentByID(int DepartmentID)
        {
            try
            {
                return _context.Departments.FirstOrDefaultAsync(d=>d.DepartmentId==DepartmentID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error While getting department data by ID", ex.InnerException);
            }
        }
        #endregion

        #region [Delete]
        public string RemoveDepartmentByID(int DepartmentID)
        {
            Department? department = new Department();
            try
            {
                department = _context.Departments.Where(d => d.DepartmentId == DepartmentID).FirstOrDefault();
                _context.Remove(department);
                _context.SaveChanges();
                return "done";
            }
            catch (Exception ex)
            {
                throw new Exception("Error While deleting department"+ex.Message);
            }
            finally
            {
            }
        }

        public string RemoveEmployeeByID(int EmployeeID)
        {
            Employee? employee = new Employee();
            try
            {
                employee = _context.Employees.Where(e => e.EmployeeId == EmployeeID).FirstOrDefault();
                _context.Remove(employee);
                _context.SaveChanges();
                return "done";
            }
            catch (Exception ex)
            {
                throw new Exception("Error While deleting employee" + ex.Message);
            }
        }
        #endregion

        #region [ChildGetInformation]

        public Child ChildGetInformation(int ChildID)
        {
            try
            {          
                return _context.Children.Where(c => c.ChildId == ChildID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error While editing child" + ex.Message);
            }
        }

        #endregion

        #region [RemoveChildByID]

        public string RemoveChildByID(int ChildID)
        {
            Child? child = new Child();
            try
            {
                child = _context.Children.Where(c => c.ChildId == ChildID).FirstOrDefault();
                _context.Remove(child);
                _context.SaveChanges();
                return "done";
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

    }
}
