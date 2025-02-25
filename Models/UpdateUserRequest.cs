using System.Text.Json.Serialization;

namespace PropelAuth.Api.Models
{
    public class UpdateUserRequest 
    {
        /// <summary>
        /// The user's username.
        /// </summary>
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        /// <summary>
        /// The user's first name.
        /// </summary>
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        /// <summary>
        /// The user's last name.
        /// </summary>
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        /// <summary>
        /// The user's profile picture URL.
        /// </summary>
        [JsonPropertyName("pictureUrl")]
        public string? PictureUrl { get; set; }

        /// <summary>
        /// A dictionary of predefined custom properties for the user.
        /// </summary>
        [JsonPropertyName("properties")]
        public Dictionary<string, object?> Properties { get; set; } = [];

        /// <summary>
        /// If true, the user will be prompted to set/update their password when they next log in. Default is false.
        /// </summary>
        [JsonPropertyName("updatePasswordRequired")]
        public bool UpdatePasswordRequired { get; set; } = false;
    }


}