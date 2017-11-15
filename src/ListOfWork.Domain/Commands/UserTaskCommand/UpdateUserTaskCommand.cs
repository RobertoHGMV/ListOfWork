using ListOfWork.Shared.Command;

namespace ListOfWork.Domain.Commands.UserTaskCommand
{
    public class UpdateUserTaskCommand : ICommand
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
