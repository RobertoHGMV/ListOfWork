using ListOfWork.Domain.Models;
using ListOfWork.Domain.Repositories;
using ListOfWork.Shared.Command;
using ListOfWork.Shared.Helpers;
using System.Data;

namespace ListOfWork.Domain.Commands.UserTaskCommand
{
    public class UserTaskHandler : Notifiable,
        IHandler<AddUserTaskCommand>,
        IHandler<UpdateUserTaskCommand>,
        IHandler<RemoveUserTaskCommand>
    {
        private readonly IUserTaskRepository _userTaskRepository;

        public UserTaskHandler(IUserTaskRepository userTaskRepository)
        {
            _userTaskRepository = userTaskRepository;
        }

        public void Handler(AddUserTaskCommand command)
        {
            var userTask = new UserTask(command.Userid, command.UserName, command.Description);
            AddNotifications(userTask.Notifications);

            if (IsValid())
                _userTaskRepository.Save(userTask);
        }

        public void Handler(UpdateUserTaskCommand command)
        {
            var userTask = _userTaskRepository.Get(command.Id);
            if (userTask == null)
            {
                AddNotification("UserTask", "Tarefa não encontrada");
                return;
            }

            userTask.Update(command.Description);

            AddNotifications(userTask.Notifications);

            if (IsValid())
                _userTaskRepository.Update(userTask);
        }

        public void Handler(RemoveUserTaskCommand command)
        {
            var userTask = _userTaskRepository.Get(command.Id);
            if (userTask == null)
            {
                AddNotification("UserTask", "Tarefa não encontrada");
                return;
            }

            _userTaskRepository.Remove(userTask);
        }

        public DataTable GetAll(string description = "")
        {
            return _userTaskRepository.GetAll(description);
        }
    }
}
