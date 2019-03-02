using eFolio.DTO;

namespace eFolio.EF
{
    public class FolioFileEntity
    {
        public FolioFileEntity()
        {

        }

        public FolioFileEntity(FolioFile folioFile, int context)
        {
            Id = folioFile.Id;
            IsInternal = folioFile.IsInternal;
            Path = folioFile.Path;
            ContextEntityId = context;
        }

        public void Update(FolioFile folioFile)
        {
            IsInternal = folioFile.IsInternal;
            Path = folioFile.Path;
        }

        public int Id { get; set; }
        public bool IsInternal { get; set; }
        public string Path { get; set; }

        public int ContextEntityId { get; set; }
        public ContextEntity Context { get; set; }
    }
}
