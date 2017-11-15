using ListOfWork.Shared.Command;
using ListOfWork.Domain.Repositories;
using ListOfWork.Shared.Helpers;
using ListOfWork.Domain.ValueObjects;
using ListOfWork.Domain.Models;
using ListOfWork.Domain.Commands.UserCommands;
using System;
using ListOfWork.Shared;

namespace ListOfWork.Domain.Commands.UserCommand
{
    public class UserHandler : Notifiable, 
        IHandler<AddUserCommand>,
        IHandler<UpdateUserCommand>,
        IHandler<RemoveUserCommand>,
        IHandler<LoginUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handler(AddUserCommand command)
        {
            if (_userRepository.Exist(command.UserName))
            {
                AddNotification("User", "Já existe usuário cadastro com nome de usuário informado");
                return;
            }

            if (!command.Password.Equals(command.ConfirmPassword))
                AddNotification("User", "Senha e confirmação de senha não coincidem");

            var name = new Name(command.FirstName, command.LastName);
            var login = new Login(command.UserName, command.Password);
            var user = new User(name, login);

            AddNotifications(user.Notifications);

            if (IsValid())
                _userRepository.Save(user);
        }

        public void Handler(UpdateUserCommand command)
        {
            var user = _userRepository.Get(command.UserName);

            if (user == null)
            {
                AddNotification("User", "Usuário não encontrado");
                return;
            }

            if (!command.Password.Equals(command.ConfirmPassword))
                AddNotification("User", "Senha e confirmação de senha não coincidem");

            var name = new Name(command.FirstName, command.LastName);
            var login = new Login(command.UserName, command.Password);
            user.Update(name, login);

            AddNotifications(user.Notifications);

            if (IsValid())
                _userRepository.Update(user);
        }

        public void Handler(RemoveUserCommand command)
        {
            var user = _userRepository.Get(command.UserName);

            if (user == null)
            {
                AddNotification("User", "Usuário não encontrado");
                return;
            }

            _userRepository.Remove(user);
        }

        public void Handler(LoginUserCommand command)
        {
            var user = _userRepository.Get(command.UserName);
            if (user == null)
            {
                AddNotification("User", "Usuário não encontrado");
                return;
            }

            if (command.Password != user.Login.Password)
                AddNotification("User", "Senha e confirmação de senha não coincidem");
            else
            {
                UserInfo.UserId = user.Id;
                UserInfo.UserName = user.Login.UserName;
            }
        }

        public User GetUser(string userName) => _userRepository.Get(userName);
    }
}
