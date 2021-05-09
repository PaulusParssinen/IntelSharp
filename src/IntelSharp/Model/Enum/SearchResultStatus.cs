namespace IntelSharp.Model
{
    /// <summary>
    /// Represents all possible search job statuses.
    /// </summary>
    public enum SearchResultStatus
    {
        Success = 0,
        NoMoreResults,
        SearchIdNotFound,
        NotReady,
        Error
    }
}
