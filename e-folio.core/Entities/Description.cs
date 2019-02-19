using System.Collections.Generic;

namespace e_folio.core.Entities
{
    public class Description
    {
        public string DescriptionText { get; set; }
        public List<string> SourceCodeLinks { get; set; }
        public List<string> ScreenLinks { get; set; }
    }
}