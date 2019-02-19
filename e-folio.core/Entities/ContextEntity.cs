using System;
using System.Collections.Generic;
using System.Text;

namespace e_folio.core.Entities
{
    public class ContextEntity
    {
        public int Id { get; set; }
        public string SourceCodeLink { get; set; }
        public List<FolioFileEntity> ScreenLinks { get; set; }
    }
}
