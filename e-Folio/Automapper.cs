using AutoMapper;
using eFolio.DTO.Common;
using eFolio.EF;
using eFolio.Elastic;
using System;
using System.Linq;

namespace eFolio.API
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            //Mapping into DTO
            CreateMap<Tuple<ElasticProjectData, ProjectEntity>, Project>().ForMember(p => p.Id, m => m.MapFrom(pe => pe.Item2.Id))
                                .ForMember(p => p.Name, m => m.MapFrom(pe => pe.Item2.Name))
                                .ForMember(p => p.Context, m => m.MapFrom(pe => pe.Item2.Context))
                                .ForMember(p => p.Developers,
                                           m => m.MapFrom(
                                               pe => pe.Item2.Developers.Select(
                                                   pd => new Developer(pd.DeveloperId,
                                                                       pd.DeveloperEntity.FullName,
                                                                       pd.DeveloperEntity.CVLink)).ToList()
                                               )
                                           )
                                .ForMember(p => p.InternalDescription, m => m.MapFrom(epd => epd.Item1.InternalDescr))
                                .ForMember(p => p.ExternalDescription, m => m.MapFrom(epd => epd.Item1.ExternalDescr));

            CreateMap<Tuple<ElasticDeveloperData, DeveloperEntity>, Developer>().ForMember(p => p.Id, m => m.MapFrom(pe => pe.Item2.Id))
                                .ForMember(p => p.FullName, m => m.MapFrom(pe => pe.Item2.FullName))
                                .ForMember(p => p.CVLink, m => m.MapFrom(pe => pe.Item2.CVLink)) 
                                .ForMember(p => p.Projects, m => m.Ignore())
                                .ForMember(p => p.InternalCV, m => m.MapFrom(pe => pe.Item1.InternalCV))
                                .ForMember(p => p.ExternalCV, m => m.MapFrom(pe => pe.Item1.ExternalCV));

            CreateMap<ClientEntity, Client>().ForMember(p => p.FullNameClient, m => m.MapFrom(pe => pe.FullNameClient))
                                .ForMember(p => p.ContactPersons, m => m.MapFrom(pe => pe.ContactPersons))
                                .ForMember(p => p.Comment, m => m.MapFrom(pe => pe.Comment));

            CreateMap<ContactPersonEntity, ContactPerson>().ForMember(p => p.FullName, m => m.MapFrom(pe => pe.FullName))
                                .ForMember(p => p.eMail, m => m.MapFrom(pe => pe.eMail))
                                .ForMember(p => p.Phone, m => m.MapFrom(pe => pe.Phone))
                                .ForMember(p => p.Comment, m => m.MapFrom(pe => pe.Comment));

            CreateMap<ContextEntity, Context>().ForMember(p => p.SourceCodeLink, m => m.MapFrom(pe => pe.SourceCodeLink))
                                .ForMember(p => p.ScreenLinks, m => m.MapFrom(pe => pe.ScreenLinks))
                                .ForMember(pe => pe.Id, m => m.MapFrom(p => p.Id));

            CreateMap<DeveloperEntity, Developer>().ForMember(p => p.Id, m => m.MapFrom(pe => pe.Id))
                                .ForMember(p => p.FullName, m => m.MapFrom(pe => pe.FullName))
                                .ForMember(p => p.CVLink, m => m.MapFrom(pe => pe.CVLink))  
                                .ForMember(p => p.Projects, m => m.Ignore());

            CreateMap<FolioFileEntity, FolioFile>().ForMember(p => p.IsInternal, m => m.MapFrom(pe => pe.IsInternal))
                                 .ForMember(p => p.Path, m => m.MapFrom(pe => pe.Path))
                                 .ForMember(pe => pe.Id, m => m.MapFrom(p => p.Id));

            //Mapping from DTO
            CreateMap<Client, ClientEntity>().ForMember(pe => pe.FullNameClient, m => m.MapFrom(p => p.FullNameClient))
                                .ForMember(pe => pe.ContactPersons, m => m.MapFrom(p => p.ContactPersons))
                                .ForMember(pe => pe.Comment, m => m.MapFrom(p => p.Comment));

            CreateMap<ContactPerson, ContactPersonEntity>().ForMember(pe => pe.FullName, m => m.MapFrom(p => p.FullName))
                                .ForMember(pe => pe.eMail, m => m.MapFrom(p => p.eMail))
                                .ForMember(pe => pe.Phone, m => m.MapFrom(p => p.Phone))
                                .ForMember(pe => pe.Comment, m => m.MapFrom(p => p.Comment));

            CreateMap<Context, ContextEntity>()
                                .ForMember(pe => pe.SourceCodeLink, m => m.MapFrom(p => p.SourceCodeLink));

            CreateMap<Developer, DeveloperEntity>().ForMember(pe => pe.FullName, m => m.MapFrom(p => p.FullName))
                                .ForMember(pe => pe.CVLink, m => m.MapFrom(p => p.CVLink)) 
                                .ForMember(pe => pe.Projects, m => m.MapFrom(p => p.Projects));

            CreateMap<Developer, ElasticDeveloperData>().ForMember(pe => pe.Name, m => m.MapFrom(p => p.FullName))
                                .ForMember(pe => pe.Id, m => m.MapFrom(p => p.Id))
                                .ForMember(pe => pe.InternalCV, m => m.MapFrom(p => p.InternalCV))
                                .ForMember(pe => pe.ExternalCV, m => m.MapFrom(p => p.ExternalCV));

            CreateMap<FolioFile, FolioFileEntity>().ForMember(pe => pe.IsInternal, m => m.MapFrom(p => p.IsInternal))
                                .ForMember(pe => pe.Path, m => m.MapFrom(p => p.Path))
                                .ForMember(pe => pe.Id, m => m.MapFrom(p => p.Id));

            CreateMap<Project, ProjectEntity>().ForMember(pe => pe.Id, m => m.MapFrom(p => p.Id))
                                .ForMember(pe => pe.Name, m => m.MapFrom(p => p.Name))
                                .ForMember(pe => pe.Context, m => m.MapFrom(p => p.Context))
                                .ForMember(pe => pe.ContextId, m => m.MapFrom(p => p.Context.Id))
                                .ForMember(pe => pe.Developers,
                                           m => m.MapFrom(
                                               p => p.Developers.Select(
                                                   d => new ProjectDeveloperEntity(p.Id, d.Id)
                                               ).ToList()
                                           )
                                        );

            CreateMap<Project, ElasticProjectData>().ForMember(epd => epd.Id, m => m.MapFrom(p => p.Id))
                                .ForMember(epd => epd.Name, m => m.MapFrom(p => p.Name))
                                .ForMember(epd => epd.InternalDescr, m => m.MapFrom(p => p.InternalDescription))
                                .ForMember(epd => epd.ExternalDescr, m => m.MapFrom(p => p.ExternalDescription));
        }
    }
}
