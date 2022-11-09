using System.Net.Http;
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
        #region Headquarters
        public async Task Run()
        {
            string? signature = "c08b5393cfd1f749e4d669c760437da8084746da81e3a5b0fe6618b8fbe45b68";
            //string? signature = Environment.GetEnvironmentVariable("SIGNATURE");
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
                Console.WriteLine("11: Cancel Order (TRADE)");
                Console.WriteLine("12: Current Open Orders (USER_DATA)");
                Console.WriteLine("13: All Orders (USER_DATA)");
                Console.WriteLine("14: Account Information (USER_DATA)");
                Console.WriteLine("15: Account Trade List (USER_DATA)");
                Console.WriteLine("0: POST TEST ORDER");
                Console.Write("\nPick number to get method: ");
                int input = System.Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    //Market section
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
                    //Trade section
                    case 0:
                        await POSTNewTstOrder(signature!, timestamp!);
                        break;
                    case 10:
                        await POSTNewOrder(signature!, timestamp!);
                        break;
                    case 11:
                        await DELCancelOrder(signature!, timestamp!);
                        break;
                    case 12:
                        await GETCurrentOpenOwnOrders(timestamp!, signature!);
                        break;
                    case 13:
                        await GETAllOwnOrders(timestamp!, signature!);
                        break;
                    case 14:
                        await GETAccountInformation(timestamp!, signature!);
                        break;
                    case 15: 
                        await GETAccountTradeList(timestamp!, signature!);
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
        #endregion

        #region Market
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
            Console.Write("Choose symbol: ");
            var symbol = Console.ReadLine();
            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/exchangeInfo?symbol={symbol}");
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
            Console.Write("Choose symbol: ");
            var symbol = Console.ReadLine();
            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/depth?symbol={symbol}");
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
            Console.Write("Choose symbol: ");
            var symbol = Console.ReadLine();
            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/trades?symbol={symbol}");
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
            Console.Write("Choose symbol: ");
            var symbol = Console.ReadLine();
            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/historicalTrades?symbol={symbol}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\n{content.ToString()}");
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");

            }

        }
        private async Task GETCurrentAveragePrice()
        {
            Console.Write("Choose symbol: ");
            var symbol = Console.ReadLine();

            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/avgPrice?symbol={symbol}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nAverage price for {symbol}:  {content.ToString()}");
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");

            }

        }
        private async Task GETDailyHoursChangeStat()
        {
            Console.WriteLine("-- Leave empty to get whole list [ Press just enter ] --");
            Console.Write("Choose symbol: ");
            string? symbol = Console.ReadLine();
            if (symbol != String.Empty)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"/api/v3/ticker/24hr?symbol={symbol}");
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
            Console.Write("Choose symbol: ");
            string? symbol = Console.ReadLine();
            if (symbol != String.Empty)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"/api/v3/ticker/24hr?symbol={symbol}");
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
        #endregion

        #region Trade
        private async Task POSTNewTstOrder(string signature, long timestamp)
        {
            // EXAMPLE: XRPBUSD
            Console.Write("Choose symbol: "); string? symbol = Console.ReadLine();
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
            string? query = $"symbol={symbol}&side={side}&type={type}&timeInForce={timeInForce}&quantity={quantity}&price={price}&timestamp={timestamp}&signature={signature}";
            var POSTDATA = new Dictionary<object, object>()
                {
                    {"symbol",symbol},
                    {"side",side},
                    {"type",type},
                    {"timeInForce",timeInForce},
                    {"quantity",quantity},
                    {"price",price},
                    {"timestamp", timestamp},
                    {"signature",signature}
                    };
            var jsonContent = JsonConvert.SerializeObject(POSTDATA);
            var stringContent = new StringContent(jsonContent);
            try
            {

                var response = await _httpClient.PostAsync("/api/v3/order/test?", stringContent);
                //response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content.ToString());

            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message + Environment.NewLine + exception.InnerException}");

            }

        }
        private async Task POSTNewOrder(string signature, long timestamp)
        {
            // EXAMPLE: XRPBUSD
            Console.Write("Choose symbol: "); string? symbol = Console.ReadLine();
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
            string? query = $"symbol={symbol}&side={side}&type={type}&timeInForce={timeInForce}&quantity={quantity}&price={price}&timestamp={timestamp}&signature={signature}";
            var POSTDATA = new Dictionary<object, object>()
                {
                    {"symbol",symbol},
                    {"side",side},
                    {"type",type},
                    {"timeInForce",timeInForce},
                    {"quantity",quantity},
                    {"price",price},
                    {"timestamp", timestamp},
                    {"signature",signature}
                    };
            var jsonContent = JsonConvert.SerializeObject(POSTDATA);
            var stringContent = new StringContent(jsonContent);
            try
            {

                var response = await _httpClient.PostAsync("/api/v3/order/test", stringContent);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content.ToString());

            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message +Environment.NewLine+ exception.InnerException}");

            }

        }
        private async Task DELCancelOrder(string signature, long timestamp)
        {
            // EXAMPLE: XRPBUSD
            Console.Write("Choose symbol: "); string? symbol = Console.ReadLine();
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
            string? query = $"symbol={symbol}&side={side}&type={type}&timeInForce={timeInForce}&quantity={quantity}&price={price}&timestamp={timestamp}&signature={signature}";
            var POSTDATA = new Dictionary<object, object>()
                {
                    {"symbol",symbol},
                    {"side",side},
                    {"type",type},
                    {"timeInForce",timeInForce},
                    {"quantity",quantity},
                    {"price",price},
                    {"timestamp", timestamp},
                    {"signature",signature}
                    };
            var jsonContent = JsonConvert.SerializeObject(POSTDATA);
            var stringContent = new StringContent(jsonContent);
            try
            {

                var response = await _httpClient.PostAsync("/api/v3/order/test", stringContent);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content.ToString());

            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message + Environment.NewLine + exception.InnerException}");

            }

        }
        private async Task GETCurrentOpenOwnOrders(long timestamp, string signature)
        {
           
            try
            {
                var response = await _httpClient.GetAsync($"/api/v3/openOrders?timestamp={timestamp}&signature={signature}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nCurrent Open Orders {content}");
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");

            }

        }
        private async Task GETAllOwnOrders(long timestamp, string signature)
        {

            try
            {

                var response = await _httpClient.GetAsync($"/api/v3/allOrders?symbol=BNBUSDT&timestamp={timestamp}&signature={signature}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nCurrent Open Orders {content}");
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");

            }

        }
        private async Task GETAccountInformation(long timestamp, string signature)
        {

            try
            {

                var response = await _httpClient.GetAsync($"/api/v3/allOrders?symbol=BNBUSDT&timestamp={timestamp}&signature={signature}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nCurrent Open Orders {content}");
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");

            }

        }
        private async Task GETAccountTradeList(long timestamp, string signature)
        {

            try
            {

                var response = await _httpClient.GetAsync($"/api/v3/allOrders?symbol=BNBUSDT&timestamp={timestamp}&signature={signature}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nCurrent Open Orders {content}");
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");

            }

        }

        #endregion
    }
}
