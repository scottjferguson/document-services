using HandlebarsDotNet;
using System.Collections.Generic;

namespace DocumentService.Application.Components.Impl
{
    public class HandlebarsTemplateCompiler : ITemplateCompiler
    {
        public string Compile(string template, Dictionary<string, string> data)
        {
            string result = Handlebars.Compile(template)(data);

            return result;
        }
    }
}
