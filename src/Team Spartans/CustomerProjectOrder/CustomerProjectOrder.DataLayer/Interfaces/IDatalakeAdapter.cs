using System.Collections.Generic;

namespace CustomerProjectOrder.DataLayer.Interfaces
{
    public interface IDatalakeAdapter
    {
        string ConnectionString { get; set; }

        IEnumerable<T> Get<T>(string query) where T : class, new();
    }
}
