using DocumentService.Application.Commands;

namespace DocumentService.Application.Components
{
    public interface ITemplateProviderComponent
    {
        string GetTemplate(TemplateToPdfCommand templateToPdfCommand);
    }
}
