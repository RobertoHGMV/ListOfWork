using ListOfWork.Shared.Command;

namespace ListOfWork.Domain.Commands.UserCommand
{
    public class RemoveUserCommand : ICommand
    {
        public string UserName { get; set; }
    }
}
