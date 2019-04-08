using eFolio.DTO.Common;
using System.Collections.Generic;
using System.Linq;

namespace eFolio.EF
{
    public class ContextEntity
    {
        public int Id { get; set; }
        public string SourceCodeLink { get; set; }
        public List<FolioFileEntity> ScreenLinks { get; set; }
        public ProjectEntity Project { get; set; }

        public void Update(Context context)
        {
            SourceCodeLink = context.SourceCodeLink;
        }

        public List<int> ScreenLinkIds()
        {
            return ScreenLinks.Select(link => link.Id).ToList();
        }
    }
}