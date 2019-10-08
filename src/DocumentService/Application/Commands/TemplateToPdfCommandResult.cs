using MediatR;
using System.IO;

namespace DocumentService.Application.Commands
{
    public class TemplateToPdfCommandResult : IRequest<TemplateToPdfCommand>
    {
        public Stream Stream { get; set; }
    }
}
