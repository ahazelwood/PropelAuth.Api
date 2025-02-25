using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class TokenOrgMemberInfo 
    {
        [JsonPropertyName("org_id")]
        public string OrgId { get; set; } = null!;

        [JsonPropertyName("org_name")]
        public string OrgName { get; set; } = null!;

        [JsonPropertyName("org_metadata")]
        public Dictionary<string, object> OrgMetadata { get; set; } = [];

        [JsonPropertyName("user_save_org_name")]
        public string? UrlSafeOrgName { get; set; }

        [JsonPropertyName("user_role")]
        public string? UserAssignedRole { get; set; }

        [JsonPropertyName("inherited_user_roles_plus_current_role")]
        public List<string> UserInheritedRolesPlusCurrentRole { get; set; } = [];

        [JsonPropertyName("user_permissions")]
        public List<string> UserPermissions { get; set; } = [];

        [JsonPropertyName("additional_roles")]
        public List<string> UserAssignedAdditionalRoles { get; set; } = [];
    }
}