namespace ListOfWork.Shared.Command
{
    public interface IHandler<T> where T : ICommand
    {
        void Handler(T command);
    }
}
