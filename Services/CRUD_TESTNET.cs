namespace ROBOT.Services
{
    public class CRUD_TESTNET : IIntegrationService
    {
        private static HttpClient _httpClient = new HttpClient();
        public CRUD_TESTNET()
        {
            _httpClient.BaseAddress = new Uri("https://testnet.binance.vision");
            _httpClient.Timeout = new TimeSpan(0, 0, 5);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task Run()
        {
            await CheckConnection();
            await GetCoinPrice();
            await GetOrderBook();
            await GetTradeList();

        }

        private async Task CheckConnection()
        {
            var response = await _httpClient.GetAsync("/api/v3/ping");
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"Connection status: {response.StatusCode}");
        }

        private async Task GetCoinPrice()
        {
            var response = await _httpClient.GetAsync("/api/v3/avgPrice?symbol=BNBUSDT");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"BNB price: {content.ToString()}");
        }
        private async Task GetOrderBook()
        {
            var response = await _httpClient.GetAsync("/api/v3/depth?symbol=BNBUSDT");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Order Book Depth: {content.Count()}");
        }
        private async Task GetTradeList()
        {
            var response = await _httpClient.GetAsync("/api/v3/trades?symbol=BNBUSDT");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Trade list: {content.ToString()}");
        }
    }
}
