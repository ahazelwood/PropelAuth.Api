using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class CreateUserRequest
    {
        /// <summary>
        /// The user's email address
        /// </summary>
        [JsonPropertyName("email")]
        public required string Email { get; set; }

        /// <summary>
        /// Whether the user's email address has been confirmed. Default is false.
        /// </summary>
        [JsonPropertyName("emailConfirmed")]
        public bool EmailConfirmed { get; set; } = false;

        /// <summary>
        /// Whether to send an email to confirm the email address. Default is false.
        /// </summary>
        [JsonPropertyName("sendEmailToConfirmEmailAddress")]
        public bool SendEmailToConfirmEmailAddress { get; set; } = false;

        /// <summary>
        /// The user's password. If you provide this, make sure to send it to the user so they can log in.
        /// </summary>
        [JsonPropertyName("password")]
        public string? Password { get; set; }

        /// <summary>
        /// If true, the user will be prompted to set/update their password when they next log in. Default is false.
        /// </summary>
        [JsonPropertyName("askUserToUpdatePasswordOnLogin")]
        public bool AskUserToUpdatePasswordOnLogin { get; set; } = false;

        /// <summary>
        /// The user's username
        /// </summary>
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        /// <summary>
        /// The user's first name
        /// </summary>
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        /// <summary>
        /// The user's last name
        /// </summary>
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        /// <summary>
        /// A dictionary of predefined custom properties for the user.
        /// </summary>
        [JsonPropertyName("properties")]
        public Dictionary<string, object?> Properties { get; set; } = [];
    }
}