using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class AddUserToOrgRequest
    {
        /// <summary>
        /// The user's ID.
        /// </summary>
        [JsonPropertyName("user_id")]
        public required string UserId { get; set; }

        /// <summary>
        /// The organization ID.
        /// </summary>
        [JsonPropertyName("org_id")]
        public required string OrgId { get; set; }

        /// <summary>
        /// The role the user should have in the organization.
        /// </summary>
        [JsonPropertyName("role")]
        public required string Role { get; set; }

        /// <summary>
        /// Additional roles that the user should have in the organization. This is only available when multi-role support is enabled.
        /// </summary>
        [JsonPropertyName("additional_roles")]
        public List<string> AdditionalRoles { get; set; } = [];
    }
}