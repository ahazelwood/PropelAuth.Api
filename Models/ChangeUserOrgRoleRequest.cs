using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class ChangeUserOrgRoleRequest
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
    }
}