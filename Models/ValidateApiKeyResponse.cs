using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class ValidateApiKeyResponse 
    {
        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; set; } = [];

        [JsonPropertyName("user")]
        public PropelAuthUser? User { get; set; }

        [JsonPropertyName("org")]
        public PropelAuthOrg? Org { get; set; }

        [JsonPropertyName("user_in_org")]
        public TokenOrgMemberInfo? UserInOrg { get; set; } 
    }
}