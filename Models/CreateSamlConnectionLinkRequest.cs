using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class CreateSamlConnectionLinkRequest
    {
        /// <summary>
        /// The amount of seconds before the link expires.
        /// </summary>
        [JsonPropertyName("expires_in_seconds")]
        public int ExpiresInSeconds { get; set; }
    }
}