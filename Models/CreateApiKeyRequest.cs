using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class CreateApiKeyRequest
    {
        /// <summary>
        /// The ID of the organization to associate the API key with. If not provided, the API key will not be associated with an organization.
        /// </summary>
        [JsonPropertyName("org_id")]
        public string? OrgId { get; set; }

        /// <summary>
        /// The ID of the user to associate the API key with. If not provided, the API key will not be associated with a user.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// A unix timestamp of when the API key should expire. If not provided, the API key will not expire.
        /// </summary>
        [JsonPropertyName("expires_at_seconds")]
        protected long? ExpiresAtSeconds {
            get {
                if (!ExpiresAt.HasValue)
                    return null;

                var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return (long)ExpiresAt.Value.Subtract(unixEpoch).TotalSeconds;
            }
        }

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