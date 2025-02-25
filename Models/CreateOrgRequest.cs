using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class CreateOrgRequest
    {
        /// <summary>
        /// The name of the organization. This can only include alphanumeric characters, spaces, and underscores.
        /// </summary>
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        /// <summary>
        /// The email domain of the organization.
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
        /// The name of the custom role and permissions configuration.
        /// </summary>
        [JsonPropertyName("custom_role_mapping_name")]
        public string? CustomRoleMappingName { get; set; }

        /// <summary>
        /// The org's ID in a previous system. This ID will be stored on the org as a legacyOrgId and it is 
        /// present everywhere orgId's are (e.g. fetching org metadata).
        /// </summary>
        [JsonPropertyName("legacy_org_id")]
        public string? LegacyOrgId { get; set; }
    }
}