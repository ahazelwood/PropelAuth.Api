using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class FindApiKeyResponse {
        [JsonPropertyName("api_keys")]
        public List<PropelAuthApiKey> ApiKeys { get; set; } = [];

        [JsonPropertyName("total_api_keys")]
        public int TotalApiKeys { get; set; }

        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }

        [JsonPropertyName("has_more_results")]
        public bool HasMoreResults { get; set; }
    }
}