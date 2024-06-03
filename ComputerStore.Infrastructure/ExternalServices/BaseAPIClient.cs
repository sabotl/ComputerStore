using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;


namespace ComputerStore.Infrastructure.ExternalServices
{
    public class BaseAPIClient<T>: Interfaces.IBaseApiClient<T> where T : class
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseUrl;
        protected readonly string _endpoint;
        protected BaseAPIClient(HttpClient httpClient, string baseUrl, string endpoint)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(httpClient));
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(httpClient));
        }
        private async Task<T?> GetAsync<Unique>(Unique id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{_endpoint}/{id}");
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<IEnumerable<T>?> GetAll()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{_endpoint}/");
            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<T>();
            else
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
                }catch (System.Text.Json.JsonException)
                {
                    return Enumerable.Empty<T>();
                }
            }
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await GetAsync(id);
        }
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await GetAsync(id);
        }
        public async Task<T?> GetByNameAsync(string name)
        {
            return await GetAsync(name);
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                var json = JsonConvert.SerializeObject(entity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/{_endpoint}", content);

                return response.IsSuccessStatusCode;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            try
            {
                var json = JsonConvert.SerializeObject(entity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_baseUrl}/{_endpoint}/{id}", content);

                return response.IsSuccessStatusCode;

            }catch(Exception ex)
            {
                return false;
            }
        }

        private async Task<bool> DeleteAsync<Unique>(Unique id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{_endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await DeleteAsync(id);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await DeleteAsync(id);
        }
        public async Task<bool> DeleteAsync(string id)
        {
            return await DeleteAsync(id);
        }
    }
}
