using Test.DataAccess.Interfaces;
using Test.DataAccess.Repository;

namespace Test.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UnitOfWork(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            EmployeeRepository = new EmployeeRepository(httpClientFactory.CreateClient("EmployeeHttpClient"));
        }

        public IEmployeeRepository EmployeeRepository { get; private set; }
    }
}
