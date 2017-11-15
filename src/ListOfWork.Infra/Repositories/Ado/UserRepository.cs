using ListOfWork.Domain.Repositories;
using System;
using ListOfWork.Domain.Models;
using System.Data.SqlClient;
using ListOfWork.Infra.Contexts;
using ListOfWork.Domain.ValueObjects;

namespace ListOfWork.Infra.Repositories.Ado
{
    public class UserRepository : IUserRepository
    {
        private ContextAdo _context;

        public UserRepository(ContextAdo context)
        {
            _context = context;
        }

        public bool Exist(string userName)
        {
            using (var cmd = new SqlCommand())
            {
                try
                {
                    cmd.Connection = _context.Connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM [ListOfWork]..[User] WHERE [UserName] = @userName";
                    cmd.Parameters.AddWithValue("@userName", userName);

                    _context.Open();
                    var result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0;
                }
                finally
                {
                    _context.Close();
                }
            }
        }

        public User Get(string userName)
        {
            using (var cmd = new SqlCommand())
            {
                SqlDataReader data = null;

                try
                {
                    cmd.Connection = _context.Connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM [ListOfWork]..[User] WHERE [UserName] = @userName";
                    cmd.Parameters.AddWithValue("@userName", userName);

                    _context.Open();

                    data = cmd.ExecuteReader();
                    if (data.HasRows)
                    {
                        data.Read();
                        var name = new Name(
                            data["FirstName"].ToString(),
                            data["LastName"].ToString());

                        var login = new Login(
                            data["UserName"].ToString(),
                            data["Password"].ToString());

                        return new User(
                            new Guid(data["Id"].ToString()), name, login);
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

        public void Save(User user)
        {
            using (var cmd = new SqlCommand())
            {
                try
                {
                    cmd.Connection = _context.Connection;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = @"INSERT INTO [ListOfWork]..[User] 
                                ([Id], [FirstName], [LastName], [UserName], [Password]) VALUES
                                (@id, @firstname, @lastname, @username, @password)";

                    cmd.Parameters.AddWithValue("@id", user.Id);
                    cmd.Parameters.AddWithValue("@firstname", user.Name.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", user.Name.LastName);
                    cmd.Parameters.AddWithValue("@username", user.Login.UserName);
                    cmd.Parameters.AddWithValue("@password", user.Login.Password);

                    _context.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    _context.Close();
                }
            }
        }

        public void Update(User user)
        {
            using (var cmd = new SqlCommand())
            {
                try
                {
                    cmd.Connection = _context.Connection;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = @"UPDATE [ListOfWork]..[User] SET 
                                    [FirstName] = @firstname, 
                                    [LastName] = @lastname, 
                                    [UserName] = @username, 
                                    [Password] = @password
                                WHERE [UserName] = @username";

                    cmd.Parameters.AddWithValue("@firstname", user.Name.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", user.Name.LastName);
                    cmd.Parameters.AddWithValue("@username", user.Login.UserName);
                    cmd.Parameters.AddWithValue("@password", user.Login.Password);

                    _context.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    _context.Close();
                }
            }
        }

        public void Remove(User user)
        {
            using (var cmd = new SqlCommand())
            {
                try
                {
                    cmd.Connection = _context.Connection;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "DELETE FROM [ListOfWork]..[User] WHERE [UserName] = @username";
                    cmd.Parameters.AddWithValue("@username", user.Login.UserName);

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
