using ListOfWork.Shared.Command;
using System;

namespace ListOfWork.Domain.Commands.UserTaskCommand
{
    public class AddUserTaskCommand : ICommand
    {
        public Guid Userid { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
    }
}
