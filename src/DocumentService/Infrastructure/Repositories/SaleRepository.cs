using Core.Data;
using Core.Plugins.Data.Command;
using Core.Plugins.Data.Extensions;
using DocumentService.Domain.Repositories;
using System.Data;

namespace DocumentService.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly IDatabase _database;

        public SaleRepository(IDatabase database)
        {
            _database = database;
        }

        public DataTable QueryView(string viewName, string id)
        {
            DatabaseCommandResult databaseCommandResult =
                _database.BuildCommand()
                    .AddInputParameter("@id", id)
                    .ForSQLQuery($"SELECT * FROM sale.{viewName} WHERE id = @id")
                    .Execute();

            return databaseCommandResult.DataTable;
        }
    }
}
