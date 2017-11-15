using ListOfWork.Domain.Commands.UserCommand;
using ListOfWork.Infra.Contexts;
using ListOfWork.Infra.Repositories.Ado;
using ListOfWork.Shared;
using ListOfWork.Shared.Enums;
using ListOfWork.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ListOfWork.UI.Helpers;

namespace ListOfWork.UI.Forms
{
    public partial class RegisterUserForm : MetroFramework.Forms.MetroForm
    {
        private readonly UserHandler _handler;
        private readonly EStatusScreen _status;

        public RegisterUserForm(EStatusScreen status)
        {
            InitializeComponent();

            _status = status;
            _handler = new UserHandler(new UserRepository(new ContextAdo()));

            SetEnableButtons();
            if (StatusScreen.IsUpdate(_status))
                Get();
        }

        private void SetEnableButtons() => btnSave.Enabled = !StatusScreen.IsRemove(_status);

        #region Crud
        private void Get()
        {
            if (StatusScreen.IsAdd(_status))
                return;

            var user = _handler.GetUser(UserInfo.UserName);

            txtFirstName.Text = user.Name.FirstName;
            txtLastName.Text = user.Name.LastName;
            txtUserName.Text = user.Login.UserName;
            txtPassword.Text = user.Login.Password;
            txtConfirmPassword.Text = user.Login.Password;
        }

        private void Save()
        {
            try
            {
                var command = new AddUserCommand();
                command.FirstName = txtFirstName.Text;
                command.LastName = txtLastName.Text;
                command.UserName = txtUserName.Text;
                command.Password = txtPassword.Text;
                command.ConfirmPassword = txtConfirmPassword.Text;

                if (command.Password != command.ConfirmPassword)
                    _handler.Notifications.Add(new Notification("User", "Senha e confirmação de senha não coincidem"));

                _handler.Handler(command);

                if (_handler.IsValid())
                {
                    MessageSuccess("Operação realizada com sucesso");
                    CleanFields();
                }
                else
                    MessageError(CreateMsg(_handler.Notifications));
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private new void Update()
        {
            try
            {
                var command = new UpdateUserCommand();
                command.FirstName = txtFirstName.ToString();
                command.LastName = txtLastName.ToString();
                command.UserName = txtUserName.ToString();
                command.Password = txtPassword.ToString();
                command.ConfirmPassword = txtConfirmPassword.ToString();

                if (command.Password != command.ConfirmPassword)
                    _handler.Notifications.Add(new Notification("User", "Senha e confirmação de senha não coincidem"));

                _handler.Handler(command);

                if (_handler.IsValid())
                {
                    CleanFields();
                    MessageSuccess("Operação realizada com sucesso");
                }
                else
                    MessageError(CreateMsg(_handler.Notifications));
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void Remove()
        {
            try
            {
                var command = new RemoveUserCommand();
                command.UserName = txtUserName.ToString();

                _handler.Handler(command);

                if (_handler.IsValid())
                    MessageSuccess("Operação realizada com sucesso");
                else
                    MessageError(CreateMsg(_handler.Notifications));
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }
        #endregion

        #region Botões
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (StatusScreen.IsAdd(_status))
                    Save();
                else
                    Update();
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void CleanFields()
        {
            txtFirstName.Text =
                txtLastName.Text =
                    txtUserName.Text =
                        txtPassword.Text =
                            txtConfirmPassword.Text = string.Empty;
        }
        #endregion

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
    }
}
