using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class PendingOrgInviteResponse 
    {
        /// <summary>
        /// A list of pending organization invitations.
        /// </summary>
        [JsonPropertyName("invites")]
        public List<PendingOrgInvite> Invites { get; set; } = [];

        /// <summary>
        /// The total number of invitations waiting.
        /// </summary>
        [JsonPropertyName("total_invites")]
        public int TotalInvites { get; set; }

        /// <summary>
        /// The current page of results.
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// The number of results to return per page.
        /// </summary>
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }
    }
}