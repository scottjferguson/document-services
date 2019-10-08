using DocumentService.Application.Commands;

namespace DocumentService.Application.Components.Impl
{
    public class MockTemplateProviderComponent : ITemplateProviderComponent
    {
        public string GetTemplate(TemplateToPdfCommand templateToPdfCommand)
        {
            return
@"<div class=""entry"">
  <h1>Hello {{vwEnrollmentRequests:First_Name}} {{vwEnrollmentRequests:Last_Name}},</h1>
  <div>
    This is the body
  </div>
</div>";
        }
    }
}
