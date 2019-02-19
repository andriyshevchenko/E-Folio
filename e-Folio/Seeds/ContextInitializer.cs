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
        public static void Initialize(eFolioDBContext context)
    {
        context.Database.EnsureCreated();
        if (!context.Projects.Any())
        {
            var projects = new List<Project>
            {
                new Project { ProjectID = 1, NameProject = "WebApp", NameClient = "jkjrmg"},
                new Project { ProjectID = 2, NameProject = "AnyApp", NameClient = "jjbknknknknkg"}
            };
            context.Projects.AddRange(projects);
            context.SaveChanges();
         }

        if (!context.Developers.Any())
        {
            var devs = new List<Developer>
            {
                new Developer {FullName = "jbjksbee", CVLink = "bdjkwlan;l"},
                new Developer {FullName = "yyyyy", CVLink = "uuuuuuuuuroel" }
            };
            context.Developers.AddRange(devs);
            context.SaveChanges();
        }
        }
   
        


}
}

