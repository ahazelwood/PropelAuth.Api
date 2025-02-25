using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class PendingOrgInviteRequest 
    {
        /// <summary>
        /// The number of invites to return per page.
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