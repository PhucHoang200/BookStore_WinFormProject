using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.UserControl_Admin;

namespace GUI
{
    public partial class fDashboardAdmin : Form
    {
        public fDashboardAdmin()
        {
            InitializeComponent();
            UC_HomeAdmin uC_HomeAdmin = new UC_HomeAdmin();
            AddControlsToPanel(uC_HomeAdmin);
        }

        private void moveSidePanel(Control btn)
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }

        private void AddControlsToPanel(Control c)
        {
            c.Dock= DockStyle.Fill;
            panelControls.Controls.Clear();
            panelControls.Controls.Add(c);
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

        private void btnHome_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnHome);
            UC_HomeAdmin uC_HomeAdmin = new UC_HomeAdmin();
            AddControlsToPanel(uC_HomeAdmin);
        }

        private void btnSach_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnSach);
            UC_SachAdmin uC_SachAdmin = new UC_SachAdmin();
            AddControlsToPanel(uC_SachAdmin);
        }

        private void btnDonhang_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnDonhang);
            UC_QLDonhangAdmin uC_QLDonhangAdmin = new UC_QLDonhangAdmin();
            AddControlsToPanel(uC_QLDonhangAdmin);
        }

        private void btnKhachhang_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnKhachhang);
            UC_KhachhangAdmin uC_KhachhangAdmin = new UC_KhachhangAdmin();
            AddControlsToPanel(uC_KhachhangAdmin);
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnNhanVien);
            UC_NhanvienAdmin uC_NhanvienAdmin = new UC_NhanvienAdmin(); 
            AddControlsToPanel(uC_NhanvienAdmin);
        }

        private void btnTaikhoan_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnTaikhoan);
            UC_TaikhoanAdmin uC_TaikhoanAdmin = new UC_TaikhoanAdmin(); 
            AddControlsToPanel(uC_TaikhoanAdmin);
        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnKho);
            UC_KhoAdmin uC_KhoAdmin = new UC_KhoAdmin();
            AddControlsToPanel(uC_KhoAdmin);
        }

        private void btnThongke_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnThongke);
            UC_ThongkeAdmin uC_ThongkeAdmin = new UC_ThongkeAdmin();
            AddControlsToPanel(uC_ThongkeAdmin);
        }

    }
}
