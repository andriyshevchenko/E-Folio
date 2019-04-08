using System;
using System.Collections.Generic;

namespace eFolio.DTO.Common
{
    public class Project 
    {
        public int Id { get; set; }

        public void UpdateId(int id)
        {
            if (id > 0)
            {
                Id = id;
            }
        }

        public void HasPhoto(byte[] content, string fileFormat)
        {
            PhotoBase64 = string.Format("data:image/{0};base64,{1}",
                            fileFormat.Substring(1, fileFormat.Length - 1),
                            Convert.ToBase64String(content)
            );
        }

        public string Name { get; set; }

        public string PhotoBase64 { get; set; }

        public Context Context { get; set; }

        public ICollection<Developer> Developers { get; set; }

        public string InternalDescription { get; set; }
        public string ExternalDescription { get; set; }
    }
}