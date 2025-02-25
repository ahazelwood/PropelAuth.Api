using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class ValidateApiKeyRequest 
    {
        [JsonPropertyName("api_key_token")]
        public required string ApiKey { get; set; }
    }
}