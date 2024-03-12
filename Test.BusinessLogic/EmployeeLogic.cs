using System.Linq.Expressions;
using Test.BusinessLogic.Interfaces;
using Test.BusinessObjects.Models;
using Test.BusinessObjects.ViewModels;
using Test.DataAccess;

namespace Test.BusinessLogic
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAll(Expression<Func<Employee, bool>>? expression = null)
        {
            var employees = (await _unitOfWork.EmployeeRepository.GetAll(expression)).ToList();
            List<EmployeeViewModel> result = new List<EmployeeViewModel>();

            for(int i = 0; i < employees.Count(); i++)
            {
                Employee emp = employees[i];
                result.Add(new EmployeeViewModel
                {
                    Name = emp.EmployeeName,
                    TotalTimeWorked = emp.EndTimeUtc - emp.StarTimeUtc
                }) ;
            }

            return result
                        .GroupBy(e => e.Name)
                        .Select(g => new EmployeeViewModel
                        {
                            Name = g.Key,
                            TotalTimeWorked = TimeSpan.FromTicks(g.Sum(e => e.TotalTimeWorked?.Ticks ?? 0))
                        })
                        .OrderBy(e => e.TotalTimeWorked);
        }
    }
}
