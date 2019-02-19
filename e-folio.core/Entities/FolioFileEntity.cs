using System;
using System.Collections.Generic;
using System.Text;

namespace e_folio.core.Entities
{
    public class FolioFileEntity
    {
        public int Id { get; set; }
        public bool IsInternal { get; set; }
        public string Path { get; set; }
    }
}
