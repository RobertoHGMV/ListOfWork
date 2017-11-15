using ListOfWork.Domain.Models;
using ListOfWork.Domain.Repositories;
using ListOfWork.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ListOfWork.Infra.Repositories.Ado
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private ContextAdo _context;

        public UserTaskRepository(ContextAdo context)
        {
            _context = context;
        }

        public UserTask Get(int id)
        {
            using (var cmd = new SqlCommand())
            {
                SqlDataReader data = null;

                try
                {
                    cmd.Connection = _context.Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM [ListOfWork]..[UserTask] WHERE [Id] = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    _context.Open();

                    data = cmd.ExecuteReader();

                    if (data.HasRows)
                    {
                        data.Read();

                        var userTask = new UserTask(
                            id,
                            new Guid(data["UserId"].ToString()),
                            data["UserName"].ToString(),
                            data["Description"].ToString());

                        return userTask;
                    }

                    return null;
                }
                finally
                {
                    if (!data.IsClosed)
                        data.Close();

                    _context.Close();
                }
            }
        }

        public ICollection<UserTask> Get(string description)
        {
            using (var cmd = new SqlCommand())
            {
                SqlDataReader data = null;

                try
                {
                    cmd.Connection = _context.Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT * FROM [ListOfWork]..[UserTask] WHERE [Description] LIKE '%{description}%'";

                    _context.Open();
                    data = cmd.ExecuteReader();
                    var listUserTask = new List<UserTask>();
                    while (data.Read())
                    {
                        var userTask = new UserTask(
                            Convert.ToInt32(data["Id"]),
                            new Guid(data["UserId"].ToString()),
                            data["UserName"].ToString(),
                            data["Description"].ToString());

                        listUserTask.Add(userTask);
                    }

                    return listUserTask;
                }
                finally
                {
                    if (!data.IsClosed)
                        data.Close();

                    _context.Close();
                }
            }
        }

        public DataTable GetAll(string description)
        {
            using (var cmd = new SqlCommand())
            {
                try
                {
                    cmd.Connection = _context.Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT * FROM [ListOfWork]..[UserTask] WHERE [Description] LIKE '%{description}%'";

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    _context.Open();
                    adapter.Fill(table);
                    return table;
                }
                finally
                {
                    _context.Close();
                }
            }
        }

        public void Save(UserTask task)
        {
            using (var cmd = new SqlCommand())
            {
                try
                {
                    cmd.Connection = _context.Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"INSERT INTO [ListOfWork]..[UserTask] ([UserId], [UserName], [Description])
                    VALUES (@userId, @userName, @description)";

                    cmd.Parameters.AddWithValue("@userId", task.UserId);
                    cmd.Parameters.AddWithValue("@userName", task.UserName);
                    cmd.Parameters.AddWithValue("@description", task.Description);

                    _context.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    _context.Close();
                }
            }
        }

        public void Update(UserTask task)
        {
            using (var cmd = new SqlCommand())
            {
                try
                {
                    cmd.Connection = _context.Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"UPDATE [ListOfWork]..[UserTask] SET [Description] = @description WHERE [Id] = @id";

                    cmd.Parameters.AddWithValue("@description", task.Description);
                    cmd.Parameters.AddWithValue("@id", task.Id);

                    _context.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    _context.Close();
                }
            }
        }

        public void Remove(UserTask task)
        {
            using (var cmd = new  SqlCommand())
            {
                try
                {
                    cmd.Connection = _context.Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM [ListOfWork]..[UserTask] WHERE [Id] = @id";
                    cmd.Parameters.AddWithValue("@id", task.Id);
                    _context.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    _context.Close();
                }
            }
        }
    }
}
