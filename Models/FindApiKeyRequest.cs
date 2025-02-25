using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class FindApiKeyRequest 
    {
        /// <summary>
        /// Filter by the user ID of the API key owner.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// Filter by the email of the API key owner.
        /// </summary>
        [JsonPropertyName("user_email")]
        public string? UserEmail { get; set; }

        /// <summary>
        /// Filter by the organization ID of the API key owner.
        /// </summary>
        [JsonPropertyName("org_id")]
        public string? OrgId { get; set; }

        /// <summary>
        /// The page number to return.
        /// </summary>
        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; } = 0;

        /// <summary>
        /// The number of results to return per page.
        /// </summary>
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = 10;
    }
}