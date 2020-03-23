namespace IntelSharp.Model
{
    /// <summary>
    /// Defines the order in which the search result is sorted.
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// No sorting.
        /// </summary>
        None = 0,

        /// <summary>
        /// Least "relevant" items first.
        /// </summary>
        XScoreAscending,
        /// <summary>
        /// Most "relevant" items first
        /// </summary>
        XScoreDescending,

        /// <summary>
        /// Oldest items first.
        /// </summary>
        DateAscending,
        /// <summary>
        /// Newest items first.
        /// </summary>
        DateDescending
    }
}
