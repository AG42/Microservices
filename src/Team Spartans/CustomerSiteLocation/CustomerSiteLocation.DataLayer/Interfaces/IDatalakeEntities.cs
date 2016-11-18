using System.Collections.Generic;

namespace CustomerSiteLocation.DataLayer.Interfaces
{
    public interface IDatalakeEntities
    {
        string ConnectionString { get; set; }
        IEnumerable<T> Get<T>(string tableName,string companyCode, bool isTransactionalDataRequire = false) where T : class, new();

        IEnumerable<T> Where<T>(string tableName, string condition, string companyCode, bool isTransactionalDataRequire = false) where T : class, new();
    }
}
