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
                for(; ; )
            {
                Console.WriteLine("");
                Console.WriteLine("Welcome in ROBOT - best cryptocurrencies bot for trading and earning money...");
                Console.WriteLine("1: Check connection state");
                Console.WriteLine("2: Check server time");
                Console.WriteLine("3: Average price");
                Console.WriteLine("4: Get order book");
                Console.WriteLine("5: Get trade list");
                Console.WriteLine("6: Get exchange information");
                Console.WriteLine("7: Get recent trades list");
                Console.Write("Pick number to get method: ");
                int input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        await GETCheckConnectionState();
                        break;
                    case 2:
                        await GETCheckServerTime();
                        break;
                    case 3:
                        await GETCoinPrice();
                        break;
                    case 4:
                        await GETOrderBook();
                        break;
                    case 5:
                        await GETTradeList();
                        break;
                    case 6:
                        await GETExchangeInformation();
                        break;
                    case 7:
                        await GETRecentTradesList();
                        break;
                    default:
                        break;
                }
            }
        }
        private async Task GETRecentTradesList()
        {
            Console.Write("Choose pair: ");
            var pair = Console.ReadLine();
            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/trades?symbol={pair}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{content.ToString()}");
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }
        private async Task GETExchangeInformation()
        {
            Console.Write("Choose pair: ");
            var pair = Console.ReadLine();
            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/exchangeInfo?symbol={pair}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{content.ToString()}");
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }
        private async Task GETCheckServerTime()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/v3/time");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{content.ToString()}");
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

        private async Task GETCheckConnectionState()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/v3/ping");
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"Connection status: {response.StatusCode}");
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

        private async Task GETCoinPrice()
        {
            Console.Write("Choose pair: ");
            var pair = Console.ReadLine();

            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/avgPrice?symbol={pair}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Average price for {pair}:  {content.ToString()}");
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }
        private async Task GETOrderBook()
        {
            Console.Write("Choose pair: ");
            var pair = Console.ReadLine();
            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/depth?symbol={pair}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Order Book Depth: {content.Count()}");
                Console.WriteLine(content.ToString());
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }
        private async Task GETTradeList()
        {
            Console.Write("Choose pair: ");
            var pair = Console.ReadLine();
            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/trades?symbol={pair}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Trade list: {content.ToString()}");
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
