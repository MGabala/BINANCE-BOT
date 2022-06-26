namespace ROBOT.Services
{
    internal class CRUD_MAINNET : IIntegrationService
    {
        private static HttpClient _httpClient = new HttpClient();
        public CRUD_MAINNET()
        {
            _httpClient.BaseAddress = new Uri("");
            _httpClient.Timeout = new TimeSpan(0, 0, 5);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task Run()
        {
           
        }
    }
}
