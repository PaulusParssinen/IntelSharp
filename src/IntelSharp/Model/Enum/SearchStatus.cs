namespace IntelSharp.Model
{
    /// <summary>
    /// Represents the status of search request. Tells whether it is possible to fetch the results using <c>FetchResultsAsync</c> or not.
    /// </summary>
    public enum SearchStatus
    {
        Success = 0,
        InvalidTerm,
        MaxConcurrentSearches
    }
}