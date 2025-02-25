using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class RemoveUserFromOrgRequest
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
    }
}