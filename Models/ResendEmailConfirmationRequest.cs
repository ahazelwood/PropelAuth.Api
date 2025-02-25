using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class ResendEmailConfirmationRequest 
    {
        /// <summary>
        /// The user ID to resend a confirmation request to.
        /// </summary>
        [JsonPropertyName("user_id")]
        public required string UserId { get; set; }
    }
}