namespace ListOfWork.Shared.Enums
{
    public class StatusScreen
    {
        public static bool IsAdd(EStatusScreen status) => status == EStatusScreen.Add;

        public static bool IsUpdate(EStatusScreen status) => status == EStatusScreen.Update;

        public static bool IsRemove(EStatusScreen status) => status == EStatusScreen.Remove;
    }
    public enum EStatusScreen
    {
        Add = 1,
        Update = 2,
        Remove = 3
    }
}
