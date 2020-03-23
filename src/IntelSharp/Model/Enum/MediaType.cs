namespace IntelSharp.Model
{
    /// <summary>
    /// Specifies the higher-level media types available
    /// </summary>
    public enum MediaType
    {
        Undefined = 0,

        PasteDocument,
        PasteUser,
        Forum,
        ForumBoard,
        ForumThread,
        ForumPost,
        ForumUser,
        WebsiteScreenshot,
        WebsiteHtml,

        //DOCS: Invalid 10-12

        Tweet = 13,
        URL,
        DocumentPdf,
        DocumentWord,
        DocumentExcel,
        DocumentPowerpoint,
        Picture,
        Audio,
        Video,
        Container, //Archive
        Html,
        Text,
        Ebook,

        Database = 27,
        Email,
        IndexFile,
        Domain
    }
}
