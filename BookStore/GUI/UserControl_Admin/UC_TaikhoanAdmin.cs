using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.UserControl_Admin
{
    public partial class UC_TaikhoanAdmin : UserControl
    {
        TaiKhoanBUS taiKhoanBUS = new TaiKhoanBUS();
        VaiTroBUS vaiTroBUS = new VaiTroBUS();
        private string Id;

        public UC_TaikhoanAdmin()
        {
            InitializeComponent();
            HienThiDS_VaiTro();
            List<TaiKhoan> ds_TaiKhoan = taiKhoanBUS.GetAllTaiKhoan();
            hienThiDS_TaiKhoan(ds_TaiKhoan);
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string matKhau = txtMatKhau.Text;
            int maVaiTro = int.Parse(cbbTenVaiTro.SelectedValue.ToString());
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !!!");
            }
            else
            {
                if (KiemTraEmail(email))
                {
                    string thongbao = taiKhoanBUS.AddTaiKhoan(email, matKhau, maVaiTro);

                    MessageBox.Show(thongbao);
                    if (thongbao == "Thêm tài khoản thành công!")
                    {
                        List<TaiKhoan> ds_TaiKhoan = taiKhoanBUS.GetAllTaiKhoan();
                        hienThiDS_TaiKhoan(ds_TaiKhoan);
                        XoaDulieu();
                    }
                }
                else
                {
                    MessageBox.Show("Mail không đúng định dạng !!!");
                }
            }
        }

        public bool KiemTraEmail(string email)
        {

            // Biểu thức chính quy kiểm tra email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string matKhau = txtMatKhau.Text;
            int maVaiTro = int.Parse(cbbTenVaiTro.SelectedValue.ToString());
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !!!");
            }
            else
            {
                if (KiemTraEmail(email))
                {
                    if (Id == null)
                    {
                        MessageBox.Show("Chọn tài khoản cần sửa thông tin");
                    }
                    else
                    {
                        int MaTK = int.Parse(Id);
                        string thongbao = taiKhoanBUS.UpdateTaiKhoan(MaTK, email, maVaiTro);
                        MessageBox.Show(thongbao);

                        if (thongbao == "Cập nhật thông tin tài khoản thành công")
                        {
                            List<TaiKhoan> ds_TaiKhoan = taiKhoanBUS.GetAllTaiKhoan();
                            hienThiDS_TaiKhoan(ds_TaiKhoan);
                            XoaDulieu();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Mail không đúng định dạng !!!");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            XoaDulieu();
            HienThiDS_VaiTro();
            List<TaiKhoan> ds_TaiKhoan = taiKhoanBUS.GetAllTaiKhoan();
            hienThiDS_TaiKhoan(ds_TaiKhoan);
        }

        public void XoaDulieu()
        {
            txtEmail.Clear();
            txtMatKhau.Clear();
            txtTenNhanVien.Clear();
            txtTimkiemNV.Clear();
            cbbTenVaiTro.SelectedValue = 0;
        }

        private void btnTimkiemTK_Click(object sender, EventArgs e)
        {
            string TimKiem = txtTimkiemNV.Text;

            if (TimKiem == "")
            {
                MessageBox.Show("Khung nhập không được để trống");
            }
            else
            {


                var taiKhoan = taiKhoanBUS.FindTaiKhoanByNameOfEmployee(TimKiem);

                if (taiKhoan == null)
                {
                    MessageBox.Show("Không có dữ liệu cần tìm.");
                }
                else
                {
                    hienThiDS_TaiKhoan(taiKhoan);
                }

            }
        }

        private void datagridviewTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = datagridviewTaiKhoan.Rows[e.RowIndex];
                Id = row.Cells["MaTaiKhoan"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
                txtTenNhanVien.Text = row.Cells["TenNhanVien"].Value.ToString();
                cbbTenVaiTro.SelectedValue = int.Parse(row.Cells["IdVaiTro"].Value.ToString());
            }
        }

        public void HienThiDS_VaiTro()
        {
            cbbTenVaiTro.DataSource = vaiTroBUS.GetAllVaiTro();
            cbbTenVaiTro.DisplayMember = "TenVaiTro";
            cbbTenVaiTro.ValueMember = "Id";
        }

        public void hienThiDS_TaiKhoan(List<TaiKhoan> ds_TaiKhoan)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaTaiKhoan");
            dt.Columns.Add("Email");
            dt.Columns.Add("MatKhau");
            dt.Columns.Add("IdVaiTro");
            dt.Columns.Add("VaiTro");
            dt.Columns.Add("TenNhanVien");


            foreach (var tk in ds_TaiKhoan)
            {
                dt.Rows.Add(
                    tk.Id,
                    tk.Email,
                    Convert.ToBase64String(tk.MatKhau),
                    tk.IdVaiTro,
                    tk.VaiTro.TenVaiTro,
                    string.Join(", ", tk.NhanViens.Select(i => i.HoTenNV))
                );
            }

            // Gán DataTable cho DataGridView
            datagridviewTaiKhoan.DataSource = dt;

            // Tùy chỉnh hiển thị DataGridView (nếu cần)
            datagridviewTaiKhoan.Columns["MaTaiKhoan"].HeaderText = "Mã Tài khoản";
            datagridviewTaiKhoan.Columns["Email"].HeaderText = "Email";
            datagridviewTaiKhoan.Columns["MatKhau"].HeaderText = "Mật khẩu";
            datagridviewTaiKhoan.Columns["VaiTro"].HeaderText = "Vai trò";
            datagridviewTaiKhoan.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";

            datagridviewTaiKhoan.Columns["IdVaiTro"].Visible = false;
        }

    }
}
