using System.Text.Json.Serialization;

namespace Api.Function
{
    public class Counter
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;  // Default to an empty string, or you can leave it uninitialized

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
