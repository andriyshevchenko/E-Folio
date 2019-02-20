using System;
using System.Collections.Generic;
using System.Text;

namespace e_folio.core.Entities
{
    public class ClientEntity
    {
        public int Id { get; set; }
        public string FullNameClient { get; set; }
        public List<ContactPersonEntity> ContactPersons { get; set; }
        public string Comment { get; set; }
    }
}
