using System;
using System.Collections.Generic;

namespace eFolio.DTO.Common
{
    public class Developer
    {
        public Developer()
        {

        }
        public Developer(int id, string fullName, string cVLink, string photoBase64)
        {
            Id = id;
            FullName = fullName;
            CVLink = cVLink;
            PhotoBase64 = photoBase64;
            Projects = new List<Project>();
        }

        public void HasPhoto(byte[] content)
        {
            PhotoBase64 = "data:image/jpg;base64," + Convert.ToBase64String(content);
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