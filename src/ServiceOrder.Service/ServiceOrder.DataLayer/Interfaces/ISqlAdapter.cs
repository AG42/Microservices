using System.Collections.Generic;

namespace ServiceOrder.DataLayer.Interfaces
{
    public interface ISqlAdapter
    {
        string ConnectionString { get; set; }
        IEnumerable<T> Get<T>(string query) where T : class, new();
        int Insert(string query);
        int Update(string query);
        int Delete(string query);
    }
}
