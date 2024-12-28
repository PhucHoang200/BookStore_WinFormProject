using BUS;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.UserControl_Admin
{
    public partial class UC_DonhangAdmin : UserControl
    {
        
        private readonly DonHangBUS _bus;
        public UC_DonhangAdmin()
        {
            InitializeComponent();
            _bus = new DonHangBUS();
        }

        private void dgvDsSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnHuyKH_Click(object sender, EventArgs e)
        {

        }

        private void btnCapnhatKH_Click(object sender, EventArgs e)
        {

        }

        private void btnLuuKH_Click(object sender, EventArgs e)
        {
            try
            {
                _bus.LuuKhachHang(txtHotenKH.Text, txtEmail.Text, txtSĐT.Text, txtDiachi.Text);
                MessageBox.Show("Lưu khách hàng thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void btnThemDonHang_Click(object sender, EventArgs e)
        {
            try
            {
                int maSach = int.Parse(txtMasach.Text);
                int soLuong = int.Parse(txtSoluong.Text);
                decimal donGia = Convert.ToDecimal(Column8.ValueType);
                _bus.ThemChiTietDonHang(dgvChitietDonhang, maSach, soLuong, donGia);

                txtTongtienDonhang.Text = _bus.TinhTongTien(dgvChitietDonhang).ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void btnHuyDonHang_Click(object sender, EventArgs e)
        {

        }

        private void btnCapnhatSach_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvChitietDonhang.Rows)
                {
                    if (Convert.ToInt32(row.Cells["Column9"].Value) == int.Parse(txtMasach.Text))
                    {
                        row.Cells["Column12"].Value = int.Parse(txtSoluong.Text);
                    }
                }

                txtTongtienDonhang.Text = _bus.TinhTongTien(dgvChitietDonhang).ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void dgvChitietDonhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTongtienDonhang_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXoaDonhang_Click(object sender, EventArgs e)
        {

        }

        private void btnLuuDonhang_Click(object sender, EventArgs e)
        {
            try
            {
                var chiTietDonHangs = dgvChitietDonhang.Rows.Cast<DataGridViewRow>().Select(row => new CT_DonHang
                {
                    IdSach = Convert.ToInt32(row.Cells["Column9"].Value),
                    SoLuongBan = Convert.ToInt32(row.Cells["Column12"].Value),
                    DonGiaBan = Convert.ToDecimal(row.Cells["Column11"].Value),
                    IdNhanVien = int.Parse(txtMaNV.Text)
                }).ToList();

                _bus.LuuDonHang(txtMaNV.Text, txtMaKH.Text, decimal.Parse(txtTongtienDonhang.Text), chiTietDonHangs);

                MessageBox.Show("Lưu đơn hàng thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void txtHotenKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSĐT_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiachi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMasach_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTimkiemSach_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTimkiemSach_Click(object sender, EventArgs e)
        {

        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void UC_DonhangAdmin_Load(object sender, EventArgs e)
        {
            LoadSachData();
            SetupDataGridView();
        }

        private void LoadSachData()
        {
            try
            {
                // Lấy danh sách sách từ BUS (sử dụng phương thức đã tạo trong DonHangBUS)
                var danhSachSach = _bus.LayDanhSachSach();

                // Đưa dữ liệu vào DataGridView
                dgvDsSach.Rows.Clear();  // Xóa tất cả các hàng cũ trước khi thêm mới

                foreach (var sach in danhSachSach)
                {
                    // Lấy tên tác giả (giả sử mỗi sách có thể có nhiều tác giả, lấy tên tác giả đầu tiên)
                    var tenTacGia = sach.TacGias.Any() ? sach.TacGias.First().TenTG : "Không có tác giả";

                    // Lấy tên thể loại (giả sử mỗi sách có thể có nhiều thể loại, lấy tên thể loại đầu tiên)
                    var tenTheLoai = sach.TheLoais.Any() ? sach.TheLoais.First().TenTL : "Không có thể loại";

                    // Lấy tên nhà xuất bản
                    var tenNXB = sach.NhaXuatBan != null ? sach.NhaXuatBan.TenNXB : "Không có nhà xuất bản";

                    // Lấy giá bán từ bảng Kho (lấy giá bán của kho đầu tiên liên quan đến sách)
                    var giaBan = sach.Khoes.Any() ? sach.Khoes.First().DonGiaBan ?? 0 : 0;

                    // Lấy số lượng tồn từ bảng Kho (lấy số lượng tồn của kho đầu tiên liên quan đến sách)
                    var soLuongTon = sach.Khoes.Any() ? sach.Khoes.First().SoLuongTon : 0;

                    // Thêm dữ liệu vào DataGridView
                    dgvDsSach.Rows.Add(sach.Id, sach.TenSach, tenTacGia, tenTheLoai, tenNXB, sach.NamXuatBan, soLuongTon, giaBan);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu sách: {ex.Message}");
            }
        }


        private void SetupDataGridView()
        {
            dgvDsSach.Columns["Column1"].HeaderText = "Mã Sách";  // Mã sách
            dgvDsSach.Columns["Column2"].HeaderText = "Tên Sách";  // Tên sách
            dgvDsSach.Columns["Column3"].HeaderText = "Tác Giả";   // Tác giả
            dgvDsSach.Columns["Column4"].HeaderText = "Thể Loại";  // Thể loại
            dgvDsSach.Columns["Column5"].HeaderText = "NXB";       // Nhà xuất bản
            dgvDsSach.Columns["Column6"].HeaderText = "Năm XB";    // Năm xuất bản
            dgvDsSach.Columns["Column7"].HeaderText = "SL Tồn";    // Số lượng tồn
            dgvDsSach.Columns["Column8"].HeaderText = "Giá";       // Giá

            dgvDsSach.Columns["Column1"].DataPropertyName = "Id";     // Mã sách
            dgvDsSach.Columns["Column2"].DataPropertyName = "TenSach"; // Tên sách
            dgvDsSach.Columns["Column3"].DataPropertyName = "TacGia";  // Tác giả
            dgvDsSach.Columns["Column4"].DataPropertyName = "TheLoai"; // Thể loại
            dgvDsSach.Columns["Column5"].DataPropertyName = "NXB";     // Nhà xuất bản
            dgvDsSach.Columns["Column6"].DataPropertyName = "NamXB";   // Năm xuất bản
            dgvDsSach.Columns["Column7"].DataPropertyName = "SoLuongTon"; // SL tồn
            dgvDsSach.Columns["Column8"].DataPropertyName = "GiaBan"; // Giá
        }

    }
}
