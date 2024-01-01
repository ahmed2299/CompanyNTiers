using DAL.Models;

namespace BLL.Services.Contracts
{
    public interface IChildService
    {
        public List<Child> ChildList(int EmployeeID);

        public void Add(Child child);

        public Child ChildGetInformation(int ChildID);

        public void Edit(Child child);

        public string RemoveChildByID(int EmployeeID);

    }
}
