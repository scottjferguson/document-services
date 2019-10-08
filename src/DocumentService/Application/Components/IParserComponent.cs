using DocumentService.Domain.Entities;
using System.Collections.Generic;
using System.Data;

namespace DocumentService.Application.Components
{
    public interface IParserComponent
    {
        TemplateMetadataEntity ParseTemplate(string template);
        Dictionary<string, string> CreateDynamicMap(TokenMapEntity tokenMapEntity, DataRow dataRow);
    }
}
