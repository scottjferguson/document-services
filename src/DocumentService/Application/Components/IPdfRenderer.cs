using System.IO;

namespace DocumentService.Application.Components
{
    public interface IPdfRenderer
    {
        Stream RenderHtml(string html);
    }
}
