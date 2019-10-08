using MediatR;

namespace DocumentService.Application.Commands
{
    public class TemplateToPdfCommand : IRequest<TemplateToPdfCommandResult>
    {
        public string Language { get; set; }

        public string DocumentType { get; set; }

        public string Id { get; set; }
    }
}
