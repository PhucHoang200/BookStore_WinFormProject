using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.UserControl_Employee;

namespace GUI
{
    public partial class fDashboardEmployee : Form
    {
        public fDashboardEmployee()
        {
            InitializeComponent();
            UC_HomeEmployee uC_HomeEmployee = new UC_HomeEmployee();
            AddControlsToPanel(uC_HomeEmployee);
        }

        private void moveSidePanel(Control btn)
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }

        private void AddControlsToPanel(Control c)
        {
            c.Dock = DockStyle.Fill;
            panelControls.Controls.Clear();
            panelControls.Controls.Add(c);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnHome);
            UC_HomeEmployee uC_HomeEmployee=new UC_HomeEmployee();
            AddControlsToPanel(uC_HomeEmployee );
        }

        private void btnSach_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnSach);
            UC_SachEmployee uC_SachEmployee=new UC_SachEmployee();
            AddControlsToPanel(uC_SachEmployee );
        }

        private void btnDonhang_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnDonhang);
            UC_DonhangEmployee uC_DonhangEmployee=new UC_DonhangEmployee();
            AddControlsToPanel(uC_DonhangEmployee );
        }

        private void btnKhachhang_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnKhachhang);
            UC_KhachhangEmployee uC_KhachhangEmployee=new UC_KhachhangEmployee();   
            AddControlsToPanel(uC_KhachhangEmployee );
        }

        private void btnThongke_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnThongke);
            UC_ThongkeEmployee uC_ThongkeEmployee=new UC_ThongkeEmployee();
            AddControlsToPanel(uC_ThongkeEmployee );
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            DialogResult res = new DialogResult();
            res = MessageBox.Show("Bạn có chắc muốn thoát?", "Thoát",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                // Tạo và hiển thị lại form Login
                fLogin loginForm = new fLogin(); // Tên form Login
                loginForm.Show();

                // Đóng form hiện tại
                this.Close();
            }
        }
    }
}
