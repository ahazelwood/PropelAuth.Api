using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class CreateSamlConnectionLinkResponse
    {
        /// <summary>
        /// URL that allows a user to setup SAML for an organization without logging in or creating an account.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;
    }
}