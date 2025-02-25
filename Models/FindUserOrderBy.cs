namespace PropelAuth.Api.Models
{
    /// <summary>
    /// Enumeration of OrderBy options when finding a User.
    /// </summary>
    public enum FindUserOrderBy {
        CreatedAtAsc,
        CreatedAtDesc,
        LastActiveAtAsc,
        LastActiveAtDesc,
        Email,
        Username
    }
}