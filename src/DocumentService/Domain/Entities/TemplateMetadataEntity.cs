using System.Collections.Generic;

namespace DocumentService.Domain.Entities
{
    public class TemplateMetadataEntity
    {
        public TemplateMetadataEntity()
        {
            TokenMapEntities = new List<TokenMapEntity>();
        }

        public List<TokenMapEntity> TokenMapEntities { get; set; }
    }
}
