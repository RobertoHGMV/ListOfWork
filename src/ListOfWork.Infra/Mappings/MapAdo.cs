using ListOfWork.Infra.Contexts;
using ListOfWork.Shared;
using System.Configuration;
using System.Data.SqlClient;

namespace ListOfWork.Infra.Mappings
{
    public class MapAdo
    {
        private ContextAdo _context;

        public MapAdo(ContextAdo context)
        {
            _context = context;
        }

        public void CreateDatabaseIfNotExist()
        {
            if (!ExistDatabase())
            {
                CreateDatabase();
                CreateTableUser();
                CreateTableUserTask();
            }
        }

        private bool ExistDatabase()
        {
            using (var conn = new SqlConnection(Runtime.ConnectString1))
            {
                using (var cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "SELECT 1 FROM sys.databases WHERE name = 'ListOfWork'";
                        conn.Open();
                        var result = cmd.ExecuteScalar();

                        return result != null;
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }

        private void CreateDatabase()
        {
            using (var conn = new SqlConnection(Runtime.ConnectString1))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "CREATE DATABASE [ListOfWork]";
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        private void CreateTableUser()
        {
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = _context.Connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = @"USE [ListOfWork]
                                CREATE TABLE [User] 
                                ([Id] nvarchar(100) primary key not null,
                                [FirstName] nvarchar(100) not null,
                                [LastName] nvarchar(100),
                                [UserName] nvarchar(60) not null,
                                [Password] nvarchar(60) not null)";
                _context.Open();
                cmd.ExecuteNonQuery();
                _context.Close();
            }
        }

        private void CreateTableUserTask()
        {
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = _context.Connection;
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"USE [ListOfWork]
                                CREATE TABLE [UserTask] 
                                ([Id] int primary key identity(1,1) not null,
                                [UserId] nvarchar(100) not null,
                                [UserName] nvarchar(60) not null,
                                [Description] nvarchar(4000) not null,
                                FOREIGN KEY (UserId) REFERENCES [User](Id))";

                _context.Open();
                cmd.ExecuteNonQuery();
                _context.Close();
            }
        }
    }
}
