namespace ComputerStore.Infrastructure.ExternalServices
{
    public class ProductClient: BaseAPIClient<ProductClient>, Interfaces.IProductClient<ProductClient>
    {
        public ProductClient(HttpClient httpClient, string _baseUrl, string _endpoint):base(httpClient, _baseUrl, _endpoint)
        {

        }
    }
}
