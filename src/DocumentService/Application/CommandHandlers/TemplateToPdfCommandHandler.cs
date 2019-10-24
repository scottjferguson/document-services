using DocumentService.Application.Commands;
using DocumentService.Application.Components;
using DocumentService.Domain.Entities;
using DocumentService.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentService.Application.CommandHandlers
{
    public class TemplateToPdfCommandHandler : IRequestHandler<TemplateToPdfCommand, TemplateToPdfCommandResult>
    {
        private readonly ITemplateProvider _templateProvider;
        private readonly ITemplateCompiler _templateCompiler;
        private readonly IPdfRenderer _pdfRenderer;
        private readonly IParser _parser;
        private readonly ISaleRepository _saleRepository;

        public TemplateToPdfCommandHandler(
            ITemplateProvider templateProvider,
            ITemplateCompiler templateCompiler,
            IPdfRenderer pdfRenderer,
            IParser parser,
            ISaleRepository saleRepository)
        {
            _templateProvider = templateProvider;
            _templateCompiler = templateCompiler;
            _pdfRenderer = pdfRenderer;
            _parser = parser;
            _saleRepository = saleRepository;
        }

        public async Task<TemplateToPdfCommandResult> Handle(TemplateToPdfCommand request, CancellationToken cancellationToken)
        {
            string template = _templateProvider.GetTemplate(request);

            TemplateMetadataEntity templateMetadataEntity = _parser.ParseTemplate(template);

            foreach (TokenMapEntity tokenMapEntity in templateMetadataEntity.TokenMapEntities)
            {
                DataTable dataTable = _saleRepository.QueryView(tokenMapEntity.ViewName, request.Id);

                Dictionary<string, string> data = _parser.CreateDynamicMap(tokenMapEntity, dataTable.Rows[0]);

                template = _templateCompiler.Compile(template, data);
            }

            Stream stream = _pdfRenderer.RenderHtml(template);

            return new TemplateToPdfCommandResult
            {
                Stream = stream
            };
        }
    }
}
