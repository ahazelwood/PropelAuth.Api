using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class UpdateEmailRequest
    {
        /// <summary>
        /// The user's new email address.
        /// </summary>
        [JsonPropertyName("newEmail")]
        public required string NewEmail { get; set; }

        /// <summary>
        /// Whether the new email address requires confirmation. If true, an email will be sent to the new email address to confirm. 
        /// If false, the users email address will be updated and confirmed immediately.
        /// </summary>
        [JsonPropertyName("requireEmailConfirmation")]
        public bool RequireEmailConfirmation { get; set; } = false;
    }


}