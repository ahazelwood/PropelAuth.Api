using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class MigrateUserRequest
    {
        /// <summary>
        /// The user's email address.
        /// </summary>
        [JsonPropertyName("email")]
        public required string Email { get; set; }

        /// <summary>
        /// Whether the user's email address has been confirmed. Default is false.
        /// </summary>
        [JsonPropertyName("emailConfirmed")]
        public bool EmailConfirmed { get; set; } = false;

        /// <summary>
        /// The user’s ID in the existing system. This ID will be stored on the user as a legacyUserId 
        /// and it is present everywhere userId's are (e.g. fetching user metadata or validating a user's token).
        /// </summary>
        [JsonPropertyName("existingUserId")]
        public string? ExistingUserId { get; set; }

        /// <summary>
        /// The user's hashed password. We support bcrypt, argon2, and pbkdf2_sha256 password hashes. If you use a different hash, please contact us.
        /// </summary>
        [JsonPropertyName("existingPasswordHash")]
        public string? ExistingPasswordHash { get; set; }

        /// <summary>
        /// The user's MFA secret, base32 encoded. If specified the user will have MFA enabled by default.
        /// </summary>
        [JsonPropertyName("existingPasswordHash")]
        public string? ExistingMfaBase32EncodedSecret { get; set; }

        /// <summary>
        /// If true, the user will be prompted to set/update their password when they next log in. Default is false.
        /// </summary>
        [JsonPropertyName("askUserToUpdatePasswordOnLogin")]
        public bool AskUserToUpdatePasswordOnLogin { get; set; } = false;

        /// <summary>
        /// Whether the user is enabled. Default is true.
        /// </summary>
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// The user's username (optional).
        /// </summary>
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        /// <summary>
        /// The user's first name.
        /// </summary>
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        /// <summary>
        /// The user's last name.
        /// </summary>
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        /// <summary>
        /// The user's profile picture URL. You can manage this yourself or use our account pages which allow the user to upload a picture that we host.
        /// </summary>
        [JsonPropertyName("pictureUrl")]
        public string? PictureUrl { get; set; }

        /// <summary>
        /// A dictionary of custom properties for the user. See Custom Properties for more details.
        /// </summary>
        [JsonPropertyName("properties")]
        public Dictionary<string, object> Properties { get; set; } = [];
    }
}