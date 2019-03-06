using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eFolio.API.Models;
using Microsoft.EntityFrameworkCore.Internal;
using eFolio.EF;
using eFolio.Elastic;
using Microsoft.AspNetCore.Identity;

namespace eFolio.Api.Seeds

{
    public class ContextInitializer
    {
        public static void Initialize(eFolioDBContext context)
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
                context.Developers.Add(new DeveloperEntity() { FullName = "Yurii Levko", CVLink = "asfasf" });
                context.Developers.Add(new DeveloperEntity() { FullName = "Ostap Roik", CVLink = "swrherh" });
                context.SaveChanges();
            }
            //    context.Database.EnsureCreated();



            //    var projectEntity1 = new ProjectEntity
            //    {
            //        Id = 0,
            //        Name = "WebApp",
            //        Context = new ContextEntity
            //        {
            //            SourceCodeLink = "bbb",
            //            ScreenLinks = new List<FolioFileEntity>()
            //            {
            //                new FolioFileEntity
            //                {
            //                    IsInternal = true,
            //                    Path = "ooo"
            //                },
            //                new FolioFileEntity
            //                {
            //                    IsInternal = false,
            //                    Path = "djnk"
            //                }
            //            }
            //        }
            //    };

            //    var projectEntity2 = new ProjectEntity
            //    {
            //        Id = 0,
            //        Name = "AnyApp",
            //        Context = new ContextEntity
            //        {
            //            SourceCodeLink = "yyy",
            //            ScreenLinks = new List<FolioFileEntity>()
            //            {
            //                new FolioFileEntity
            //                {
            //                    IsInternal = false,
            //                    Path = "uuru"
            //                },
            //                new FolioFileEntity
            //                {
            //                    IsInternal = true,
            //                    Path = "hyrrr"
            //                }
            //            }
            //        }
            //    };

            //    var CPersons = new List<ContactPersonEntity>
            //    {
            //        new ContactPersonEntity
            //        {
            //            FullName = "Oksana Sydorenko",
            //            eMail = "osyd@gmail.com",
            //            Phone = 201545,
            //            Comment = "fejkk"
            //        },
            //        new ContactPersonEntity
            //        {
            //            FullName = "Yaryna Gopchuk",
            //            eMail = "gopchuk@gmail.com",
            //            Phone = 255478,
            //            Comment = "ejejjrjj"
            //        }
            //    };

            //    var clients = new List<ClientEntity>
            //    {
            //        new ClientEntity
            //        {
            //            FullNameClient = "Tetyana Dovha",
            //            ContactPersons = new List < ContactPersonEntity > ()
            //            {
            //                new ContactPersonEntity
            //                {
            //                    FullName = "Anna Albert",
            //                    eMail = "anna@gmail.com",
            //                    Phone = 22454,
            //                    Comment = "hfke"
            //                },
            //                new ContactPersonEntity
            //                {
            //                    FullName = "Oleksiy Vasyliev",
            //                    eMail = "vasyliev@gmail.com",
            //                    Phone = 8787,
            //                    Comment = "oerro"
            //                }
            //            },
            //            Comment = "hbje"
            //        },
            //        new ClientEntity
            //        {
            //            FullNameClient = "Oleh Marinyak",
            //            ContactPersons = new List < ContactPersonEntity > ()
            //            {
            //                new ContactPersonEntity
            //                {
            //                    FullName = "Vasyl Bilyi",
            //                    eMail = "vbilyi@gmail.com",
            //                    Phone = 774,
            //                    Comment = "klklr"
            //                },
            //                new ContactPersonEntity
            //                {
            //                    FullName = "Jonas Zhukovsky",
            //                    eMail = "jonas@gmail.com",
            //                    Phone = 8997,
            //                    Comment = "jrlkel"
            //                }
            //            },
            //            Comment = "mktyr"
            //        }
            //    };

            //    context.AddRange(clients);

            //    if (!context.Projects.Any())
            //    {

            //        var projects = new List<ProjectEntity>
            //        {
            //            projectEntity1,
            //            projectEntity2
            //        };
            //        context.Projects.AddRange(projects);
            //        context.SaveChanges();
            //    }

            //    if (!context.Developers.Any())
            //    {
            //        var devs = new List<DeveloperEntity>
            //        {
            //            new DeveloperEntity {
            //                FullName = "Yurii Levko",
            //                CVLink = "bdjkwljfj"
            //            },
            //            new DeveloperEntity
            //            {
            //                FullName = "Ostap Roik",
            //                CVLink = "uuuuuuuuuroel"
            //            }
            //        };
            //        context.Developers.AddRange(devs);
            //        context.SaveChanges();
            //    }

            //    var folio = new List<FolioFileEntity>
            //    {

            //        new FolioFileEntity
            //        {
            //            IsInternal = true,
            //            Path = "chvhbjkn"
            //        },
            //        new FolioFileEntity
            //        {
            //            IsInternal = false,
            //            Path = "hbjdjk"
            //        }
            //    };
            //    context.FolioFiles.AddRange(folio);
            //    context.SaveChanges();

            //    if (!context.Contexsts.Any())
            //    {
            //        var cont = new List<ContextEntity>
            //        {
            //            new ContextEntity
            //            {
            //                SourceCodeLink = "bjcknkd",
            //                ScreenLinks = new List < FolioFileEntity > ()
            //                {
            //                    new FolioFileEntity
            //                    {
            //                        IsInternal = true,
            //                        Path = "ooo"
            //                    }, 
            //                    new FolioFileEntity
            //                    {
            //                        IsInternal = false,
            //                        Path = "djnk"
            //                    }
            //                }
            //            },
            //            new ContextEntity
            //            {
            //                SourceCodeLink = "hbje",
            //                ScreenLinks = new List < FolioFileEntity > ()
            //                {
            //                    new FolioFileEntity
            //                    {
            //                        IsInternal = true,
            //                        Path = "ooo"
            //                    },
            //                    new FolioFileEntity
            //                    {
            //                        IsInternal = false,
            //                        Path = "djnk"
            //                    }
            //                }
            //            }

            //        };
            //    }

            //    var ElasticProj = new List<ElasticProjectData>
            //    {
            //        new ElasticProjectData {Id = projectEntity1.Id, Name = "WebAppProj", InternalDescr= "1st int descr", ExternalDescr = "1st ext descr" },
            //        new ElasticProjectData {Id = projectEntity2.Id, Name ="AnyAppProj", InternalDescr = "2nd int descr", ExternalDescr = "2nd ext descr"},

            //    };

            //    var ElasticDev = new List<ElasticDeveloperData>
            //    {
            //        new ElasticDeveloperData {Id = projectEntity1.Id, Name = "WebAppDev", InternalCV= "1st int descr", ExternalCV = "1st ext descr" },
            //        new ElasticDeveloperData {Id = projectEntity2.Id, Name ="AnyAppDev", InternalCV = "2nd int descr", ExternalCV = "2nd ext descr"},

            //    };
            //    context.SaveChanges();
            //}

        }
    }
}