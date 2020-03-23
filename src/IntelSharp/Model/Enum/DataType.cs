namespace IntelSharp.Model
{
    /// <summary>
    /// Specifies the low-level data types available
    /// </summary>
    public enum DataType
    {
        Binary = 0,
        Plaintext,
        Picture,
        Video,
        Audio,
        Document,
        Executable,
        Container,

        User = 1001,
        Leak,
        URL,
        Forum
    }
}
