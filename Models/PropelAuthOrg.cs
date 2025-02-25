using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class PropelAuthOrg
    {
        /// <summary>
        /// The organization ID.
        /// </summary>
        [JsonPropertyName("org_id")]
        public string OrgId { get; set; } = null!;

        /// <summary>
        /// The name of the organization. This can only include alphanumeric characters, spaces, and underscores.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// A Url-Safe slug that can be used in a URL for the current organization name.
        /// </summary>
        [JsonPropertyName("url_safe_org_slug")]
        public string? UrlSafeOrgSlug { get; set; }

        /// <summary>
        /// Is the organization allowed to configure their SAML setup.
        /// </summary>
        [JsonPropertyName("can_setup_saml")]
        public bool CanSetupSaml { get; set; }

        /// <summary>
        /// Has the organization configured a custom SAML setup.
        /// </summary>
        [JsonPropertyName("is_saml_configured")]
        public bool IsSamlConfigured { get; set; }

        /// <summary>
        /// Is SAML only configured in test mode.
        /// </summary>
        [JsonPropertyName("is_saml_in_test_mode")]
        public bool IsSamlInTestMode { get; set; }

        /// <summary>
        /// The domain of the organization.
        /// </summary>
        [JsonPropertyName("domain")]
        public string? Domain { get; set; }

        /// <summary>
        /// Whether users can automatically join the organization if their email address matches the organization domain.
        /// </summary>
        [JsonPropertyName("domain_auto_join")]
        public bool DomainAutoJoin { get; set; }

        /// <summary>
        /// Whether users will not be able to join the organization unless their email address matches the organization domain. 
        /// </summary>
        [JsonPropertyName("domain_restrict")]
        public bool DomainRestrict { get; set; }

        /// <summary>
        /// The maximum number of users allowed in the organization.
        /// </summary>
        [JsonPropertyName("max_users")]
        public int? MaxUsers { get; set; } = null;

        /// <summary>
        /// The name of the custom role and permissions configuration.
        /// </summary>
        [JsonPropertyName("custom_role_mapping_name")]
        public string? CustomRoleMappingName { get; set; }

        /// <summary>
        /// The org ID from another system, such as a former auth provider.
        /// </summary>
        [JsonPropertyName("legacy_org_id")]
        public string? LegacyOrgId { get; set; }

        /// <summary>
        /// A object containing any metadata stored about the organization.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; } = [];
    }
}