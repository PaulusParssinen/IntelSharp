using System.Collections.Generic;

namespace IntelSharp.Model
{
    public class ListSelectorsResponse
    {
        public IEnumerable<Selector> Selectors { get; set; }
        public int Count { get; set; }
    }
}
