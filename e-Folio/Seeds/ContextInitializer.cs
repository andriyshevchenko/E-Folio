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
                new Project { ProjectID = 1, NameProject = "WebApp", NameClient = "Ivan Lisovskiy"},
                new Project { ProjectID = 2, NameProject = "AnyApp", NameClient = "Yeyzaveta Brednyeva"}
            };
            context.Projects.AddRange(projects);
            context.SaveChanges();
         }

        if (!context.Developers.Any())
        {
            var devs = new List<Developer>
            {
                new Developer {FullName = "Yurii Levko", CVLink = "bdjkwljfj"},
                new Developer {FullName = "Ostap Roik", CVLink = "uuuuuuuuuroel" }
            };
            context.Developers.AddRange(devs);
            context.SaveChanges();
        }

        if (!context.Descriptions.Any())
        {
            var descr = new List<Description>
            {

                new Description
                    {DescriptionText = "yyeenklfdglkr", SourceCodeLinks = {"fheufhk", "adssed"}, ScreenLinks = {"fbdjkrj", "hjhewfhk"}},
                new Description {DescriptionText = "oroeoif", SourceCodeLinks = {"kfmelew", "ghfhjh"}, ScreenLinks = {"ppeiri", "hoefkk"}}
            };
            context.Descriptions.AddRange(descr);
            context.SaveChanges();
        }
    }
   
        


}
}

