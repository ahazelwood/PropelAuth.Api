using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class FindUsersInOrgRequest
    {
        /// <summary>
        /// The ID of the organization to fetch users for.
        /// </summary>
        [JsonPropertyName("org_id")]
        public required string OrgId { get; set; }

        /// <summary>
        /// Whether to include all organizations the user is in in the response.
        /// </summary>
        [JsonPropertyName("include_orgs")]
        public bool IncludeOrgs { get; set; }

        /// <summary>
        /// Only return users with this role within the organization. This is case sensitive.
        /// </summary>
        [JsonPropertyName("role")]
        public string? Role { get; set; }

        /// <summary>
        /// The number of users to return per page.
        /// </summary>
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = 0;

        /// <summary>
        /// The page number to return.
        /// </summary>
        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; } = 10;
    }
}