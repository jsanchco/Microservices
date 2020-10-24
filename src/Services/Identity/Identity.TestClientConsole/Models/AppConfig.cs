namespace Identity.TestClientConsole.Models
{
    public class AppConfig
    {
        public Identity Identity { get; set; }
        public API API { get; set; }
    }

    public class Identity
    {
        public string url { get; set; }
        public string clientId { get; set; }
        public string secret { get; set; }
        public string scope { get; set; }
    }

    public class API
    {
        public string url { get; set; }
    }
}
