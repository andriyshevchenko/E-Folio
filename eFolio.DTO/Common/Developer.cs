using System;
using System.Collections.Generic;

namespace eFolio.DTO.Common
{
    public class Developer
    {
        public Developer()
        {

        }
        public Developer(int id, string fullName, string cVLink)
        {
            Id = id;
            FullName = fullName;
            CVLink = cVLink;
            Projects = new List<Project>();
        }

        public void HasPhoto(byte[] content, string fileFormat)
        {
            PhotoBase64 = string.Format("data:image/{0};base64,{1}", 
                fileFormat.Substring(1,fileFormat.Length-1), 
                Convert.ToBase64String(content)
            );
        }

        public void UpdateId(int id)
        {
            if (id > 0)
            {
                Id = id;
            }
        }

        public int Id { get; set; }

        public string PhotoBase64 { get; set; }

        public string FullName { get; set; }
        public string CVLink { get; set; }
        public string InternalCV { get; set; }
        public string ExternalCV { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}