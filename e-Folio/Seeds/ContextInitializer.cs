using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_folio.data;
using e_folio.core;
using Microsoft.EntityFrameworkCore.Internal;
using eFolio.EF;
using eFolio.Elastic;

namespace e_Folio.Seeds

{
    public class ContextInitializer
    {

        public static void Initialize(eFolioDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Projects.Any()) return;

            var projectEntity1 = new ProjectEntity
            { Id = 0,
                Name = "WebApp",
                Context = {SourceCodeLink = "bbb",
                    ScreenLinks =new List<FolioFileEntity>()
                    { new FolioFileEntity {IsInternal=true, Path="ooo"},
                   new FolioFileEntity { IsInternal = false, Path = "djnk"}
                    } }
            };

            var projectEntity2 = new ProjectEntity
            {
                Id =1, 
                Name = "AnyApp",
                Context = {SourceCodeLink="yyy",
                    ScreenLinks = new List<FolioFileEntity>()
                    {new FolioFileEntity{IsInternal = false, Path = "uuru"},
                    new FolioFileEntity {IsInternal = true, Path = "hyrrr"}}
                    }
            };

            var CPersons = new List<ContactPersonEntity>
            {
                new ContactPersonEntity
                    {FullName = "Oksana Sydorenko", eMail = "osyd@gmail.com", Phone = 201545, Comment = "fejkk"},
                new ContactPersonEntity
                    {FullName = "Yaryna Gopchuk", eMail = "gopchuk@gmail.com", Phone = 255478, Comment = "ejejjrjj"}
            };

            var clients = new List<ClientEntity>
            {
                new ClientEntity   {FullNameClient = "Tetyana Dovha",
                    ContactPersons = new List<ContactPersonEntity>()
                    { new ContactPersonEntity {FullName="Anna Albert", eMail="anna@gmail.com", Phone=22454, Comment="hfke" },
                    new ContactPersonEntity {FullName="Oleksiy Vasyliev", eMail="vasyliev@gmail.com", Phone=8787, Comment="oerro" }
                    } ,
                    Comment = "hbje"},
                new ClientEntity   {FullNameClient = "Oleh Marinyak",
                    ContactPersons = new List<ContactPersonEntity>()
                    { new ContactPersonEntity {FullName="Vasyl Bilyi", eMail="vbilyi@gmail.com", Phone=774, Comment="klklr" },
                    new ContactPersonEntity {FullName="Jonas Zhukovsky", eMail="jonas@gmail.com", Phone=8997, Comment="jrlkel" }
                    }  ,
                    Comment = "mktyr"} };

            context.AddRange(clients);

            if (!context.Projects.Any())
            {

                var projects = new List<ProjectEntity>
            {
                projectEntity1,
                projectEntity2
               };
                context.Projects.AddRange(projects);
                context.SaveChanges();
            }

            if (!context.Developers.Any())
            {
                var devs = new List<DeveloperEntity>
            {
                new DeveloperEntity {FullName = "Yurii Levko", CVLink = "bdjkwljfj"},
                new DeveloperEntity {FullName = "Ostap Roik", CVLink = "uuuuuuuuuroel" }
            };
                context.Developers.AddRange(devs);
                context.SaveChanges();
            }


            var folio = new List<FolioFileEntity>
            {

                new FolioFileEntity {IsInternal = true, Path = "chvhbjkn"},
                new FolioFileEntity {IsInternal = false, Path = "hbjdjk"}
            };
            context.FolioFiles.AddRange(folio);
            context.SaveChanges();


            if (!context.Contexsts.Any())
            {
                var cont = new List<ContextEntity>
            {
                new ContextEntity {SourceCodeLink = "bjcknkd",
                    ScreenLinks =  new List<FolioFileEntity>()
                    { new FolioFileEntity {IsInternal=true, Path="ooo"},
                   new FolioFileEntity { IsInternal = false, Path = "djnk"}
                    }},
                new ContextEntity { SourceCodeLink = "hbje",
                    ScreenLinks = new List<FolioFileEntity>()
                    { new FolioFileEntity {IsInternal=true, Path="ooo"},
                   new FolioFileEntity { IsInternal = false, Path = "djnk"}
                    } }

            };
            }

            var Elastic = new List<ElasticProjectData>
            {
                new ElasticProjectData {Id = 0, Name = "WebApp", InternalDescr= "1st int descr", ExternalDescr = "1st ext descr" },
                new ElasticProjectData {Id = 1, Name ="AnyApp", InternalDescr = "2nd int descr", ExternalDescr = "2nd ext descr"}

            };

            context.SaveChanges();
        }

    }
}

