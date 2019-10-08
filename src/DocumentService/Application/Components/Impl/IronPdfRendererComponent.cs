using Core.Framework.Attributes;
using Core.Framework.Enums;
using IronPdf;
using System.IO;

namespace DocumentService.Application.Components.Impl
{
    [Injectable(AutoWiring = Opt.Out)]
    public class IronPdfRendererComponent : IPdfRendererComponent
    {
        public Stream RenderHtml(string html)
        {
            var renderer = new HtmlToPdf();

            PdfDocument pdfDocument = renderer.RenderHtmlAsPdf(html);

            return pdfDocument.Stream;
        }
    }
}
