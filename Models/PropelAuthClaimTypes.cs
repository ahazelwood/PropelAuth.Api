namespace PropelAuth.Api.Models
{
    /// <summary>
    /// A list of predefined ClaimTypes that correspond to known PropelAuth properties.
    /// </summary>
    public class PropelAuthClaimTypes
    {
        public const string UserId = "urn:PropelAuth:user_id";
        public const string LegacyUserId = "urn:PropelAuth:legacy_user_id";
        public const string ImpersonatorId = "urn:PropelAuth.impersonator_id";
        public const string PictureUrl = "urn:PropelAuth:picture_url";
        public const string Locked = "urn:PropelAuth:locked";
        public const string Enabled = "url:PropelAuth:enabled";
    }
}
