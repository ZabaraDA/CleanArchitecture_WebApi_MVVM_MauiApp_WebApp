
using Frontend.MauiApp.Core.Application.Interfaces;
using Frontend.MauiApp.Core.Domain.Models;
using Frontend.MauiApp.Infrastructure.Utils;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Frontend.MauiApp.Infrastructure.DataManager
{
    public class DataManager : IDataManager
    {
        private HttpClient _httpClient;
        private Token _token;
        public DataManager(Token token, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _token = token;
        }

        public void Authorization(string login, string password)
        {
            _token.Name = GetToken(_token.BaseUrl, login, password).Result;
        }

        public async Task<T> GetItems<T>(string pointName, Dictionary<string, string> parameters = null)
        {
            try
            {
                //Адрес сервиса с токеном
                //string urlService = _token.GetUrlApiService(pointName);
                //var paramString = parameters.GetParameters();
                //var url = new Uri($"{urlService}{paramString}");

                var responseData = await _httpClient.GetStreamAsync("https://localhost:7106/api/Product");
                var result = await JsonSerializer.DeserializeAsync<T>(responseData);


                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        private async Task<string> GetToken(string url, string login, string password)
        {
            var authorizationUrl = $"{url}/token";
            var responce = await GetRequestAsync<TokenResponse>(authorizationUrl, new Dictionary<string, string>()
            {
                {"login",login },
                {"password",password }
            });
            //Если result есть в ответе, значит ошибка аутентификации
            if (!string.IsNullOrEmpty(responce.Result))
            {
                throw new Exception(responce.Result);
            }
            //Полученный с сервера токен
            return responce.Token;
        }
        private async Task<T> GetRequestAsync<T>(string url, Dictionary<string, string> pars)
        {
            var paramString = pars.GetParameters();
            var destUrl = $"{url}?{paramString}";
            var responseData = await _httpClient.GetStringAsync(new Uri(destUrl));
            var result = JsonSerializer.Deserialize<T>(responseData);
            return result;
        }
    }
}
