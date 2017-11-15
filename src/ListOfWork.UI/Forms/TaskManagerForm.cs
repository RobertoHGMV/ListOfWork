using ListOfWork.Domain.Commands.UserTaskCommand;
using ListOfWork.Domain.Models;
using ListOfWork.Infra.Contexts;
using ListOfWork.Infra.Repositories.Ado;
using ListOfWork.Shared;
using ListOfWork.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ListOfWork.UI.Forms
{
    public partial class TaskManagerForm : MetroFramework.Forms.MetroForm
    {
        private readonly UserTaskHandler _handler;

        public TaskManagerForm()
        {
            InitializeComponent();

            _handler = new UserTaskHandler(new UserTaskRepository(new ContextAdo()));
            FillGrid();
            FormatGrid();
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

        private void SetCursor(Cursor cursor) => this.Cursor = cursor;

        private void FormatGrid()
        {
            //DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
            //{
            //    columnCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //    columnCheck.FlatStyle = FlatStyle.Standard;
            //    columnCheck.CellTemplate.Style.BackColor = Color.Beige;
            //}
            //gridTasks.Columns.Insert(0, columnCheck);

            gridTasks.Columns["UserId"].Visible =
                gridTasks.Columns["UserName"].Visible = false;

            gridTasks.Columns["Id"].HeaderText = "Código";
            gridTasks.Columns["Description"].HeaderText = "Descrição";

            gridTasks.Font = new Font("Verdana", 10, FontStyle.Bold);
            gridTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void FillGrid()
        {
            var tasks = _handler.GetAll();
            gridTasks.DataSource = tasks;
            gridTasks.Refresh();
        }

        public DataTable ConvertToDataTable(List<UserTask> tasks)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("UserId", typeof(Guid));
            dataTable.Columns.Add("UserName", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));

            foreach (var task in tasks)
            {
                var row = dataTable.NewRow();
                row["Id"] = task.Id;
                row["UserId"] = task.UserId;
                row["UserName"] = task.UserName;
                row["Description"] = task.Description;
            }

            return dataTable;
        }

        #region Crud
        private void AddOrUpdateTask()
        {
            foreach (DataGridViewRow row in gridTasks.Rows)
            {
                if (row.Cells["Description"].Value == null)
                    continue;

                if (string.IsNullOrEmpty(row.Cells["Id"].Value.ToString()))
                {
                    var command = new AddUserTaskCommand();
                    command.Userid = UserInfo.UserId;
                    command.UserName = UserInfo.UserName;
                    command.Description = row.Cells["Description"].Value.ToString();
                    _handler.Handler(command);
                }
                else
                {
                    var command = new UpdateUserTaskCommand();
                    command.Id = (int)row.Cells["Id"].Value;
                    command.Description = row.Cells["Description"].Value.ToString();
                    _handler.Handler(command);
                }
            }
        }

        private void RemoveTask()
        {
            var row = gridTasks.CurrentRow;
            if (row == null)
                return;

            var command = new RemoveUserTaskCommand();

            command.Id = (int) row.Cells["Id"].Value;
            _handler.Handler(command);
        }
        #endregion

        private void SelectAll()
        {
            foreach (DataGridViewRow row in gridTasks.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell) row.Cells[0];
                //chk.Value = (chk.Value == null ? false : (bool) chk.Value);
                chk.Value = chk.Value ?? true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddOrUpdateTask();
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveTask();
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }
    }
}
