using System;
using System.Collections.Generic;
using System.Text;

namespace e_folio.core.Entities
{
    public class ContactPersonEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string eMail { get; set; }
        public int Phone { get; set; }
        public string Comment { get; set; }
    }
}
