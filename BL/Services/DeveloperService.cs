using AutoMapper;
using eFolio.DTO;
using eFolio.EF;
using eFolio.Elastic;
using System;
using System.Collections.Generic;

namespace eFolio.BL
{
    public class DeveloperService : IDeveloperService
    {
        private IMapper mapper;
        private DeveloperRepository developerRepository;
        private ElasticSearch elastic;

        public DeveloperService(eFolioDBContext DBContext, IMapper mapper)
        {
            developerRepository = new DeveloperRepository(DBContext);
            elastic = new ElasticSearch();
            this.mapper = mapper;
        }

        public void Add(Developer item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Developer GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Developer> GetItemsList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Developer> Search(string request, Paging paging)
        {
            throw new NotImplementedException();
        }

        public void Update(Developer item)
        {
            throw new NotImplementedException();
        }
    }
}