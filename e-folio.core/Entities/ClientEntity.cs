using System.Collections.Generic;

namespace eFolio.EF
{
    public class ClientEntity
    {
        public int Id { get; set; }
        public string FullNameClient { get; set; }
        public List<ContactPersonEntity> ContactPersons { get; set; }
        public string Comment { get; set; }
    }
}