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
            Console.WriteLine("1: Check connection state");
            Console.WriteLine("2: Check server time");

            var pick = Console.ReadLine();
            var input = Convert.ToInt32(pick);
          
            //await GETCoinPrice(pair!);
            //await GETOrderBook(pair!);
            //await GETTradeList(pair!);
            //await GETExchangeInformation(pair!);
            //await GETRecentTradesList(pair!);
            //Now Old Trade Lookup
            switch (input)
            {
                case 1:
                    await GETCheckConnectionState();
                    break;
                case 2:
                    await GETCheckServerTime();
                    break;
                default:
                    Console.WriteLine("Wybierz wartość");
                    break;

            }
        }
        private async Task GETRecentTradesList(string pair)
        {
            var response = await _httpClient.GetAsync($"/api/v3/trades?symbol={pair}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{content.ToString()}");
        }
        private async Task GETExchangeInformation(string pair)
        {
            var response = await _httpClient.GetAsync($"/api/v3/exchangeInfo?symbol={pair}");
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

        private async Task GETCoinPrice(string pair)
        {
            var response = await _httpClient.GetAsync($"/api/v3/avgPrice?symbol={pair}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"BNB price: {content.ToString()}");
        }
        private async Task GETOrderBook(string pair)
        {
            var response = await _httpClient.GetAsync($"/api/v3/depth?symbol={pair}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Order Book Depth: {content.Count()}");
            Console.WriteLine(content.ToString()) ;
        }
        private async Task GETTradeList(string pair)
        {
            var response = await _httpClient.GetAsync($"/api/v3/trades?symbol={pair}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Trade list: {content.ToString()}");
        }
    }
}
