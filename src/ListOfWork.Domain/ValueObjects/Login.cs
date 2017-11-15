using ListOfWork.Shared.Helpers;

namespace ListOfWork.Domain.ValueObjects
{
    public class Login : Notifiable
    {
        public Login(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; private set; }
        public string Password { get; private set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(UserName))
                AddNotification("Login", "Nome de usuário é obrigatório");

            if (string.IsNullOrEmpty(Password))
                AddNotification("Password", "Senha é obrigatório");
        }
    }
}
