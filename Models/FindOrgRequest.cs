using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class FindOrgRequest
    {
        /// <summary>
        /// A name to search for. We'll return any organizations whose name contains this value.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// A domain to search for. We'll return any organizations whose domain matches this value.
        /// </summary>
        [JsonPropertyName("domain")]
        public string? Domain { get; set; }

        /// <summary>
        /// The org ID from another system, such as a former auth provider. We'll return any organizations that contain this ID.
        /// </summary>
        [JsonPropertyName("legacy_org_id")]
        public string? LegacyOrgId { get; set; }

        /// <summary>
        /// An enum value to order the organizations by. Possible values include CREATED_AT_ASC, CREATED_AT_DESC, NAME.
        /// </summary>
        [JsonPropertyName("order_by")]
        public FindOrgOrderBy OrderBy { get; set; } = FindOrgOrderBy.Name;

        /// <summary>
        /// The number of organizations to return per page.
        /// </summary>
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// The page number to return.
        /// </summary>
        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; } = 0;
    }
}