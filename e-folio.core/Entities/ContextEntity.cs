using System.Collections.Generic;

namespace eFolio.EF
{
    public class ContextEntity
    {
        public int Id { get; set; }
        public string SourceCodeLink { get; set; }
        public List<FolioFileEntity> ScreenLinks { get; set; }
    }
}