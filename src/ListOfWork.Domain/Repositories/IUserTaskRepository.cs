using ListOfWork.Domain.Models;
using System.Collections.Generic;
using System.Data;

namespace ListOfWork.Domain.Repositories
{
    public interface IUserTaskRepository
    {
        UserTask Get(int id);
        ICollection<UserTask> Get(string description);
        DataTable GetAll(string description = "");
        void Save(UserTask task);
        void Update(UserTask task);
        void Remove(UserTask task);
    }
}
