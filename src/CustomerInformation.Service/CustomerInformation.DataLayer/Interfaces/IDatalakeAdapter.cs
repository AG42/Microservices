using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInformation.DataLayer.Interfaces
{
    public interface IDatalakeAdapter
    {
        string ConnectionString { get; set; }

        IEnumerable<T> Get<T>(string query) where T : class, new();
    }
}
