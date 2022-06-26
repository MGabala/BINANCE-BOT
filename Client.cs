namespace ROBOT
{
    internal class Client
    {
        public Client(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public HttpClient _httpClient { get; set; }
        
    }
}
