using iText.Html2pdf;
using iText.Kernel.Pdf;
using System.IO;

namespace DocumentService.Application.Components.Impl
{
    public class ITextTemplateCompiler : IPdfRenderer
    {
        public Stream RenderHtml(string html)
        {
            var memoryStream = new MemoryStream();

            using (var pdfWriter = new PdfWriter(memoryStream))
            {
                pdfWriter.SetCloseStream(false);
                HtmlConverter.ConvertToPdf(html, pdfWriter);
            }

            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
