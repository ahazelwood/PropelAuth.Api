using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class CreateAccessTokenRequest
    {
        /// <summary>
        /// The user's Id.
        /// </summary>
        [JsonPropertyName("userId")]
        public required string UserId { get; set; }

        /// <summary>
        /// How long the token should be valid for.
        /// </summary>
        [JsonPropertyName("durationInMinutes")]
        public required int DurationInMinutes { get; set; }
    }
}