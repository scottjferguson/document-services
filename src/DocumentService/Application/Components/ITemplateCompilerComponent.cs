using System.Collections.Generic;

namespace DocumentService.Application.Components
{
    public interface ITemplateCompilerComponent
    {
        string Compile(string template, Dictionary<string, string> data);
    }
}
