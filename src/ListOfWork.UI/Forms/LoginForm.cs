using ListOfWork.Domain.Commands.UserCommand;
using ListOfWork.Domain.Commands.UserCommands;
using ListOfWork.Infra.Contexts;
using ListOfWork.Infra.Repositories.Ado;
using ListOfWork.Shared.Enums;
using ListOfWork.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ListOfWork.UI.Forms
{
    public partial class LoginForm : MetroFramework.Forms.MetroForm
    {
        private readonly UserHandler _handler;

        public LoginForm()
        {
            InitializeComponent();
            _handler = new UserHandler(new UserRepository(Program.ContextAdo));
        }

        #region Mensagens de aviso
        private string CreateMsg(ICollection<Notification> notifications)
        {
            var error = string.Empty;

            foreach (var notification in notifications)
                error += notification.Message + "\n";

            return error;
        }

        private void MessageError(string message)
        {
            MetroFramework.MetroMessageBox.Show(this, message, "Mensagem do Sistema",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MessageSuccess(string message)
        {
            MetroFramework.MetroMessageBox.Show(this, message, "Mensagem do Sistema",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private DialogResult MessageQuestion(string message)
        {
            return MetroFramework.MetroMessageBox.Show(this, message, "Mensagem do Sistema",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        #endregion


        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var command = new LoginUserCommand();
                command.UserName = txtUserName.Text;
                command.Password = txtPassword.Text;

                _handler.Handler(command);

                if (_handler.IsValid())
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                    MessageError(CreateMsg(_handler.Notifications));
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                var registerUserForm = new RegisterUserForm(EStatusScreen.Add);
                registerUserForm.Show();
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }
    }
}
