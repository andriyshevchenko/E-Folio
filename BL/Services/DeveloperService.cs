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
            DeveloperEntity de = mapper.Map<DeveloperEntity>(item);
            developerRepository.Add(de);

            item.Update(de.Id);
            ElasticDeveloperData eld = mapper.Map<ElasticDeveloperData>(item);
            elastic.AddItem(eld);
        }

        public void Delete(int id)
        {
            developerRepository.Delete(id);

            elastic.DeleteDeveloperItem(id);
        }

        public Developer GetItem(int id)
        {
            var developerEntity = developerRepository.GetItem(id);
            var elasticDeveloper = elastic.GetDeveloperById(id);

            return GetMergeDeveloper(developerEntity, elasticDeveloper);
        }

        public IEnumerable<Developer> GetItemsList()
        {
            var developerEntities = developerRepository.GetItemsList();
            var elasticDevelopers = GetElasticDevelopers(developerEntities);

            var e1 = developerEntities.GetEnumerator();
            var e2 = elasticDevelopers.GetEnumerator();
            while (e1.MoveNext() && e2.MoveNext())
            {
                yield return GetMergeDeveloper(e1.Current, e2.Current);
            }
        }

        public IEnumerable<Developer> Search(string request, Paging paging)
        {
            var elasticDevelopers = elastic.SearchItemsDeveloper(request, paging);
            var developerEntities = GetEntityDevelopers(elasticDevelopers);

            var e1 = developerEntities.GetEnumerator();
            var e2 = elasticDevelopers.GetEnumerator();
            while (e1.MoveNext() && e2.MoveNext())
            {
                yield return GetMergeDeveloper(e1.Current, e2.Current);
            }
        }

        public void Update(Developer item)
        {
            developerRepository.Update(mapper.Map<DeveloperEntity>(item));

            elastic.UpdateDeveloperData(mapper.Map<ElasticDeveloperData>(item));
        }

        private IEnumerable<ElasticDeveloperData> GetElasticDevelopers(IEnumerable<DeveloperEntity> developers)
        {
            foreach (var item in developers)
            {
                yield return elastic.GetDeveloperById(item.Id);
            }
        }

        private IEnumerable<DeveloperEntity> GetEntityDevelopers(IEnumerable<ElasticDeveloperData> developers)
        {
            foreach (var item in developers)
            {
                yield return developerRepository.GetItem(item.Id);
            }
        }

        private Developer GetMergeDeveloper(DeveloperEntity developerEntity, ElasticDeveloperData elasticDeveloperData)
        {
            var developer = mapper.Map<Developer>(Tuple.Create(elasticDeveloperData, developerEntity));

            return developer;
        }
    }
}