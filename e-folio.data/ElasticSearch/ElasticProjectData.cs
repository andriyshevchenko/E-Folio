using eFolio.DTO.Common;

namespace eFolio.Elastic
{
    public class ElasticProjectData
    {
        public static readonly ElasticProjectData NotFound = new ElasticProjectData()
        {
            Id = 0,
            Name = "Not found",
        };

        public ElasticProjectData()
        {
        }

        public ElasticProjectData(ElasticProjectData data, DescriptionKind isExtended)
        {
            Id = data.Id;
            Name = data.Name;
            ExternalDescr = data.ExternalDescr;

            if (isExtended == DescriptionKind.Internal)
            {
                InternalDescr = data.InternalDescr  ;
            }
            else
            {
                InternalDescr = "Internal CV is not available";
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        
        public string InternalDescr { get; set; }
        public string ExternalDescr { get; set; }
    }
}