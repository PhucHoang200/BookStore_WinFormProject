using BUS;
using DTO;
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
    public partial class UC_NhapSachMoiAdmin : UserControl
    {
        private NhapSachMoiBUS _bus;
        private List<ChiTietPhieuNhap> _chiTietPhieuNhapList;

        public UC_NhapSachMoiAdmin()
        {
            InitializeComponent();
            _bus = new NhapSachMoiBUS();
            _chiTietPhieuNhapList = new List<ChiTietPhieuNhap>();
        }

        private void numSLNhap_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnThemVaoPhieuNhap_Click(object sender, EventArgs e)
        {
            string tenSach = txtTenSach.Text.Trim();
            string tacGia = txtTacGia.Text.Trim();
            string theLoai = txtTheLoai.Text.Trim();
            string nhaXB = txtNhaXB.Text.Trim();
            string nhaCungCap = txtNCC.Text.Trim();
            if (!decimal.TryParse(txtDonGiaNhap.Text.Trim(), out decimal giaNhap))
            {
                MessageBox.Show("Đơn giá nhập không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtDonGiaBan.Text.Trim(), out decimal giaBan))
            {
                MessageBox.Show("Đơn giá bán không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int soLuongNhap = (int)numSLNhap.Value;
            if (soLuongNhap <= 0)
            {
                MessageBox.Show("Số lượng nhập phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Xử lý namXB
            if (!int.TryParse(txtNamXB.Text.Trim(), out int namXB))
            {
                MessageBox.Show("Năm xuất bản phải là một số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var chiTiet = new ChiTietPhieuNhap
            {
                TenSach = tenSach,
                TacGia = tacGia,
                TheLoai = theLoai,
                NhaXB = nhaXB,
                NamXuatBan = namXB,
                SoLuongNhap = soLuongNhap,
                GiaNhap = giaNhap,
                GiaBan = giaBan,
                NhaCungCap = nhaCungCap,
                ThanhTien = soLuongNhap * giaNhap
            };

            _chiTietPhieuNhapList.Add(chiTiet);

            dgvChitietPhieunhap.Rows.Add(tenSach, tacGia, theLoai, nhaXB, namXB, soLuongNhap, giaNhap, giaBan, nhaCungCap, chiTiet.ThanhTien);
            lblTinhTongTien.Text = _chiTietPhieuNhapList.Sum(x => x.ThanhTien).ToString("N2");
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvChitietPhieunhap.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvChitietPhieunhap.SelectedRows[0].Index;

                string tenSach = txtTenSach.Text.Trim();
                string tacGia = txtTacGia.Text.Trim();
                string theLoai = txtTheLoai.Text.Trim();
                string nhaXB = txtNhaXB.Text.Trim();
                string nhaCungCap = txtNCC.Text.Trim();
                if (!decimal.TryParse(txtDonGiaNhap.Text.Trim(), out decimal giaNhap))
                {
                    MessageBox.Show("Đơn giá nhập không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtDonGiaBan.Text.Trim(), out decimal giaBan))
                {
                    MessageBox.Show("Đơn giá bán không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int soLuongNhap = (int)numSLNhap.Value;
                if (soLuongNhap <= 0)
                {
                    MessageBox.Show("Số lượng nhập phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Xử lý namXB
                if (!int.TryParse(txtNamXB.Text.Trim(), out int namXB))
                {
                    MessageBox.Show("Năm xuất bản phải là một số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var chiTiet = _chiTietPhieuNhapList[selectedIndex];
                chiTiet.TenSach = tenSach;
                chiTiet.TacGia = tacGia;
                chiTiet.TheLoai = theLoai;
                chiTiet.NhaXB = nhaXB;
                chiTiet.NamXuatBan = namXB;
                chiTiet.SoLuongNhap = soLuongNhap;
                chiTiet.GiaNhap = giaNhap;
                chiTiet.GiaBan = giaBan;
                chiTiet.NhaCungCap = nhaCungCap;
                chiTiet.ThanhTien = soLuongNhap * giaNhap;

                dgvChitietPhieunhap.Rows[selectedIndex].SetValues(tenSach, tacGia, theLoai, nhaXB, namXB, soLuongNhap, giaNhap, giaBan, nhaCungCap, chiTiet.ThanhTien);
                lblTinhTongTien.Text = _chiTietPhieuNhapList.Sum(x => x.ThanhTien).ToString("N2");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

        }

        private void dgvChitietPhieunhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblTinhTongTien_Click(object sender, EventArgs e)
        {

        }

        private void btnLuuPhieuNhap_Click(object sender, EventArgs e)
        {
            if (_chiTietPhieuNhapList == null || !_chiTietPhieuNhapList.Any())
            {
                MessageBox.Show("Không có dữ liệu để lưu. Vui lòng thêm chi tiết phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gọi phương thức lưu trong lớp BUS
                _bus.LuuPhieuNhapSach(_chiTietPhieuNhapList);

                MessageBox.Show("Lưu phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa danh sách sau khi lưu
                _chiTietPhieuNhapList.Clear();
                dgvChitietPhieunhap.Rows.Clear();
                lblTinhTongTien.Text = "0";
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                MessageBox.Show($"Đã xảy ra lỗi khi lưu phiếu nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvChitietPhieunhap.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvChitietPhieunhap.SelectedRows[0].Index;
                _chiTietPhieuNhapList.RemoveAt(selectedIndex);
                dgvChitietPhieunhap.Rows.RemoveAt(selectedIndex);
                lblTinhTongTien.Text = _chiTietPhieuNhapList.Sum(x => x.ThanhTien).ToString("N2");
            }
        }

        private void txtTacGia_Leave(object sender, EventArgs e)
        {
            string tacGia = txtTacGia.Text.Trim();
            if (!_bus.KiemTraTacGiaTonTai(tacGia))
            {
                DialogResult result = MessageBox.Show("Tác giả chưa tồn tại. Bạn có muốn thêm mới?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _bus.LuuTacGia(tacGia);
                }
            }
        }

        private void txtTenSach_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTheLoai_Leave(object sender, EventArgs e)
        {
            string theLoai = txtTheLoai.Text.Trim();
            if (!_bus.KiemTraTheLoaiTonTai(theLoai))
            {
                DialogResult result = MessageBox.Show("Thể loại chưa tồn tại. Bạn có muốn thêm mới?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _bus.LuuTheLoai(theLoai);
                }
            }
        }

        private void txtNhaXB_Leave(object sender, EventArgs e)
        {
            string nhaXB = txtNhaXB.Text.Trim();
            if (!_bus.KiemTraNhaXBTonTai(nhaXB))
            {
                DialogResult result = MessageBox.Show("Nhà xuất bản chưa tồn tại. Bạn có muốn thêm mới?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _bus.LuuNhaXB(nhaXB);
                }
            }
        }

        private void txtNamXB_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDonGiaNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDonGiaBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNCC_Leave(object sender, EventArgs e)
        {
            string nhaCungCap = txtNCC.Text.Trim();
            if (!_bus.KiemTraNCCTonTai(nhaCungCap))
            {
                DialogResult result = MessageBox.Show("Nhà cung cấp chưa tồn tại. Bạn có muốn thêm mới?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _bus.LuuNCC(nhaCungCap);
                }
            }
        }
    }
}
