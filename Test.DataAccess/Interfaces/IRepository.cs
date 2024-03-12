using System.Linq.Expressions;

namespace Test.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? expression = null);
    }
}
