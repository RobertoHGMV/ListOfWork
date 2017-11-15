using ListOfWork.Shared.Helpers;

namespace ListOfWork.Domain.ValueObjects
{
    public class Name : Notifiable
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            Validate();
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public void Validate()
        {
            if (Validations.IsEmpty(FirstName))
                AddNotification("FirstName", "Nome é obrigatório");

            if (Validations.IsEmpty(FirstName))
                AddNotification("LastName", "Sobrenome é obrigatório");
        }
    }
}
