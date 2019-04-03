using eFolio.DTO.Common;

namespace eFolio.Elastic
{
    public class ElasticDeveloperData
    {
        public static readonly ElasticDeveloperData NotFound = new ElasticDeveloperData()
        {
            Id = 0,
            Name = "Not found",
        };

        public ElasticDeveloperData()
        {
        }

        public ElasticDeveloperData(ElasticDeveloperData data, CVKind isExtended)
        {
            Id = data.Id;
            Name = data.Name;
            ExternalCV = data.ExternalCV;

            if (isExtended == CVKind.Internal)
            {
                InternalCV = data.InternalCV;
            }
            else 
            {
                InternalCV = "Internal CV is not available";
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
    
        public string InternalCV { get; set; }
        public string ExternalCV { get; set; }
    }
}

