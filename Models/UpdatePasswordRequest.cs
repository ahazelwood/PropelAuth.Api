using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class UpdatePasswordRequest
    {
        /// <summary>
        /// The user's new password.
        /// </summary>
        [JsonPropertyName("password")]
        public required string Password { get; set; }

        /// <summary>
        /// Whether to ask the user to update their password on their next login.
        /// </summary>
        [JsonPropertyName("askUserToUpdatePasswordOnLogin")]
        public bool AskUserToUpdatePasswordOnLogin { get; set; } = false;
    }


}