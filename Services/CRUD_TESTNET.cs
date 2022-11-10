using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;

using Binance.Common;
using Binance.Spot;
using Binance.Spot.Models;

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
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
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
                    case 0:
                        await POSTNewTstOrder();
                        break;
                    case 10:
                        await POSTNewOrder();
                        break;
                    case 11:
                        await DELCancelOrder();
                        break;
                    case 12:
                        await GETCurrentOpenOwnOrders();
                        break;
                    case 13:
                        await GETAllOwnOrders();
                        break;
                    case 14:
                        await GETAccountInformation();
                        break;
                    case 15:
                        await GETAccountTradeList();
                        break;
                    default:
                        Console.WriteLine("Something went wrong, try again.");
                        break;
                }


                Console.WriteLine("\n\nPress enter to clean window");
                
                Console.ReadKey();
                Console.Clear();
                
            }
        }

        #region Market
        private async Task GETTestConnectivity()
        {
            var market = new Market(_httpClient);
            var result = await market.TestConnectivity();
            Console.WriteLine(result);
            #region FromScratch
            //try
            //{
            //    var response = await _httpClient.GetAsync("/api/v3/ping");
            //    response.EnsureSuccessStatusCode();
            //    Console.WriteLine($"\nConnection status: {response.StatusCode}");
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
            //}
            #endregion
        }
        private async Task GETCheckServerTime()
        {
            var market = new Market(_httpClient);
            var result = await market.CheckServerTime();
            Console.WriteLine(result.ToString());
            #region FromScratch
            //try
            //{
            //    var response = await _httpClient.GetAsync("/api/v3/time");
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine($"\n{content.ToString()}");
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
            //}
            #endregion
        }
        private async Task GETExchangeInformation()
        {
            Console.Write("Choose symbol: ");
            var symbol = Console.ReadLine();
            var market = new Market(_httpClient);
            var result = await market.ExchangeInformation(symbol);
            Console.WriteLine(JsonConvert.DeserializeObject(result));
            #region FromScratch
            //try
            //{
            //    var response = await _httpClient.GetAsync($"/api/v3/exchangeInfo?symbol={symbol}");
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine($"{content.ToString()}");
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
            //}
            #endregion
        }
        private async Task GETOrderBook()
        {
            Console.Write("Choose symbol: ");
            var symbol = Console.ReadLine();
            var market = new Market(_httpClient);
            var result = await market.OrderBook(symbol);
            Console.WriteLine(JsonConvert.DeserializeObject(result));

            #region FromScratch
            //Console.Write("Choose symbol: ");
            //var symbol = Console.ReadLine();
            //try
            //{
            //    var response = await _httpClient.GetAsync($"/api/v3/depth?symbol={symbol}");
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine($"\nOrder Book Depth: {content.Count()}");
            //    Console.WriteLine(content.ToString());
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
            //}
            #endregion
        }
        private async Task GETRecentTradeList()
        {
            Console.Write("Choose symbol: ");
            var symbol = Console.ReadLine();
            var market = new Market(_httpClient);
            var result = await market.RecentTradesList(symbol);
            Console.WriteLine(JsonConvert.DeserializeObject(result));
            #region FromScratch
            //Console.Write("Choose symbol: ");
            //var symbol = Console.ReadLine();
            //try
            //{
            //    var response = await _httpClient.GetAsync($"/api/v3/trades?symbol={symbol}");
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine($"\nTrade list: {content.ToString()}");
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
            //}
            #endregion
        }
        private async Task GETOldTradeLookup()
        {
            var market = new Market(_httpClient, "https://testnet.binance.vision", apiKey: Environment.GetEnvironmentVariable("APIKEY"), apiSecret: Environment.GetEnvironmentVariable("SECRETKEY"));

            var result = await market.OldTradeLookup("BNBUSDT");

            #region FromScratch
            //Console.Write("Choose symbol: ");
            //var symbol = Console.ReadLine();
            //try
            //{
            //    var response = await _httpClient.GetAsync($"/api/v3/historicalTrades?symbol={symbol}");
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine($"\n{content.ToString()}");
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
            //}
            #endregion

        }
        private async Task GETCurrentAveragePrice()
        {
            Console.Write("Choose symbol: ");
            var symbol = Console.ReadLine();
            var market = new Market(_httpClient);
            var result = await market.CurrentAveragePrice(symbol);
            Console.WriteLine(JsonConvert.DeserializeObject(result));

            #region FromScratch
            //try
            //{
            //    var response = await _httpClient.GetAsync($"/api/v3/avgPrice?symbol={symbol}");
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine($"\nAverage price for {symbol}:  {content.ToString()}");
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
            //}
            #endregion
        }
        private async Task GETDailyHoursChangeStat()
        {
            Console.Write("Choose symbol: ");
            var symbol = Console.ReadLine();
            var market = new Market(_httpClient);
            var result = await market.TwentyFourHrTickerPriceChangeStatistics(symbol);
            Console.WriteLine(JsonConvert.DeserializeObject(result));
            #region FromScratch
            //Console.WriteLine("-- Leave empty to get whole list [ Press just enter ] --");
            //Console.Write("Choose symbol: ");
            //string? symbol = Console.ReadLine();
            //if (symbol != String.Empty)
            //{
            //    try
            //    {
            //        var response = await _httpClient.GetAsync($"/api/v3/ticker/24hr?symbol={symbol}");
            //        response.EnsureSuccessStatusCode();
            //        var content = await response.Content.ReadAsStringAsync();
            //        Console.WriteLine($"{content.ToString()}");
            //    }
            //    catch (Exception exception)
            //    {
            //        Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");

            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        var response = await _httpClient.GetAsync($"/api/v3/ticker/24hr");
            //        response.EnsureSuccessStatusCode();
            //        var content = await response.Content.ReadAsStringAsync();
            //        Console.WriteLine($"{content.ToString()}");
            //    }
            //    catch (Exception exception)
            //    {
            //        Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");

            //    }
            //}
            #endregion

        }
        private async Task GETSymbolPriceTracker()
        {
            Console.Write("Choose symbol: ");
            var symbol = Console.ReadLine();
            var market = new Market(_httpClient);
            var result = await market.SymbolPriceTicker(symbol);
            Console.WriteLine(JsonConvert.DeserializeObject(result));
            #region FromScratch
            //Console.WriteLine("-- Leave empty to get whole list [ Press just enter ] --");
            //Console.Write("Choose symbol: ");
            //string? symbol = Console.ReadLine();
            //if (symbol != String.Empty)
            //{
            //    try
            //    {
            //        var response = await _httpClient.GetAsync($"/api/v3/ticker/24hr?symbol={symbol}");
            //        response.EnsureSuccessStatusCode();
            //        var content = await response.Content.ReadAsStringAsync();
            //        Console.WriteLine($"{content.ToString()}");
            //    }
            //    catch (Exception exception)
            //    {
            //        Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");

            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        var response = await _httpClient.GetAsync($"/api/v3/ticker/24hr");
            //        response.EnsureSuccessStatusCode();
            //        var content = await response.Content.ReadAsStringAsync();
            //        Console.WriteLine($"{content.ToString()}");
            //    }
            //    catch (Exception exception)
            //    {
            //        Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
            //    }
            //}
            #endregion

        }
        #endregion

        #region Trade
        private async Task POSTNewTstOrder()
        {
            var spotAccountTrade = new SpotAccountTrade(_httpClient,
                "https://testnet.binance.vision", apiKey: Environment.GetEnvironmentVariable("APIKEY"), apiSecret: Environment.GetEnvironmentVariable("SECRETKEY"));
            var result = await spotAccountTrade.TestNewOrder("XRPBUSD", Side.BUY, OrderType.MARKET, quantity: 35);
            Console.WriteLine(result);

                #region FromScratch
                //// EXAMPLE: XRPBUSD
                //Console.Write("Choose symbol: "); string? symbol = Console.ReadLine();
                ////EXAMPLE: BUY / SELL
                //Console.Write("Side: "); string? side = Console.ReadLine();
                ////EXAMPLE: LIMIT / MARKET / STOP_LOSS / STOP_LOSS_LIMIT / TAKE_PROFIT / TAKE_PROFIT_LIMIT / LIMIT_MAKER
                //Console.Write("Type: "); string? type = Console.ReadLine();
                ////EXAMPLE: GTC (good till canceled) / FOK (fill or kill) / IOC (immediate or cancel)
                //Console.Write("TimeInForce: "); string? timeInForce = Console.ReadLine();
                ////EXAMPLE: Quantity: 100
                //Console.Write("Quantity: "); string? quantity = Console.ReadLine();
                ////EXAMPLE: Price: 350
                //Console.Write("Price: "); string? price = Console.ReadLine();
                //string? query = $"symbol={symbol}&side={side}&type={type}&timeInForce={timeInForce}&quantity={quantity}&price={price}&timestamp={timestamp}&signature={signature}";
                //var POSTDATA = new Dictionary<object, object>()
                //    {
                //        {"symbol",symbol},
                //        {"side",side},
                //        {"type",type},
                //        {"timeInForce",timeInForce},
                //        {"quantity",quantity},
                //        {"price",price},
                //        {"timestamp", timestamp},
                //        {"signature",signature}
                //        };
                //var jsonContent = JsonConvert.SerializeObject(POSTDATA);
                //var stringContent = new StringContent(jsonContent);
                //try
                //{

                //    var response = await _httpClient.PostAsync("/api/v3/order/test?", stringContent);
                //    //response.EnsureSuccessStatusCode();
                //    var content = await response.Content.ReadAsStringAsync();
                //    Console.WriteLine(content.ToString());

                //}
                //catch (Exception exception)
                //{
                //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message + Environment.NewLine + exception.InnerException}");
                //}
                #endregion

            }
        private async Task POSTNewOrder()
        {
             var spotAccountTrade = new SpotAccountTrade(_httpClient,
                "https://testnet.binance.vision", apiKey: Environment.GetEnvironmentVariable("APIKEY"), apiSecret: Environment.GetEnvironmentVariable("SECRETKEY"));
            var result = await spotAccountTrade.NewOrder("XRPBUSD", Side.BUY, OrderType.MARKET, quantity: 35);
            Console.WriteLine(result);

            #region FromScratch
            //// EXAMPLE: XRPBUSD
            //Console.Write("Choose symbol: "); string? symbol = Console.ReadLine();
            ////EXAMPLE: BUY / SELL
            //Console.Write("Side: "); string? side = Console.ReadLine();
            ////EXAMPLE: LIMIT / MARKET / STOP_LOSS / STOP_LOSS_LIMIT / TAKE_PROFIT / TAKE_PROFIT_LIMIT / LIMIT_MAKER
            //Console.Write("Type: "); string? type = Console.ReadLine();
            ////EXAMPLE: GTC (good till canceled) / FOK (fill or kill) / IOC (immediate or cancel)
            //Console.Write("TimeInForce: "); string? timeInForce = Console.ReadLine();
            ////EXAMPLE: Quantity: 100
            //Console.Write("Quantity: "); string? quantity = Console.ReadLine();
            ////EXAMPLE: Price: 350
            //Console.Write("Price: "); string? price = Console.ReadLine();
            //string? query = $"symbol={symbol}&side={side}&type={type}&timeInForce={timeInForce}&quantity={quantity}&price={price}&timestamp={timestamp}&signature={signature}";
            //var POSTDATA = new Dictionary<object, object>()
            //    {
            //        {"symbol",symbol},
            //        {"side",side},
            //        {"type",type},
            //        {"timeInForce",timeInForce},
            //        {"quantity",quantity},
            //        {"price",price},
            //        {"timestamp", timestamp},
            //        {"signature",signature}
            //        };
            //var jsonContent = JsonConvert.SerializeObject(POSTDATA);
            //var stringContent = new StringContent(query);
            //try
            //{

            //    var response = await _httpClient.PostAsync("/api/v3/order?", stringContent);
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(content.ToString());

            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message +Environment.NewLine+ exception.InnerException}");
            //}
            #endregion

        }
        private async Task DELCancelOrder()
        {
            //cannot cancel an order via orderId
            var spotAccountTrade = new SpotAccountTrade(_httpClient,
                    "https://testnet.binance.vision", apiKey: Environment.GetEnvironmentVariable("APIKEY"), apiSecret: Environment.GetEnvironmentVariable("SECRETKEY"));
            var result = await spotAccountTrade.CancelOrder("BNBBUSD", 1491388);

            #region FromScratch
            //// EXAMPLE: XRPBUSD
            //Console.Write("Choose symbol: "); string? symbol = Console.ReadLine();
            ////EXAMPLE: BUY / SELL
            //Console.Write("Side: "); string? side = Console.ReadLine();
            ////EXAMPLE: LIMIT / MARKET / STOP_LOSS / STOP_LOSS_LIMIT / TAKE_PROFIT / TAKE_PROFIT_LIMIT / LIMIT_MAKER
            //Console.Write("Type: "); string? type = Console.ReadLine();
            ////EXAMPLE: GTC (good till canceled) / FOK (fill or kill) / IOC (immediate or cancel)
            //Console.Write("TimeInForce: "); string? timeInForce = Console.ReadLine();
            ////EXAMPLE: Quantity: 100
            //Console.Write("Quantity: "); string? quantity = Console.ReadLine();
            ////EXAMPLE: Price: 350
            //Console.Write("Price: "); string? price = Console.ReadLine();
            //string? query = $"symbol={symbol}&side={side}&type={type}&timeInForce={timeInForce}&quantity={quantity}&price={price}&timestamp={timestamp}&signature={signature}";
            //var POSTDATA = new Dictionary<object, object>()
            //    {
            //        {"symbol",symbol},
            //        {"side",side},
            //        {"type",type},
            //        {"timeInForce",timeInForce},
            //        {"quantity",quantity},
            //        {"price",price},
            //        {"timestamp", timestamp},
            //        {"signature",signature}
            //        };
            //var jsonContent = JsonConvert.SerializeObject(POSTDATA);
            //var stringContent = new StringContent(jsonContent);
            //try
            //{

            //    var response = await _httpClient.PostAsync("/api/v3/order/test", stringContent);
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(content.ToString());

            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message + Environment.NewLine + exception.InnerException}");
            //}
            #endregion

        }
        private async Task GETCurrentOpenOwnOrders()
        {
            var spotAccountTrade = new SpotAccountTrade(_httpClient,
                        "https://testnet.binance.vision", apiKey: Environment.GetEnvironmentVariable("APIKEY"), apiSecret: Environment.GetEnvironmentVariable("SECRETKEY"));
            var result = await spotAccountTrade.CurrentOpenOrders();
            #region FromScratch
            //try
            //{
            //    var response = await _httpClient.GetAsync($"/api/v3/openOrders?timestamp={timestamp}&signature={signature}");
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine($"\nCurrent Open Orders {content}");
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
            //}
            #endregion

        }
        private async Task GETAllOwnOrders()
        {
            var spotAccountTrade = new SpotAccountTrade(_httpClient,
            "https://testnet.binance.vision", apiKey: Environment.GetEnvironmentVariable("APIKEY"), apiSecret: Environment.GetEnvironmentVariable("SECRETKEY"));
            var result = await spotAccountTrade.AllOrders("BNBBUSD");

            #region FromScratch
            //try
            //{

            //    var response = await _httpClient.GetAsync($"/api/v3/allOrders?symbol=BNBUSDT&timestamp={timestamp}&signature={signature}");
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine($"\nCurrent Open Orders {content}");
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
            //}
            #endregion
        }
        private async Task GETAccountInformation()
        {
            var spotAccountTrade = new SpotAccountTrade(_httpClient,
            "https://testnet.binance.vision", apiKey: Environment.GetEnvironmentVariable("APIKEY"), apiSecret: Environment.GetEnvironmentVariable("SECRETKEY"));
            var result = await spotAccountTrade.AccountInformation();
            #region FromScratch
            //try
            //{

            //    var response = await _httpClient.GetAsync($"/api/v3/allOrders?symbol=BNBUSDT&timestamp={timestamp}&signature={signature}");
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine($"\nCurrent Open Orders {content}");
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
            //}
            #endregion

        }
        private async Task GETAccountTradeList()
        {
            var spotAccountTrade = new SpotAccountTrade(_httpClient,
            "https://testnet.binance.vision", apiKey: Environment.GetEnvironmentVariable("APIKEY"), apiSecret: Environment.GetEnvironmentVariable("SECRETKEY"));
            var result = await spotAccountTrade.AccountTradeList("BNBBUSD");
            #region FromScratch
            //try
            //{

            //    var response = await _httpClient.GetAsync($"/api/v3/allOrders?symbol=BNBUSDT&timestamp={timestamp}&signature={signature}");
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine($"\nCurrent Open Orders {content}");
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nSorry, cannot proceed your request.." + $"\n{exception.Message}");
            //}
            #endregion

        }
        #endregion

    }
}
