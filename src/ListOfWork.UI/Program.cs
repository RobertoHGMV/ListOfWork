using ListOfWork.Infra.Contexts;
using ListOfWork.Infra.Mappings;
using ListOfWork.UI.Forms;
using System;
using System.Windows.Forms;

namespace ListOfWork.UI
{
    static class Program
    {
        public static readonly ContextAdo ContextAdo = new ContextAdo();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CreateDatabase();

            var loginForm = new LoginForm();
            loginForm.ShowDialog();

            if (loginForm.DialogResult == DialogResult.OK)
                Application.Run(new TaskManagerForm());
        }

        private static void CreateDatabase()
        {
            var mapAdo = new MapAdo(ContextAdo);
            mapAdo.CreateDatabaseIfNotExist();
        }
    }
}
