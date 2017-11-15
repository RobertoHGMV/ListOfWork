using ListOfWork.Shared.Command;

namespace ListOfWork.Domain.Commands.UserCommands
{
    public class LoginUserCommand : ICommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
