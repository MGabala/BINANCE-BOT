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
            Console.WriteLine("Welcome in ROBOT - best cryptocurrencies bot for trading and earning money...");
            Console.Write("Which pair you are interested in? ");
            var pair = Console.ReadLine();
            //await GETCheckConnectionState();
            //await GETCoinPrice();
            //await GETOrderBook();
            //await GETTradeList();
            //await GETCheckServerTime();
            //await GETExchangeInformation();
            await GETRecentTradesList(pair);
            //Now Old Trade Lookup
        }
        private async Task GETRecentTradesList(string pair)
        {
            var response = await _httpClient.GetAsync($"/api/v3/trades?symbol={pair}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{content.ToString()}");
        }
        private async Task GETExchangeInformation()
        {
            var response = await _httpClient.GetAsync("/api/v3/exchangeInfo?symbol=BNBUSDT");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{content.ToString()}");
        }
        private async Task GETCheckServerTime()
        {
            var response = await _httpClient.GetAsync("/api/v3/time");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{content.ToString()}");
        }

        private async Task GETCheckConnectionState()
        {
            var response = await _httpClient.GetAsync("/api/v3/ping");
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"Connection status: {response.StatusCode}");
        }

        private async Task GETCoinPrice()
        {
            var response = await _httpClient.GetAsync("/api/v3/avgPrice?symbol=BNBUSDT");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"BNB price: {content.ToString()}");
        }
        private async Task GETOrderBook()
        {
            var response = await _httpClient.GetAsync("/api/v3/depth?symbol=BNBUSDT");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Order Book Depth: {content.Count()}");
            Console.WriteLine(content.ToString()) ;
        }
        private async Task GETTradeList()
        {
            var response = await _httpClient.GetAsync("/api/v3/trades?symbol=BNBUSDT");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Trade list: {content.ToString()}");
        }
    }
}
