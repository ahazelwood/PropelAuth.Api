using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class FindUserRequest 
    {
        /// <summary>
        /// A partial email or username to filter the list by.
        /// </summary>
        [JsonPropertyName("email_or_username")]
        public string? EmailOrUsername { get; set; }

        /// <summary>
        /// The user ID from another system, such as a former auth provider. We'll return any users that contain this ID.
        /// </summary>
        [JsonPropertyName("legacy_user_id")]
        public string? LegacyUserId { get; set; }

        /// <summary>
        /// How should results be ordered?
        /// </summary>
        [JsonPropertyName("order_by")]
        public FindUserOrderBy OrderBy { get; set; } = FindUserOrderBy.Email;

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        [JsonPropertyName("include_orgs")]
        public bool IncludeOrgs { get; set; } = false;

        /// <summary>
        /// The page number to return.
        /// </summary>
        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; } = 0;

        /// <summary>
        /// The number of users to return per page.
        /// </summary>
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = 10;
    }
}