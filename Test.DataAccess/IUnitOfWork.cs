using Test.DataAccess.Interfaces;

namespace Test.DataAccess
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
    }
}