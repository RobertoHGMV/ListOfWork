using ListOfWork.Shared.Helpers;
using System;

namespace ListOfWork.Domain.Models
{
    public class UserTask : Notifiable
    {
        public UserTask(Guid userId, string userName, string description)
        {
            UserId = userId;
            UserName = userName;
            Description = description;
        }

        public UserTask(int id, Guid userId, string userName, string description)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
            Description = description;
        }

        public int Id { get; private set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; private set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Description))
                AddNotification("Description", "Descrição da tarefa é obrigatório");
        }

        public void Update(string description)
        {
            Description = description;
            Validate();
        }
    }
}
