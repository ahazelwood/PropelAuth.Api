using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class SubscribeOrgToCustomRoleRequest
    {
        /// <summary>
        /// The name of the Custom Role to subscribe the organization to.
        /// </summary>
        [JsonPropertyName("custom_role_mapping_name")]
        public required string CustomRoleMappingName { get; set; }
    }
}