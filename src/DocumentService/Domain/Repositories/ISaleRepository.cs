using System.Data;

namespace DocumentService.Domain.Repositories
{
    public interface ISaleRepository
    {
        DataTable QueryView(string viewName, string id);
    }
}
