namespace IntelSharp.Model
{
    /// <summary>
    /// Represents the possible output formats for <see cref="FileApi.ViewAsync(Item, ViewFormat, bool)"/>
    /// </summary>
    public enum ViewFormat
    {
        /// <summary>
        /// Text view, any non-printable characters shall be removed, UTF-8 encoding
        /// </summary>
        Text = 0,
        /// <summary>
        /// Hex view.
        /// </summary>
        Hex,
        /// <summary>
        /// Detects automatically whether to return hex or text view.
        /// </summary>
        AutoHexOrText,
        /// <summary>
        /// Picture view.
        /// </summary>
        Picture,

        /// <summary>
        /// HTML inline view. Content will be sanitized and modified!
        /// </summary>
        InlineHTML = 5,
        /// <summary>
        /// Text view of PDF. Content will be automatically converted
        /// </summary>
        PDFText,
        /// <summary>
        /// Text view of HTML.
        /// </summary>
        HTMLText,
        /// <summary>
        /// Text view of Word files (DOC/DOCX/RTF). 
        /// </summary>
        MSWordText,
    }
}
