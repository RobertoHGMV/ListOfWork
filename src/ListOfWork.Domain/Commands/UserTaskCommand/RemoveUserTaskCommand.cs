using ListOfWork.Shared.Command;

namespace ListOfWork.Domain.Commands.UserTaskCommand
{
    public class RemoveUserTaskCommand : ICommand
    {
        public int Id { get; set; }
    }
}
