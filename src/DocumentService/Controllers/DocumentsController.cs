using DocumentService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{language}/{documentType}/{id}")]
        public ActionResult Get(string language, string documentType, string id)
        {
            var command = new TemplateToPdfCommand
            {
                Language = language,
                DocumentType = documentType,
                Id = id
            };

            var result = _mediator.Send(command);

            return new FileStreamResult(result.Result.Stream, "application/pdf");
        }
    }
}
