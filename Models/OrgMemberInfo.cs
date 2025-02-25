using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class OrgMemberInfo
    {
        [JsonPropertyName("org_id")]
        public string? OrgId { get; set; }

        [JsonPropertyName("org_name")]
        public string? OrgName { get; set; }

        [JsonPropertyName("url_safe_org_name")]
        public string? UrlSafeOrgName { get; set; }

        [JsonPropertyName("org_metadata")]
        public Dictionary<string, object>? OrgMetadata { get; set; }

        [JsonPropertyName("user_role")]
        public string? UserRole { get; set; }

        [JsonPropertyName("inherited_user_roles_plus_current_role")]
        public List<string>? InheritedUserRolesPlusCurrentRole { get; set; }

        [JsonPropertyName("org_role_structure")]
        public string? OrgRoleStructure { get; set; }

        [JsonPropertyName("additional_roles")]
        public List<string>? AdditionalRoles { get; set; }

        [JsonPropertyName("user_permissions")]
        public List<string>? UserPermissions { get; set; }

        public bool IsRole(string role) {
            return UserRole == role;
        }
        public bool IsAtLeastRole(string role) {
            return UserRole == role || InheritedUserRolesPlusCurrentRole != null && InheritedUserRolesPlusCurrentRole.Contains(role);
        }
        public bool HasPermission(string permission) {
            return UserPermissions != null && UserPermissions.Contains(permission);
        }
        public bool HasAllPermissions(string[] permissions) {
            if (UserPermissions != null) {
                return permissions.All(UserPermissions.Contains);
            }
            return false;
        }
    }
}
