using Newtonsoft.Json;
using System.Linq.Expressions;
using Test.DataAccess.Interfaces;

namespace Test.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HttpClient _httpClient;

        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? expression = null)
        {
            var response = await _httpClient.GetAsync("");
            
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
                
            var entities = JsonConvert.DeserializeObject<IEnumerable<T>>(content);
            return expression != null ? entities?.AsQueryable().Where(expression).ToList() : entities;
            
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> expression)
        {
            var response = await _httpClient.GetAsync("");
            
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var entities = JsonConvert.DeserializeObject<IEnumerable<T>>(content);
            return entities.AsQueryable().FirstOrDefault(expression);
           
        }
    }
}
