using Newtonsoft.Json;

namespace EldenRingMVC.Models
{
    /// <summary>
    /// Model for deserializing data from the external Elden Ring API
    /// https://eldenring.fanapis.com/api/bosses
    /// </summary>
    public class ApiBoss
    {
        [JsonProperty("id")]
        public string? ApiId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("location")]
        public string Location { get; set; } = string.Empty;

        [JsonProperty("image")]
        public string Image { get; set; } = string.Empty;

        [JsonProperty("drops")]
        public List<string> Drops { get; set; } = new();
    }

    public class ApiBossResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("data")]
        public List<ApiBoss> Data { get; set; } = new();
    }

    /// <summary>
    /// Model for a random quote from an external quotes API,
    /// used on the home page as thematic flavor text.
    /// </summary>
    public class ApiQuote
    {
        [JsonProperty("content")]
        public string Content { get; set; } = string.Empty;

        [JsonProperty("author")]
        public string Author { get; set; } = string.Empty;
    }
}
