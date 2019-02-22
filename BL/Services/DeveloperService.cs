using eFolio.DTO;
using eFolio.EF;
using eFolio.Elastic;
using System;
using System.Collections.Generic;

namespace eFolio.BL
{
    public class DeveloperService : IDeveloperService
    {
        private DeveloperRepository developerRepository;
        private ElasticSearch elastic;

        public DeveloperService(eFolioDBContext DBContext)
        {
            developerRepository = new DeveloperRepository(DBContext);
            elastic = new ElasticSearch();
        }

        public Developer GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Developer> GetItemsList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Developer> Search(string request)
        {
            throw new NotImplementedException();
        }
    }
}