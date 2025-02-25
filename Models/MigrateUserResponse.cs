using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class MigrateUserResponse 
    {
        /// <summary>
        /// The newly created user ID.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = null!;
    }
}