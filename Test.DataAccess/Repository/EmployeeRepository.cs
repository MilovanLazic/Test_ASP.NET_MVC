using Test.BusinessObjects.Models;
using Test.DataAccess.Interfaces;

namespace Test.DataAccess.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(HttpClient httpClient) : base(httpClient)
        {
            
        }
    }
}
