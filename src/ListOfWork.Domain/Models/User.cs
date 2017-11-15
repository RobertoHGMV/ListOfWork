using ListOfWork.Domain.ValueObjects;
using ListOfWork.Shared.Helpers;
using System;
using System.Collections.Generic;

namespace ListOfWork.Domain.Models
{
    public class User : Notifiable
    {
        private ICollection<UserTask> _tasks;

        public User(Name name, Login login)
        {
            Name = name;
            Login = login;

            Validate();
        }

        public User(Guid id, Name name, Login login)
        {
            Id = id;
            Name = name;
            Login = login;

            Validate();
        }

        public Guid Id = Guid.NewGuid();
        public Name Name { get; private set; }
        public Login Login { get; private set; }

        public void Validate()
        {
            AddNotifications(Name.Notifications);
            AddNotifications(Login.Notifications);
        }

        public void Update(Name name, Login login)
        {
            Name = name;
            Login = login;

            Validate();
        }
    }
}
