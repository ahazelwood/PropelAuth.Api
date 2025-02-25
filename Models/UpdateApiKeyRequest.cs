using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class UpdateApiKeyRequest
    {
        /// <summary>
        /// A unix timestamp of when the API key should expire. If not provided, the API key will not expire.
        /// </summary>
        [JsonPropertyName("expires_at_seconds")]
        protected long? ExpiresAtSeconds => ExpiresAt.HasValue ? (long)ExpiresAt.Value.Subtract(DateTime.UnixEpoch).TotalSeconds : null;

        /// <summary>
        /// A <see cref="DateTime"/> instance of when the API key should expire.  If not provided, the API key will not expire.
        /// </summary>
        [JsonIgnore]
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Metadata to attach to the API key. This will be returned on validation.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; set; } = [];
    }
}