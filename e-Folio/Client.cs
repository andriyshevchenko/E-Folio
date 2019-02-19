using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eFolio
{
    public class Client
    {
        public string FullNameClient { get; set; }
        public List<ContactPerson> ContactPersons { get; set; }
        public string Comment { get; set; }
    }
}
