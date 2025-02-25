using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class CustomRoleMappingResponse 
    {
        /// <summary>
        /// The current list of defined custom roles.
        /// </summary>
        [JsonPropertyName("custom_role_mappings")]
        public List<CustomRoleMapping> CustomRoleMappings { get; set; } = [];
    }

    public class CustomRoleMapping 
    {
        /// <summary>
        /// The name of the custom role.
        /// </summary>
        [JsonPropertyName("custom_role_mapping_name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// How many organizations are subscribed to the custom role.
        /// </summary>
        [JsonPropertyName("num_orgs_subscribed")]
        public int NumberOrgsSubscribed { get; set; }
    }
}