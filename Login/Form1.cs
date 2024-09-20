using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;

            #region "Size"
            this.MinimumSize = new Size(800, 500); // default
            this.MaximumSize = new Size(800, 500);
            this.FormBorderStyle = FormBorderStyle.Sizable; // change size
            this.DoubleBuffered = true;
            #endregion

            #region "Font"
            lblEmail.ForeColor = ColorTranslator.FromHtml("#4A2574");
            lblPassword.ForeColor = ColorTranslator.FromHtml("#4A2574");
            txtEmail.ForeColor = ColorTranslator.FromHtml("#4A2574");
            txtPassword.ForeColor = ColorTranslator.FromHtml("#4A2574");
            txtEmail.BackColor = ColorTranslator.FromHtml("#E2DDFF");
            txtPassword.BackColor = ColorTranslator.FromHtml("#E2DDFF");
            lblErrorMessage.ForeColor = ColorTranslator.FromHtml("#87101E");

            lblRegister.Font = new Font(lblRegister.Font, FontStyle.Italic);

            account_now.Font = new Font(lblRegister.Font, FontStyle.Bold | FontStyle.Italic);

            lblForgotPassword.Font = new Font(lblForgotPassword.Font, FontStyle.Italic);


            this.Load += new EventHandler(Form1_Load);



            #endregion

            #region "button login"
            // event màu gradient cho btnLogin

            this.btnLogin.Paint += btnLogin_Paint;
            this.btnLogin.MouseEnter += btnLogin_MouseEnter;
            this.btnLogin.MouseLeave += btnLogin_MouseLeave;
            // click effective

            #endregion

            #region "icon default"
            string eyeOpenIconPath = @"image\eye.png"; 

            btnTogglePassword.Image = Image.FromFile(eyeOpenIconPath);
            btnTogglePassword.ImageAlign = ContentAlignment.MiddleCenter;
            btnTogglePassword.Size = new Size(31, 27);
            #endregion


        }

        #region "font"
        private PrivateFontCollection privateFonts = new PrivateFontCollection();

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                CustomizeControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tùy chỉnh giao diện: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string fontPath = @"font\Inter_18pt-Regular.ttf";

           
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(fontPath);

            Font interFont = new Font(pfc.Families[0], 10f, FontStyle.Regular); 

      
            lblTitle.Font = new Font(pfc.Families[0], lblTitle.Font.Size, lblTitle.Font.Style);
            lblEmail.Font = new Font(pfc.Families[0], lblEmail.Font.Size, lblEmail.Font.Style);
            lblPassword.Font = new Font(pfc.Families[0], lblPassword.Font.Size, lblPassword.Font.Style);
            btnLogin.Font = new Font(pfc.Families[0], btnLogin.Font.Size, btnLogin.Font.Style);
            lblErrorMessage.Font = new Font(pfc.Families[0], lblErrorMessage.Font.Size, lblErrorMessage.Font.Style);
            lblRegister.Font = new Font(pfc.Families[0], lblRegister.Font.Size, lblRegister.Font.Style);
            lblForgotPassword.Font = new Font(pfc.Families[0], lblForgotPassword.Font.Size, lblForgotPassword.Font.Style);
            account_now.Font = new Font(pfc.Families[0], account_now.Font.Size, account_now.Font.Style);
            btnTogglePassword.Font = new Font(pfc.Families[0], btnTogglePassword.Font.Size, btnTogglePassword.Font.Style);
        }



        #endregion

        #region "right"
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

            // màu gradient
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(108, 139, 197),
                Color.FromArgb(88, 28, 131),  
                102.92f)) // Góc gradient
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        #endregion


        #region "left"
        private void CustomizeControls()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(CustomizeControls));
                return;
            }

            SafeCustomize(txtEmail, CustomizeTextBox);
            SafeCustomize(txtPassword, CustomizeTextBox);
            SafeCustomize(btnLogin, CustomizeButton);
            SafeCustomize(btnTogglePassword, CustomizeToggleButton);
        }

        private void SafeCustomize<T>(T control, Action<T> customizeAction) where T : Control
        {
            if (control != null && !control.IsDisposed)
            {
                customizeAction(control);
            }
        }

        private void CustomizeTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.None;
            textBox.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox.Width, textBox.Height, 8, 8));
   
        }

        private void CustomizeButton(Button button)
        {
            button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 8, 8));
        }

        private void CustomizeToggleButton(Button button)
        {
            button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 8, 8));
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
       
            if (txtPassword.Text == "123")
            {
                lblErrorMessage.Visible = false;
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblErrorMessage.Visible = true; 
            }
        }

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            // Toggle ẩn/hiện
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;

            string eyeOpenIconPath = @"image\eye.png"; 
            string eyeClosedIconPath = @"image\hidden.png"; 
            btnTogglePassword.Image = Image.FromFile(eyeOpenIconPath);

            if (txtPassword.UseSystemPasswordChar)
            {
                btnTogglePassword.Image = Image.FromFile(eyeOpenIconPath);
            }
            else
            {

                btnTogglePassword.Image = Image.FromFile(eyeClosedIconPath);
            }

            btnTogglePassword.ImageAlign = ContentAlignment.MiddleCenter;
            btnTogglePassword.Size = new Size(31, 27); 
        }


       
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false; 
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg); 
            }
        }

        #region "button login (color)"
        private bool isMouseOverButton = false;
        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            isMouseOverButton = true;
            btnLogin.Invalidate(); 
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            isMouseOverButton = false;
            btnLogin.Invalidate(); 
        }

        private void btnLogin_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            Color startColor = isMouseOverButton
                ? ColorTranslator.FromHtml("#6C8BC5") 
                : ColorTranslator.FromHtml("#D69BFD"); 

            Color endColor = isMouseOverButton
                ? ColorTranslator.FromHtml("#581C83") 
                : ColorTranslator.FromHtml("#9672FF"); 

            using (LinearGradientBrush brush = new LinearGradientBrush(
                btn.ClientRectangle,
                startColor,
                endColor,
                102.92f)) 
            {
                e.Graphics.FillRectangle(brush, btn.ClientRectangle);
            }

            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, btn.ClientRectangle, btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
        #endregion



        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
      
        }


        private void lblRegister_Click(object sender, EventArgs e)
        {
    
        }


        private void account_now_Click(object sender, EventArgs e)
        {
       
        }

        #endregion

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }
    }
}
