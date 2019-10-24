using DocumentService.Application.Commands;
using DocumentServices.Common.Exceptions;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace DocumentService.Application.Components.Impl
{
    public class LocalTemplateProvider : ITemplateProvider
    {
        private IHostingEnvironment _hostingEnvironment;

        public LocalTemplateProvider(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string GetTemplate(TemplateToPdfCommand templateToPdfCommand)
        {
            string path;

            switch (templateToPdfCommand.DocumentType?.ToLower())
            {
                case "affidavit":
                    path = Path.Combine(_hostingEnvironment.ContentRootPath, "Resources\\TaxAffidavit\\PA\\attachment.html");
                    break;
                default:
                    throw new MicroserviceException($"No template setup for DocumentType {templateToPdfCommand.DocumentType}");
            }

            return File.ReadAllText(path);
        }
    }
}
