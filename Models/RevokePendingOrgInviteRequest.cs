using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class RevokePendingOrgInviteRequest 
    {
        /// <summary>
        /// The organization ID.
        /// </summary>
        [JsonPropertyName("org_id")]
        public required string OrgId { get; set; }


        /// <summary>
        /// The email address of the invitee.
        /// </summary>
        [JsonPropertyName("invitee_email")]
        public required string InviteeEmail { get; set; }
    }
}