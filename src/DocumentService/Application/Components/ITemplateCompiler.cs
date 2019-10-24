using System.Collections.Generic;

namespace DocumentService.Application.Components
{
    public interface ITemplateCompiler
    {
        string Compile(string template, Dictionary<string, string> data);
    }
}
