using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class PropelAuthApiKey 
    {
        /// <summary>
        /// The API Key internal ID.  This is not the API Key Token.
        /// </summary>
        [JsonPropertyName("api_key_id")]
        public string ApiKeyId { get; set; } = null!;

        /// <summary>
        /// If specified, this is a API Key associated with the specified User.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// If specified, this is a Organization API Key.
        /// </summary>
        [JsonPropertyName("org_id")]
        public string? OrgId { get; set; }

        /// <summary>
        /// Timestamp that represents when the API Key was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// A <see cref="DateTime"> representation of the CreatedAt property.
        /// </summary>
        [JsonIgnore]
        public DateTime CreatedAtDate => DateTime.UnixEpoch.AddSeconds(CreatedAt);

        /// <summary>
        /// Timesamp that represents when the API Key will expire.
        /// </summary>
        [JsonPropertyName("expires_at_seconds")]
        public long? ExpiresAtSeconds { get; set; }

        /// <summary>
        /// A <see cref="DateTime"> representation of the ExpiresAtSeconds property.
        /// </summary>
        [JsonIgnore]
        public DateTime? ExpiresAtSecondsDate => ExpiresAtSeconds.HasValue ? DateTime.UnixEpoch.AddSeconds(ExpiresAtSeconds.Value) : null;

        /// <summary>
        /// A list of Metadata to associate with the API Key.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; set; } = [];
    }
}