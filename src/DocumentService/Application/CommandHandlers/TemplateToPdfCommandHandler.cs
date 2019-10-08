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
        private readonly ITemplateProviderComponent _templateProviderComponent;
        private readonly ITemplateCompilerComponent _templateCompilerComponent;
        private readonly IPdfRendererComponent _pdfRendererComponent;
        private readonly IParserComponent _parserComponent;
        private readonly ISaleRepository _saleRepository;

        public TemplateToPdfCommandHandler(
            ITemplateProviderComponent templateProviderComponent,
            ITemplateCompilerComponent templateCompilerComponent,
            IPdfRendererComponent pdfRendererComponent,
            IParserComponent parserComponent,
            ISaleRepository saleRepository)
        {
            _templateProviderComponent = templateProviderComponent;
            _templateCompilerComponent = templateCompilerComponent;
            _pdfRendererComponent = pdfRendererComponent;
            _parserComponent = parserComponent;
            _saleRepository = saleRepository;
        }

        public async Task<TemplateToPdfCommandResult> Handle(TemplateToPdfCommand request, CancellationToken cancellationToken)
        {
            string template = _templateProviderComponent.GetTemplate(request);

            TemplateMetadataEntity templateMetadataEntity = _parserComponent.ParseTemplate(template);

            foreach (TokenMapEntity tokenMapEntity in templateMetadataEntity.TokenMapEntities)
            {
                DataTable dataTable = _saleRepository.QueryView(tokenMapEntity.ViewName, request.Id);
                
                Dictionary<string, string> data = _parserComponent.CreateDynamicMap(tokenMapEntity, dataTable.Rows[0]);

                template = _templateCompilerComponent.Compile(template, data);
            }

            Stream stream = _pdfRendererComponent.RenderHtml(template);

            return new TemplateToPdfCommandResult
            {
                Stream = stream
            };
        }
    }
}
