using DocumentService.Application.Commands;

namespace DocumentService.Application.Components
{
    public interface ITemplateProvider
    {
        string GetTemplate(TemplateToPdfCommand templateToPdfCommand);
    }
}
