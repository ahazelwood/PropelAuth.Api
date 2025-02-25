using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class UpdateOrgRequest
    {
        /// <summary>
        /// The name of the organization. This can only include alphanumeric characters, spaces, and underscores.
        /// </summary>
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        /// <summary>
        /// The domain of the organization.
        /// </summary>
        [JsonPropertyName("domain")]
        public string? Domain { get; set; }

        /// <summary>
        /// Whether users can automatically join the organization if their email address matches the organization domain. 
        /// If this is true, you must also set the domain property.
        /// </summary>
        [JsonPropertyName("enable_auto_joining_by_domain")]
        public bool EnableAutoJoiningByDomain { get; set; }

        /// <summary>
        /// Whether users will not be able to join the organization unless their email address matches the organization domain. 
        /// If this is true, you must also set the domain property.
        /// </summary>
        [JsonPropertyName("members_must_have_matching_domain")]
        public bool MembersMustHaveMatchingDomain { get; set; }

        /// <summary>
        /// The maximum number of users allowed in the organization. If not set, there is no limit.
        /// </summary>
        [JsonPropertyName("max_users")]
        public int? MaxUsers { get; set; }

        /// <summary>
        /// Whether users can set up SAML for the organization.
        /// </summary>
        [JsonPropertyName("can_setup_saml")]
        public bool CanSetupSaml { get; set; }

        /// <summary>
        /// The org ID from another system, such as a former auth provider. This ID will be be present everywhere the org ID is.
        /// </summary>
        [JsonPropertyName("legacy_org_id")]
        public string? LegacyOrgId { get; set; }

        /// <summary>
        /// A JSON object containing any metadata you want to store about the organization.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; set; } = [];
    }
}