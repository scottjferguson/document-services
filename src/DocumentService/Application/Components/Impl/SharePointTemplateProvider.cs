using Core.Framework.Attributes;
using Core.Framework.Enums;
using DocumentService.Application.Commands;
using System.Net;

namespace DocumentService.Application.Components.Impl
{
    [Injectable(AutoWiring = Opt.Out)]
    public class SharePointTemplateProvider : ITemplateProvider
    {
        const string testURL = @"https://perigeeenergy.sharepoint.com/Operations/_layouts/15/DocIdRedir.aspx?ID=3A7QS5M76UKQ-764267245-77&hintUrl=DropOffLibrary/test-loa2.html";

        public string GetTemplate(TemplateToPdfCommand templateToPdfCommand)
        {
            string template;

            using (var webClient = new WebClient())
            {
                template = webClient.DownloadString(testURL);
            }

            return template;
        }
    }
}
