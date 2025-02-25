using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class CreateMagicLinkRequest
    {
        /// <summary>
        /// The user's email address.
        /// </summary>
        [JsonPropertyName("email")]
        public required string Email { get; set; }

        /// <summary>
        /// The URL to redirect the user to after they've logged in.
        /// </summary>
        [JsonPropertyName("redirectToUrl")]
        public string? RedirectToUrl { get; set; }

        /// <summary>
        /// How many hours the link should be valid for.
        /// </summary>
        [JsonPropertyName("expiresInHours")]
        public int ExpireInHours { get; set; }

        /// <summary>
        /// If true, when the user clicks the link, a new user will be created if one does not already exist.
        /// </summary>
        [JsonPropertyName("createNewUserIfOneDoesntExist")]
        public bool CreateNewUserIfNotExists { get; set; } = false;
    }
}