using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class CreateAccessTokenResponse 
    {
        /// <summary>
        /// The newly created AccessToken that can be used to test the backend without a frontend.
        /// </summary>
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}