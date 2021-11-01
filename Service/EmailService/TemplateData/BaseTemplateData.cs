using Newtonsoft.Json;

namespace Service.EmailService
{
    public class BaseTemplateData
    {
        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Weblink")]
        public string Weblink { get; set; }
    }
}
