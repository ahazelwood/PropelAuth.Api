using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class SamlIdentityProviderMetadataRequest
    {
        /// <summary>
        /// The organization ID.
        /// </summary>
        [JsonPropertyName("org_id")]
        public required string OrgId { get; set; }

        /// <summary>
        /// The Entity ID (also known as Identity Provider Issuer) from the organization's Identity Provider (IdP).
        /// </summary>
        [JsonPropertyName("idp_entity_id")]
        public required string EntityId { get; set; }

        /// <summary>
        /// The Single Sign-On URL from the organization's Identity Provider (IdP).
        /// </summary>
        [JsonPropertyName("idp_sso_url")]
        public required string SsoUrl { get; set; }

        /// <summary>
        /// The Base-64 encoded X.509 certificate from the organization's Identity Provider (IdP).
        /// </summary>
        [JsonPropertyName("idp_certificate")]
        public required string Certificate { get; set; }

        /// <summary>
        /// The name of the SAML provider. Must equal 'Google', 'Rippling', 'OneLogin', 'JumpCloud', 'Okta', 'Azure', 'Duo', or 'Generic'
        /// </summary>
        [JsonPropertyName("provider")]
        public required string ProviderName { get; set; }
    }
}