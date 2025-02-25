using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class CreateOrgResponse
    {
        /// <summary>
        /// The newly created Organization's ID.
        /// </summary>
        [JsonPropertyName("org_id")]
        public string OrgId { get; set; }


        /// <summary>
        /// The Organization's name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}