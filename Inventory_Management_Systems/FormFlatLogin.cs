using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Domain;
using Common.Cache;

namespace Presentation
{
    public partial class FormFlatLogin : Form
    {
        public FormFlatLogin()
        {
            InitializeComponent();
            CustomizeComponents();
        }
        #region "Form behaviors"
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion
        #region "drag form"
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        #endregion
        #region "Customize control"
            private void CustomizeComponents()
        {
            txtUser.AutoSize = false;// thuôc tính này bị ẩn nên không hiện ở chế độ intellsense và không xuất hiện trong bảng thuộc tính, xem thêm trên stackoverlow
            txtUser.Size = new Size(350, 35);
            txtUser.TextAlign = HorizontalAlignment.Center;
            txtPassword.AutoSize = false;
            txtPassword.Size = new Size(350, 35);
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.TextAlign = HorizontalAlignment.Center;
        }
        #endregion

        private void panelTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FormFlatLogin_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
                if (txtUser.Text != "UserName")
                {
                    if (txtPassword.Text != "Password")
                    {
                        UserModel user = new UserModel();
                        var validLogin = user.LoginUser(txtUser.Text, txtPassword.Text);
                        if (validLogin == true)
                        {
                            Form1 mainFrame = new Form1();
                            mainFrame.Show();
                            MessageBox.Show("Welcome " + UserLoginCache.FirstName + "," + UserLoginCache.LastName);
                            mainFrame.FormClosed += MainFrame_FormClosed;//create method when FormClosed event happen
                            this.Hide();       // operator "+=" explain: Method is excute when happend event FormClose"
                        }
                        else
                        {
                            errorMessage("Incorrect user or password entered.\n Please try again!");
                            txtUser.Clear();
                            txtPassword.Clear();
                            txtUser.Focus();
                        }
                    }
                    else
                        errorMessage("Please enter password");
                }
                else errorMessage("Please enter users");         

        }

        private void MainFrame_FormClosed(object sender, FormClosedEventArgs e)
        {
            txtPassword.Clear();
            txtUser.Clear();
            lblErrorMessage.Visible = false;
            this.Show();
            txtUser.Focus();
        }

        private void errorMessage(string msg)
        {
            lblErrorMessage.Text = " " + msg;
            lblErrorMessage.Visible = true;
        }

        private void linkPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var recoverPassword = new FormRecoverPassword();
            recoverPassword.ShowDialog();
        }
    }
}
