using System.Linq.Expressions;
using Test.BusinessObjects.Models;
using Test.BusinessObjects.ViewModels;

namespace Test.BusinessLogic.Interfaces
{
    public interface IEmployeeLogic
    {
        Task<IEnumerable<EmployeeViewModel>> GetAll(Expression<Func<Employee, bool>>? expression = null);
    }
}
