using ListOfWork.Shared.Command;

namespace ListOfWork.Domain.Commands.UserCommand
{
    public class UpdateUserCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
