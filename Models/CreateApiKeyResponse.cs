namespace PropelAuth.Api.Models
{
    public class CreateApiKeyResponse
    {
        public required string ApiKeyId { get; set; }
        public required string ApiKeyToken { get; set; }
    }
}