using System.Collections.Generic;

namespace DocumentService.Domain.Entities
{
    public class TokenMapEntity
    {
        public TokenMapEntity()
        {
            TokenEntities = new List<TokenEntity>();
        }

        public string ViewName { get; set; }

        public List<TokenEntity> TokenEntities { get; set; }
    }
}
