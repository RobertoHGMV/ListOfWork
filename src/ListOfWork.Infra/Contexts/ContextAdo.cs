using ListOfWork.Shared;
using System.Data.SqlClient;

namespace ListOfWork.Infra.Contexts
{
    public class ContextAdo
    {
        public ContextAdo()
        {
            Connection = new SqlConnection(Runtime.ConnectString1);
        }

        public SqlConnection Connection { get; private set; }
        public SqlConnection Connection1 { get; set; }

        public bool IsOpen() => Connection.State == System.Data.ConnectionState.Open;
        public bool IsClose() => Connection.State == System.Data.ConnectionState.Closed;

        public void Open()
        {
            if (!IsOpen())
                Connection.Open();
        }

        public void Close()
        {
            if (!IsClose())
                Connection.Close();
        }
    }
}
