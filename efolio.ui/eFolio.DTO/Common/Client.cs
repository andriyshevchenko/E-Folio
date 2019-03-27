using System.Collections.Generic;

namespace eFolio.DTO.Common
{
    public class Client
    {
        public string FullNameClient { get; set; }
        public List<ContactPerson> ContactPersons { get; set; }
        public string Comment { get; set; }
    }
}
