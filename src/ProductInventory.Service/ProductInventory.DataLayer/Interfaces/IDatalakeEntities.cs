using System.Collections.Generic;

namespace ProductInventory.DataLayer.Interfaces
{
    public interface IDatalakeEntities
    {
        string ConnectionString { get; set; }
        IEnumerable<T> Get<T>(string tableName, bool isTransactionalDataRequire = false) where T : class, new();
        IEnumerable<T> Where<T>(string tableName, string condition, bool isTransactionalDataRequire = false) where T : class, new();
        IEnumerable<T> GetJoinData<T>(string primaryTableName, string JoinConditions, bool isTransactionalDataRequire = false) where T : class, new();
        IEnumerable<T> WhereJoin<T>(string primaryTableName, string JoinConditions, string whereCondition, bool isTransactionalDataRequire = false) where T : class, new();
    }
}
