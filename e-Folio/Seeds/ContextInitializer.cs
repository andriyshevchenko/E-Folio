using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_folio.data;
using e_folio.core;
using Microsoft.EntityFrameworkCore.Internal;
using e_folio.core.Entities;

namespace e_Folio.Seeds

{
public class ContextInitializer
{
    protected void CreateProjects()
        { }
    protected void CreateDevelopers()
        { }
        public static void Initialize(eFolioDBContext context)
    {
       context.Database.EnsureCreated();

        if (!context.ContactPersons.Any())
        {
            var CPersons = new List<ContactPersonEntity>
            {
                new ContactPersonEntity
                    {FullName = "Oksana Sydorenko", eMail = "osyd@gmail.com", Phone = 201545, Comment = "fejkk"},
                new ContactPersonEntity
                    {FullName = "Yaryna Gopchuk", eMail = "gopchuk@gmail.com", Phone = 255478, Comment = "ejejjrjj"}
            };
            context.AddRange(CPersons);
            context.SaveChanges();
        }

        if (!context.Clients.Any())
        {
            var clients = new List<ClientEntity>
            {
                new ClientEntity
                    {FullNameClient = "Tetyana Dovha", ContactPersons = {"hfjkkr", "fehfe"}, Comment = "hbje"},
                new ClientEntity
                    {FullNameClient = "Oleh Marinyak", ContactPersons = {"iiit", "ehehe"}, Comment = "mktyr"}
            };
                context.AddRange(clients);
            context.SaveChanges();
        }
        if (!context.Projects.Any())
        {
            var projects = new List<ProjectEntity>
            {
                new ProjectEntity { Name = "WebApp", Context = {SourceCodeLink = "bbb", ScreenLinks = {"rr", "jj"} }},
                new ProjectEntity {  Name = "AnyApp", Context = {SourceCodeLink = "ppp", ScreenLinks = {"ss", "tt"}}}
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

        if (!context.FolioFiles.Any())
        {
            var folio = new List<FolioFileEntity>
            {

                new FolioFileEntity {IsInternal = true, Path = "chvhbjkn"},
                new FolioFileEntity {IsInternal = false, Path = "hbjdjk"}
            };
            context.FolioFiles.AddRange(folio);
            context.SaveChanges();
        }

        if (!context.Contexsts.Any())
        {
            var cont = new List<ContextEntity>
            {
                new ContextEntity {SourceCodeLink = "bjcknkd", ScreenLinks = {"bcjenk", "jvjknkv"} },
                new ContextEntity { SourceCodeLink = "hbje", ScreenLinks = { "iiirje", "mmgnd"} }

            };
        }

    }
   
        


}
}

