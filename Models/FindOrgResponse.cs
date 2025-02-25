using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class FindOrgResponse 
    {
        /// <summary>
        /// The list of matching Organizations.
        /// </summary>
        [JsonPropertyName("orgs")]
        public List<PropelAuthOrg> Orgs { get; set; } = [];

        /// <summary>
        /// The total number of Organizations that matched.
        /// </summary>
        [JsonPropertyName("total_orgs")]
        public int TotalOrgs { get; set; }

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

        /// <summary>
        /// Returns if there more results (pages).
        /// </summary>
        [JsonPropertyName("has_more_results")]
        public bool HasMoreResults { get; set; }
    }
}