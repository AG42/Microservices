
using System.Collections.Generic;

namespace Microservices.Common.Interface
{
    public interface IDatabase
    {
        string ConnectionString { get; set; }
        IEnumerable<T> Get<T>(string tableName, string column) where T : class, new();

        IEnumerable<T> Where<T>(string tableName, string column, string condition, bool isTransactionalDataRequire = false) where T : class, new();

        IEnumerable<T> GetJoinData<T>(string primaryTableName, string columnName, string JoinConditions, 
                                    bool isTransactionalDataRequire = false) where T : class, new();

        IEnumerable<T> WhereJoin<T>(string primaryTableName, string columnName, string JoinConditions, 
                                    string whereCondition, bool isTransactionalDataRequire = false) where T : class, new();

    }
}
