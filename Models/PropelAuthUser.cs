using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class PropelAuthUser
    {
        /// <summary>
        /// The internal unique id associated with the user.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = null!;

        /// <summary>
        /// The user's email address
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Whether the user's email address has been confirmed. 
        /// </summary>
        [JsonPropertyName("email_confirmed")]
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// The user's username.
        /// </summary>
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        /// <summary>
        /// The user's first name (or given name).
        /// </summary>
        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        /// <summary>
        /// The user's last name (or surname).
        /// </summary>
        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        /// <summary>
        /// The external id associated with this user.
        /// </summary>
        [JsonPropertyName("legacy_user_id")]
        public string? LegacyUserId { get; set; }

        /// <summary>
        /// If specified, a url to a picture that represents the user.
        /// </summary>
        [JsonPropertyName("picture_url")]
        public string? PictureUrl { get; set; }

        /// <summary>
        /// Has a password been set for the user yet.
        /// </summary>
        [JsonPropertyName("has_password")]
        public bool HasPassword { get; set; }

        /// <summary>
        /// If true, the user needs to be prompted to set/update their password.
        /// </summary>
        [JsonPropertyName("update_password_required")]
        public bool UpdatePasswordRequired { get; set; }

        /// <summary>
        /// If true, the user has been locked out.
        /// </summary>
        [JsonPropertyName("locked")]
        public bool Locked { get; set; }

        /// <summary>
        /// If false, the user has been logically deleted from the system. 
        /// </summary>
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Has the user enabled MFA yet?
        /// </summary>
        [JsonPropertyName("mfa_enabled")]
        public bool MfaEnabled { get; set; }

        /// <summary>
        /// Is the user allowed to create additional organizations?
        /// </summary>
        [JsonPropertyName("can_create_orgs")]
        public bool CanCreateOrgs { get; set; }

        /// <summary>
        /// Timestamp that presents when the user was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> representation of the CreatedAt property.
        /// </summary>
        [JsonIgnore]
        public DateTime CreatedAtDate => DateTime.UnixEpoch.AddSeconds(CreatedAt);

        /// <summary>
        /// Timestamp that represents when the user was last active.
        /// </summary>
        [JsonPropertyName("last_active_at")]
        public long? LastActiveAt { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> representation of the LastActiveAt property.
        /// </summary>
        public DateTime? LastActiveAtDate => LastActiveAt.HasValue ? DateTime.UnixEpoch.AddSeconds((double)LastActiveAt) : null;

        /// <summary>
        /// If set, the user id of the person impersonating the specified user.
        /// </summary>
        [JsonPropertyName("impersonator_user_id")]
        public string? ImpersonatorUserId { get; set; }

        /// <summary>
        /// Lists the organizations the user is a member of and their membership information.
        /// </summary>
        [JsonPropertyName("org_id_to_org_info")]
        public Dictionary<string, OrgMemberInfo>? OrgIdToOrgMemberInfo { get; set; }

        /// <summary>
        /// A list of predefined properties associated with the user.
        /// </summary>
        [JsonPropertyName("properties")]
        public Dictionary<string, object>? Properties { get; set; }

        /// <summary>
        /// If specified, the means by which the user was logged in.
        /// </summary>
        [JsonPropertyName("login_method")]
        public LoginMethod? LoginMethod { get; set; }

        /// <summary>
        /// If specified, the user's current organization.
        /// </summary>
        [JsonPropertyName("active_org_id")]
        public string? ActiveOrgId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropelAuthUser" /> class.
        /// </summary>
        public PropelAuthUser() {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropelAuthUser" /> class.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        public PropelAuthUser(ClaimsPrincipal claimsPrincipal) {
            UserId = claimsPrincipal.FindFirst("user_id")?.Value ?? null!;
            Email = claimsPrincipal.FindFirst("email")?.Value ?? null!;
            FirstName = claimsPrincipal.FindFirst("first_name")?.Value;
            LastName = claimsPrincipal.FindFirst("last_name")?.Value;
            Username = claimsPrincipal.FindFirst("username")?.Value;
            LegacyUserId = claimsPrincipal.FindFirst("legacy_user_id")?.Value;
            ImpersonatorUserId = claimsPrincipal.FindFirst("impersonator_user_id")?.Value;

            var orgsClaim = claimsPrincipal.FindFirst("org_id_to_org_member_info") ?? claimsPrincipal.FindFirst("org_member_info");
            if (orgsClaim != null) {
                if (orgsClaim.Type == "org_id_to_org_member_info") {
                    OrgIdToOrgMemberInfo = JsonSerializer.Deserialize<Dictionary<string, OrgMemberInfo>>(orgsClaim.Value);
                }
                else {
                    var orgInfo = JsonSerializer.Deserialize<OrgMemberInfo>(orgsClaim.Value);
                    if (orgInfo != null) {
                        OrgIdToOrgMemberInfo = new Dictionary<string, OrgMemberInfo> {
                        { orgInfo.OrgId ?? "", orgInfo }
                    };
                        ActiveOrgId = orgInfo.OrgId;
                    }
                }
            }

            var propertiesClaim = claimsPrincipal.FindFirst("properties");
            if (propertiesClaim != null) {
                Properties = JsonSerializer.Deserialize<Dictionary<string, object>>(propertiesClaim.Value);
            }

            var loginMethodClaim = claimsPrincipal.FindFirst("login_method");
            if (loginMethodClaim != null) {
                LoginMethod = ParseLoginMethod(loginMethodClaim.Value);
            }
            else {
                LoginMethod = LoginMethod.Unknown();
            }

            LoginMethod ParseLoginMethod(string loginMethodString) {
                switch (loginMethodString) {
                    case "password":
                        return LoginMethod.Password();
                    case "magic_link":
                        return LoginMethod.MagicLink();
                    case "social_sso":
                        var provider = claimsPrincipal.FindFirst("social_provider")?.Value;
                        return LoginMethod.SocialSso(provider ?? "unknown");
                    case "email_confirmation_link":
                        return LoginMethod.EmailConfirmationLink();
                    case "saml_sso":
                        var samlProvider = claimsPrincipal.FindFirst("saml_provider")?.Value;
                        var orgId = claimsPrincipal.FindFirst("org_id")?.Value;
                        return LoginMethod.SamlSso(samlProvider ?? "unknown", orgId ?? "unknown");
                    case "impersonation":
                        return LoginMethod.Impersonation();
                    case "generated_from_backend_api":
                        return LoginMethod.GeneratedFromBackendApi();
                    default:
                        return LoginMethod.Unknown();
                }
            }
        }

        public bool IsRoleInOrg(string orgId, string role) {
            var org = GetOrg(orgId);
            return org?.IsRole(role) ?? false;
        }

        public bool IsAtLeastRoleInOrg(string orgId, string role) {
            var org = GetOrg(orgId);
            return org?.IsAtLeastRole(role) ?? false;
        }

        public bool HasPermissionInOrg(string orgId, string permission) {
            var org = GetOrg(orgId);
            return org?.HasPermission(permission) ?? false;
        }

        public bool HasAllPermissionsInOrg(string orgId, string[] permissions) {
            var org = GetOrg(orgId);
            return org?.HasAllPermissions(permissions) ?? false;
        }

        public OrgMemberInfo[] GetOrgs() {
            if (OrgIdToOrgMemberInfo == null) {
                return [];
            }
            return OrgIdToOrgMemberInfo.Values.ToArray();
        }

        public bool IsImpersonated() {
            return !string.IsNullOrEmpty(ImpersonatorUserId);
        }

        public OrgMemberInfo? GetOrg(string orgId) {
            if (OrgIdToOrgMemberInfo != null && OrgIdToOrgMemberInfo.TryGetValue(orgId, out var orgInfo)) {
                return orgInfo;
            }
            return null;
        }

        public object? GetUserProperty(string propertyName) {
            if (Properties != null && Properties.TryGetValue(propertyName, out var value)) {
                return value;
            }
            return null;
        }

        public OrgMemberInfo? GetActiveOrg() {
            if (string.IsNullOrEmpty(ActiveOrgId) || OrgIdToOrgMemberInfo == null) {
                return null;
            }

            if (OrgIdToOrgMemberInfo.TryGetValue(ActiveOrgId, out var activeOrgInfo)) {
                return activeOrgInfo;
            }
            return null;
        }

        public string? GetActiveOrgId() {
            return ActiveOrgId;
        }
    }
}
