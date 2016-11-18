using System.Data;

namespace Microservices.Datalake
{
    interface IDatalakeAdapter
    {
        DataSet Execute(string query);
        int ExecuteNonQuery(string query);
    }
}
