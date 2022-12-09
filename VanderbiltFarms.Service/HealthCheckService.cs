using VanderbiltFarms.Service.Interfaces;

namespace VanderbiltFarms.Service
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://5rcjffzih8.execute-api.us-east-1.amazonaws.com/";

        public HealthCheckService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> HealthCheck()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}api/healthcheck");

            return response.IsSuccessStatusCode ? true : false;
        }
    }
}