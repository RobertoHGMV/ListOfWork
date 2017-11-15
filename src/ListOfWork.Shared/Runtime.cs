using System.Configuration;

namespace ListOfWork.Shared
{
    public static class Runtime
    {
        public static string ConnectString = ConfigurationManager.ConnectionStrings["CnnStr"].ConnectionString;
        public static string ConnectString1 = ConfigurationManager.ConnectionStrings["CnnStr1"].ConnectionString;
    }
}
