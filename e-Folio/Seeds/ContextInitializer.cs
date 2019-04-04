using eFolio.EF;
using eFolio.Elastic;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eFolio.API.Seeds
{
    public class ContextInitializer
    {
        public static void Initialize(eFolioDBContext context, IEfolioElastic elastic)
        {
            context.Database.EnsureCreated();

            if (!context.Projects.Any())
            {
                context.Projects.Add(new ProjectEntity()
                {
                    Id = 0,
                    Name = "WebApp",
                    Context = new ContextEntity()
                    {
                        SourceCodeLink = "bbb",
                        ScreenLinks = new List<FolioFileEntity>()
                        {
                            new FolioFileEntity()
                            {
                                IsInternal = true,
                                Path = "ooo"
                            },
                            new FolioFileEntity()
                            {
                                IsInternal = false,
                                Path = "djnk"
                            }
                        }
                    }
                });
                context.Projects.Add(new ProjectEntity()
                {
                    Id = 0,
                    Name = "AnyApp",
                    Context = new ContextEntity()
                    {
                        SourceCodeLink = "yyy",
                        ScreenLinks = new List<FolioFileEntity>()
                        {
                            new FolioFileEntity()
                            {
                                IsInternal = false,
                                Path = "uuru"
                            },
                            new FolioFileEntity()
                            {
                                IsInternal = true,
                                Path = "hyrrr"
                            }
                        }
                    }
                });
                context.SaveChanges();

                foreach (var project in context.Projects.ToList())
                {
                    elastic.AddItem(new ElasticProjectData()
                    {
                        Id = project.Id,
                        Name = project.Name,
                        ExternalDescr = "You are gay",
                        InternalDescr = "This is internal description"
                    });
                }
            } 

            if (!context.Clients.Any())
            {
                context.Clients.Add(new ClientEntity()
                {
                    FullNameClient = "Tetyana Dovha",
                    Comment = "hbje",
                    ContactPersons = new List<ContactPersonEntity>()
                    {
                        new ContactPersonEntity
                        {
                            FullName = "Anna Albert",
                            eMail = "anna@gmail.com",
                            Phone = 22454,
                            Comment = "hfke"
                        },
                        new ContactPersonEntity
                        {
                            FullName = "Oleksiy Vasyliev",
                            eMail = "vasyliev@gmail.com",
                            Phone = 8787,
                            Comment = "oerro"
                        }
                    }
                });
                context.Clients.Add(new ClientEntity()
                {
                    FullNameClient = "Oleh Marinyak",
                    Comment = "mktyr",
                    ContactPersons = new List<ContactPersonEntity>()
                    {
                        new ContactPersonEntity
                        {
                            FullName = "Vasyl Bilyi",
                            eMail = "vbilyi@gmail.com",
                            Phone = 774,
                            Comment = "klklr"
                        },
                        new ContactPersonEntity
                        {
                            FullName = "Jonas Zhukovsky",
                            eMail = "jonas@gmail.com",
                            Phone = 8997,
                            Comment = "jrlkel"
                        }
                    }
                });
                context.SaveChanges();
            }

            if (!context.Developers.Any())
            {
                context.Developers.Add(
                    new DeveloperEntity() {
                        FullName = "Yurii Levko",
                        CVLink = "asfasf",
                        PhotoLink = Environment.CurrentDirectory + "\\Seeds\\PhotoDeveloper\\Levko.jpg"
                    });
                context.Developers.Add(
                    new DeveloperEntity() {
                        FullName = "Ostap Roik",
                        CVLink = "swrherh",
                        PhotoLink = Environment.CurrentDirectory + "\\Seeds\\PhotoDeveloper\\Roik.jpg"
                    });
                context.SaveChanges();
            }
        }
    }
}