using System.Net.Http.Json;

using Newtonsoft.Json;

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
            string? signature = Environment.GetEnvironmentVariable("SIGNATURE");
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            for (; ; )
            {
                Console.WriteLine("\n\nWelcome in ROBOT - best cryptocurrencies bot for trading and earning money...");
                Console.WriteLine("1: Test Connectivity");
                Console.WriteLine("2: Check server time");
                Console.WriteLine("3: Exchange Information");
                Console.WriteLine("4: Order Book");
                Console.WriteLine("5: Recent Trades List");
                Console.WriteLine("6: Old Trade Lookup (MARKET_DATA)");
                Console.WriteLine("7: Current Average Price");
                Console.WriteLine("8: 24hr Ticker Price Change Statistics");
                Console.WriteLine("9: Symbol Price Ticker");
                Console.WriteLine("10: New Order (TRADE)");
                Console.Write("\nPick number to get method: ");
                int input = System.Convert.ToInt32(Console.ReadLine());

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
                    case 8:
                        await GETDailyHoursChangeStat();
                        break;
                    case 9:
                        await GETSymbolPriceTracker();
                        break;
                    case 10:
                        await POSTNewOrder(signature!, timestamp!);
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
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
                 
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
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
                 
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
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
                 
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
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
                 
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
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
                 
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
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
                 
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
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");

            }

        }
        private async Task GETDailyHoursChangeStat()
        {
            Console.WriteLine("-- Leave empty to get whole list [ Press just enter ] --");
            Console.Write("Choose pair: ");
            string? pair = Console.ReadLine();
            if(pair != String.Empty)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"/api/v3/ticker/24hr?symbol={pair}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"{content.ToString()}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
                     
                }
            }
            else
            {
                try
                {
                    var response = await _httpClient.GetAsync($"/api/v3/ticker/24hr");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"{content.ToString()}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
                     
                }
            }

        }
        private async Task GETSymbolPriceTracker()
        {
            Console.WriteLine("-- Leave empty to get whole list [ Press just enter ] --");
            Console.Write("Choose pair: ");
            string? pair = Console.ReadLine();
            if (pair != String.Empty)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"/api/v3/ticker/24hr?symbol={pair}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"{content.ToString()}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
                     
                }
            }
            else
            {
                try
                {
                    var response = await _httpClient.GetAsync($"/api/v3/ticker/24hr");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"{content.ToString()}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
                     
                }
            }

        }
        private async Task POSTNewOrder(string signature, long timestamp)
        {
            // EXAMPLE: XRPBUSD
            Console.Write("Choose pair: "); string? pair = Console.ReadLine();
            //EXAMPLE: BUY / SELL
            Console.Write("Side: "); string? side = Console.ReadLine();
            //EXAMPLE: LIMIT / MARKET / STOP_LOSS / STOP_LOSS_LIMIT / TAKE_PROFIT / TAKE_PROFIT_LIMIT / LIMIT_MAKER
            Console.Write("Type: "); string? type = Console.ReadLine();
            //EXAMPLE: GTC (good till canceled) / FOK (fill or kill) / IOC (immediate or cancel)
            Console.Write("TimeInForce: "); string? timeInForce = Console.ReadLine();
            //EXAMPLE: Quantity: 100
            Console.Write("Quantity: "); string? quantity = Console.ReadLine();
            //EXAMPLE: Price: 350
            Console.Write("Price: "); string? price = Console.ReadLine();
            string? query = $"symbol={pair}&side={side}&type={type}&timeInForce={timeInForce}&quantity={quantity}&price={price}&timestamp={timestamp}&signature={signature}";

            try
            {
                var response = await _httpClient.PostAsync("/api/v3/order?", query);
                //response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;
                var xd = response.RequestMessage;
                
                Console.WriteLine($"{content.ToString()}");
                Console.WriteLine($"{statusCode.ToString()}");
                Console.WriteLine($"{xd.ToString()}");
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");

            }

        }

    }
}
