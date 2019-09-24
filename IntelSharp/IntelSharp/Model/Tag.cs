namespace IntelSharp.Model
{
    /// <summary>
    /// Represents the classification of the <see cref="Item"/>.
    /// </summary>
    public class Tag
    {
        public ClassType Class { get; set; }
        public string Value { get; set; }
    }
}
