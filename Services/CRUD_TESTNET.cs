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
            _httpClient.DefaultRequestHeaders.Add("X-MBX-APIKEY", Environment.GetEnvironmentVariable("APIKEY"));
            _httpClient.DefaultRequestHeaders.Add("SecretKey", Environment.GetEnvironmentVariable("SECRETKEY"));
            
        }
        public async Task Run()
        {
                for(; ; )
            {
                Console.WriteLine("\n\nWelcome in ROBOT - best cryptocurrencies bot for trading and earning money...");
                Console.WriteLine("1: Test Connectivity");
                Console.WriteLine("2: Check server time");
                Console.WriteLine("3: Exchange Information");
                Console.WriteLine("4: Order Book");
                Console.WriteLine("5: Recent Trades List");
                Console.WriteLine("6: Old Trade Lookup (MARKET_DATA)");
                Console.WriteLine("7: Current Average Price");
                Console.Write("\nPick number to get method: ");
                int input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        await GETTestConnectivity();
                        break;
                    case 2:
                        await GETCheckServerTime();
                        break;
                    case 3:
                        await GETExchangeInformation();
                        break;
                    case 4:
                        await GETOrderBook();
                        break;
                    case 5:
                        await GETRecentTradeList();
                        break;
                    case 6:
                        await GETOldTradeLookup();
                        break;
                    case 7:
                        await GETCurrentAveragePrice();
                        break;

                    default:
                        Console.WriteLine($"\nSorry, {input} not supported yet. Please choose another number.");
                        break;
                }
                Console.WriteLine("\n\nPress enter to clean window");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private async Task GETTestConnectivity()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/v3/ping");
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"\nConnection status: {response.StatusCode}");
               
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request..");
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
                Console.WriteLine($"\n{content.ToString()}");
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request..");
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
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request..");
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
                Console.WriteLine($"\nOrder Book Depth: {content.Count()}");
                Console.WriteLine(content.ToString());
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request..");
                Console.WriteLine(exception.Message);
            }

        }
        private async Task GETRecentTradeList()
        {
            Console.Write("Choose pair: ");
            var pair = Console.ReadLine();
            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/trades?symbol={pair}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nTrade list: {content.ToString()}");
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request..");
                Console.WriteLine(exception.Message);
            }
        }
        private async Task GETOldTradeLookup()
        {
            Console.Write("Choose pair: ");
            var pair = Console.ReadLine();
            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/historicalTrades?symbol={pair}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\n{content.ToString()}");
            }
            catch(Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request..");
                Console.WriteLine(exception.Message);
            }

        }
        private async Task GETCurrentAveragePrice()
        {
            Console.Write("Choose pair: ");
            var pair = Console.ReadLine();

            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/avgPrice?symbol={pair}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nAverage price for {pair}:  {content.ToString()}");
            }
            catch (Exception exception)
            {
                Console.WriteLine("\n   Sorry, cannot proceed your request..");
                Console.WriteLine(exception.Message);
            }

        }

    }
}
