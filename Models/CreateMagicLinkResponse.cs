namespace PropelAuth.Api.Models
{
    public class CreateMagicLinkResponse 
    {
        /// <summary>
        /// The Magic Link URL that can be emailed to a user.
        /// </summary>
        public string Url { get; set; }
    }
}