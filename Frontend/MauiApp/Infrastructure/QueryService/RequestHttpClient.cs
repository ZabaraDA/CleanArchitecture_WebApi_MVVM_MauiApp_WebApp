namespace Frontend.MauiApp.Infrastructure.QueryService
{
    public class RequestHttpClient
    {
        private HttpClient _httpClient;

        public RequestHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}