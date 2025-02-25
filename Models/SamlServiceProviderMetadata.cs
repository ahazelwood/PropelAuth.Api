using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class SamlServiceProviderMetadata 
    {
        /// <summary>
        /// The unique Entity ID of the SAML issuer.
        /// </summary>
        [JsonPropertyName("entity_id")]
        public string EntityId { get; set; } = null!;

        /// <summary>
        /// The Attribute Consume Service (ACS) Endpoint.
        /// </summary>
        [JsonPropertyName("acs_url")]
        public string AcsUrl { get; set; } = null!;

        /// <summary>
        /// Gets or sets the logout URL.
        /// </summary>
        [JsonPropertyName("logout_url")]
        public string LogoutUrl { get; set; } = null!;
    }
}