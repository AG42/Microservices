using System.Collections.Generic;

namespace CustomerProjectOrder.DataLayer.Interfaces
{
    public interface IDatalakeEntities
    {
        string ConnectionString { get; set; }
        IEnumerable<T> Get<T>(string tableName, bool isTransactionalDataRequire = false) where T : class, new();

        IEnumerable<T> Where<T>(string tableName, string condition, bool isTransactionalDataRequire = false) where T : class, new();
    }
}
