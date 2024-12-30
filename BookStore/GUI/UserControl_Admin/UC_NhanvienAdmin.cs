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
    public partial class UC_NhanvienAdmin : UserControl
    {
        TaiKhoanBUS tkBUS = new TaiKhoanBUS();
        NhanVienBUS nvBUS = new NhanVienBUS();
        private string Id;

        public UC_NhanvienAdmin()
        {
            InitializeComponent();
            hienThiDSNhanVien();
            hienThiDSTaiKhoan();
        }

        public void hienThiDSNhanVien()
        {
            var nv = nvBUS.GetAllNhanVien();
            datagridviewNV.DataSource = nv;
            datagridviewNV.Columns["Id"].HeaderText = "Mã nhân viên";
            datagridviewNV.Columns["HoTenNV"].HeaderText = "Họ tên nhân viên";
            datagridviewNV.Columns["Luong"].HeaderText = "Lương";
            datagridviewNV.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
            datagridviewNV.Columns["NgayBatDauNhanViec"].HeaderText = "Ngày bắt đầu nhận việc";
            datagridviewNV.Columns["IdTaiKhoan"].HeaderText = "Mã tài khoản";

          //  datagridviewNV.Columns["CT_DonHang"].Visible = false;
            datagridviewNV.Columns["TaiKhoan"].Visible = false;

        }

        public void hienThiDSTaiKhoan()
        {
            cbbMaTaiKhoan.DataSource = tkBUS.GetTaiKhoanChuaCoNguoiDung();
            cbbMaTaiKhoan.DisplayMember = "Id";
            cbbMaTaiKhoan.ValueMember = "Id";
        }

        private void datagridviewNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = datagridviewNV.Rows[e.RowIndex];
                Id = row.Cells["Id"].Value.ToString();
                txtHoTenNV.Text = row.Cells["HoTenNV"].Value.ToString();
                txtLuong.Text = row.Cells["Luong"].Value.ToString();
                txtSodienthoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                dtpNgayBatDauLamViec.Text = row.Cells["NgayBatDauNhanViec"].Value.ToString();
                cbbMaTaiKhoan.Text = row.Cells["IdTaiKhoan"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string hoTenNV = txtHoTenNV.Text.Trim();
            string luongText = txtLuong.Text.Trim();
            string sdt = txtSodienthoai.Text.Trim();
            string ngayBatDauText = dtpNgayBatDauLamViec.Text.Trim();

            if (!KiemTraDuLieuNhap(hoTenNV, luongText, sdt, ngayBatDauText, out string errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }

            // Chuyển đổi dữ liệu
            decimal luong = decimal.Parse(luongText);
            DateTime ngayBatDauLamViec = DateTime.Parse(ngayBatDauText);
            int maTaiKhoan = int.Parse(cbbMaTaiKhoan.SelectedValue.ToString());
            string thongBao = nvBUS.AddNhanVien(hoTenNV, ngayBatDauLamViec, sdt, luong, maTaiKhoan);
            MessageBox.Show(thongBao);

            if (thongBao == "Thêm nhân viên thành công!")
            {
                hienThiDSNhanVien();
                hienThiDSTaiKhoan();
                XoaDuLieu();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string hoTenNV = txtHoTenNV.Text.Trim();
            string luongText = txtLuong.Text.Trim();
            string sdt = txtSodienthoai.Text.Trim();
            string ngayBatDauText = dtpNgayBatDauLamViec.Text.Trim();

            if (!KiemTraDuLieuNhap(hoTenNV, luongText, sdt, ngayBatDauText, out string errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }

            if (string.IsNullOrWhiteSpace(Id))
            {
                MessageBox.Show("Chọn nhân viên cần sửa thông tin!");
                return;
            }

            // Chuyển đổi dữ liệu
            int maNV = int.Parse(Id);
            decimal luong = decimal.Parse(luongText);
            DateTime ngayBatDauLamViec = DateTime.Parse(ngayBatDauText);

            string thongBao = nvBUS.UpdateNhanVien(maNV, hoTenNV, ngayBatDauLamViec, sdt, luong);
            MessageBox.Show(thongBao);

            if (thongBao == "Cập nhật thông tin nhân viên thành công")
            {
                hienThiDSNhanVien();
                hienThiDSTaiKhoan();
                XoaDuLieu();
            }
        }

        private bool KiemTraDuLieuNhap(string hoTenNV, string luongText, string sdt, string ngayBatDauText, out string errorMessage)
        {
            errorMessage = "";

            // Kiểm tra dữ liệu trống
            if (string.IsNullOrWhiteSpace(hoTenNV) || string.IsNullOrWhiteSpace(luongText) ||
                string.IsNullOrWhiteSpace(sdt) || string.IsNullOrWhiteSpace(ngayBatDauText))
            {
                errorMessage = "Cần nhập đầy đủ thông tin!";
                return false;
            }

            // Kiểm tra định dạng lương
            if (!decimal.TryParse(luongText, out _))
            {
                errorMessage = "Lương không hợp lệ!";
                return false;
            }

            // Kiểm tra định dạng ngày
            if (!DateTime.TryParse(ngayBatDauText, out _))
            {
                errorMessage = "Ngày bắt đầu làm việc không hợp lệ!";
                return false;
            }

            // Kiểm tra số điện thoại
            if (!KiemTraSoDienThoai(sdt))
            {
                errorMessage = "Số điện thoại không hợp lệ!";
                return false;
            }

            return true;
        }

        private bool KiemTraSoDienThoai(string soDienThoai)
        {
            string phonePattern = @"^0\d{9}$"; // Số điện thoại bắt đầu bằng 0 và có 10 chữ số
            return Regex.IsMatch(soDienThoai, phonePattern);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem Id có hợp lệ không
            if (string.IsNullOrWhiteSpace(Id) || !int.TryParse(Id, out int parsedId))
            {
                MessageBox.Show("Chọn dữ liệu cần xóa hoặc Mã nhân viên không hợp lệ");
            }
            else
            {
                // Lấy đối tượng NhanVien
                NhanVien nhanVien = nvBUS.FindNhanVienById(parsedId);  // Trả về đối tượng NhanVien
                if (nhanVien == null)
                {
                    MessageBox.Show("Nhân viên không tồn tại");
                    return;
                }

                // Lấy IdTaiKhoan từ đối tượng NhanVien
                int maTK = nhanVien.Id;

                string kq = nvBUS.DeleteNhanVien(Id);  // Giữ lại kiểu string cho DeleteNhanVien (do phương thức yêu cầu string)
                tkBUS.DeleteTaiKhoan(maTK.ToString());

                if (kq == "Xóa nhân viên thành công")
                {
                    XoaDuLieu();
                    hienThiDSNhanVien();
                    MessageBox.Show(kq);
                }
                else
                {
                    MessageBox.Show(kq);
                }
            }
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            hienThiDSNhanVien();
            hienThiDSTaiKhoan();
            XoaDuLieu();
        }

        public void XoaDuLieu()
        {
            txtHoTenNV.Clear();
            txtLuong.Clear();
            txtSodienthoai.Clear();
            txtTimkiemNV.Clear();
        }

        private void btnTimkiemNV_Click(object sender, EventArgs e)
        {
            string TimKiem = txtTimkiemNV.Text;

            if (TimKiem == "")
            {
                MessageBox.Show("Khung nhập không được để trống");
            }
            else
            {
                var nv = nvBUS.FindNhanVienByName(TimKiem);
                if (nv.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu cần tìm.");
                }
                else
                {
                    datagridviewNV.DataSource = nv;
                    datagridviewNV.Columns["Id"].HeaderText = "Mã nhân viên";
                    datagridviewNV.Columns["HoTenNV"].HeaderText = "Họ tên nhân viên";
                    datagridviewNV.Columns["Luong"].HeaderText = "Lương";
                    datagridviewNV.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
                    datagridviewNV.Columns["NgayBatDauNhanViec"].HeaderText = "Ngày bắt đầu nhận việc";
                    datagridviewNV.Columns["IdTaiKhoan"].HeaderText = "Mã tài khoản";

                 //   datagridviewNV.Columns["CT_DonHang"].Visible = false;
                    datagridviewNV.Columns["TaiKhoan"].Visible = false;
                }
            }
        }
    }
}
