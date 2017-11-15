using ListOfWork.Domain.Models;

namespace ListOfWork.Domain.Repositories
{
    public interface IUserRepository
    {
        bool Exist(string userName);
        User Get(string userName);
        void Save(User user);
        void Update(User user);
        void Remove(User user);
    }
}
