using System.Collections.Generic;

namespace e_folio.data
{
    public class Client
    {
        public string FullNameClient { get; set; }
        public List<ContactPerson> ContactPersons { get; set; }
        public string Comment { get; set; }
    }
}
