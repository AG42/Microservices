using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInformation.DataLayer.Interfaces
{
    public interface IDatalakeEntities
    {
        string ConnectionString { get; set; }
        IEnumerable<T> Get<T>(string tableName, bool isTransactionalDataRequire = false) where T : class, new();

        IEnumerable<T> Where<T>(string tableName, string condition, bool isTransactionalDataRequire = false) where T : class, new();
    }
}
