using System.Collections.Generic;

namespace eFolio.DTO
{
    public class Context
    {
        public int Id { get; set; }
        public string SourceCodeLink { get; set; }
        public List<FolioFile> ScreenLinks { get; set; }
    }
}