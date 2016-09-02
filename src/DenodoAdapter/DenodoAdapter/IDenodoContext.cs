using System.Collections.Generic;

namespace DenodoAdapter
{
    public interface IDenodoContext
    {
        List<T> GetData<T>(string viewUri) where T : class;
        T GetData<T>(string viewUri, string id) where T : class;
        bool Insert<T>(string viewUri, T t) where T : class;
        bool Delete(string viewUri, string id);
        bool Update<T>(string viewUri, string id, T t) where T : class;
        List<T> SearchData<T>(string viewUri, string filter) where T : class;
    }
}
