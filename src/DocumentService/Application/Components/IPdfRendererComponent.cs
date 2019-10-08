using System.IO;

namespace DocumentService.Application.Components
{
    public interface IPdfRendererComponent
    {
        Stream RenderHtml(string html);
    }
}
