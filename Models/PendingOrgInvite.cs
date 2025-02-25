using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class PendingOrgInvite 
    {
        /// <summary>
        /// The email address of the user being invited.
        /// </summary>
        [JsonPropertyName("invitee_email")]
        public string InviteeEmail { get; set; } = null!;

        /// <summary>
        /// The organization the user is being invited to.
        /// </summary>
        [JsonPropertyName("org_id")]
        public string OrgId { get; set; } = null!;

        /// <summary>
        /// The name of the organization.
        /// </summary>
        [JsonPropertyName("org_name")]
        public string OrgName { get; set; } = null!;

        /// <summary>
        /// The primary role associated to the user.
        /// </summary>
        [JsonPropertyName("role_in_org")]
        public string RoleInOrg { get; set; } = null!;

        /// <summary>
        /// Any additional roles assigned to the user.
        /// </summary>
        [JsonPropertyName("additional_roles_in_org")]
        public List<string> AdditionalRolesInOrg { get; set; } = [];

        /// <summary>
        /// The timestamp for when the invitation is was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> representation of the CreatedAt property.
        /// </summary>
        [JsonIgnore]
        public DateTime CreatedAtDate => DateTime.UnixEpoch.AddSeconds(CreatedAt);

        /// <summary>
        /// The timestamp for when the invitation will expire.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public long ExpiresAt { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> representation of the ExpiresAt property.
        /// </summary>
        [JsonIgnore]
        public DateTime ExpiresAtDate => DateTime.UnixEpoch.AddSeconds(ExpiresAt);

        /// <summary>
        /// The email address of the user who invited the new user.
        /// </summary>
        [JsonPropertyName("inviter_email")]
        public string InviterEmail { get; set; } = null!;

        /// <summary>
        /// The user id of the user who is inviting the new user.
        /// </summary>
        [JsonPropertyName("inviter_user_id")]
        public string InviterUserId { get; set; } = null!;
    }
}