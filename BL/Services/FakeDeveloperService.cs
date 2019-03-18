using eFolio.DTO;
using eFolio.EF;
using eFolio.Elastic;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using eFolio.DTO.Common;
using Microsoft.EntityFrameworkCore;

namespace eFolio.BL
{
    /// <summary>
    /// This class is used to test controllers. Please don't delete.
    /// </summary>
    public class FakeDeveloperService : IDeveloperService
    {
        private DeveloperRepository developerRepository; 
        private eFolioDBContext dbContext;
        private IMapper converter;

        public FakeDeveloperService(eFolioDBContext DBContext, IMapper converter)
        {
            dbContext = DBContext;
            this.converter = converter;
            developerRepository = new DeveloperRepository(DBContext); 
        }

        public void Add(Developer item)
        {
            developerRepository.Add(converter.Map<DeveloperEntity>(item));
        }

        public void Delete(int id)
        {
            developerRepository.Delete(id);
        }

        public Developer GetItem(int id)
        {
            DeveloperEntity source = dbContext.Developers.Find(id);
            return converter.Map<Developer>(source);
        }

        public IEnumerable<Developer> GetItemsList()
        {
            return dbContext.Developers.ToListAsync().Result
            .Select(converter.Map<Developer>);
        }

        public IEnumerable<Developer> Search(string request, Paging paging)
        {
            throw new NotImplementedException();
        }

        public void Update(Developer item)
        {
            developerRepository.Update(converter.Map<DeveloperEntity>(item));
        }
    }
}