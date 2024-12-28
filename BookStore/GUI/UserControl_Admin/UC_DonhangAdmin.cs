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
        private LoginViewModel currentUser;

        public UC_DonhangAdmin(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            _bus = new DonHangBUS();
            currentUser = loginViewModel;

            // Gán IdTaiKhoan vào txtMaTK khi UC_DonhangAdmin được load
            txtMaTK.Text = currentUser.IdTaiKhoan.ToString();
        }
        //public UC_DonhangAdmin()
        //{
        //    InitializeComponent();

        //}

        private void dgvDsSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra chỉ số hàng hợp lệ
            {
                txtMasach.Text = dgvDsSach.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
            }
        }

        private void btnHuyKH_Click(object sender, EventArgs e)
        {
            txtHotenKH.Clear();
            txtEmail.Clear();
            txtSĐT.Clear();
            txtDiachi.Clear();
        }

        private void btnCapnhatKH_Click(object sender, EventArgs e)
        {
            try
            {
                _bus.CapNhatKhachHang(int.Parse(txtMaKH.Text), txtHotenKH.Text, txtEmail.Text, txtSĐT.Text, txtDiachi.Text);
                MessageBox.Show("Cập nhật khách hàng thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void btnLuuKH_Click(object sender, EventArgs e)
        {
            try
            {
                var maKH = _bus.LuuKhachHangVaLayMa(txtHotenKH.Text, txtEmail.Text, txtSĐT.Text, txtDiachi.Text);
                txtMaKH.Text = maKH.ToString();
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
                if (string.IsNullOrWhiteSpace(txtMasach.Text) || string.IsNullOrWhiteSpace(txtSoluong.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã sách và số lượng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maSach = int.Parse(txtMasach.Text);
                int soLuong = int.Parse(txtSoluong.Text);

                // Tìm giá sách từ dgvDsSach
                decimal donGia = 0;
                var rowFound = dgvDsSach.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r => r.Cells["Column1"].Value != null && Convert.ToInt32(r.Cells["Column1"].Value) == maSach);

                if (rowFound != null)
                {
                    donGia = Convert.ToDecimal(rowFound.Cells["Column8"].Value);
                }

                if (donGia == 0)
                {
                    MessageBox.Show("Không tìm thấy giá sách cho mã sách đã chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Thêm chi tiết đơn hàng
                _bus.ThemChiTietDonHang(dgvChitietDonhang, maSach, soLuong, donGia);
                txtTongtienDonhang.Text = _bus.TinhTongTien(dgvChitietDonhang).ToString("N0");
            }
            catch (FormatException)
            {
                MessageBox.Show("Dữ liệu nhập không hợp lệ. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyDonHang_Click(object sender, EventArgs e)
        {
            txtMasach.Clear();
            txtSoluong.Clear();
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
            if (e.RowIndex >= 0) // Kiểm tra chỉ số hàng hợp lệ
            {
                txtMasach.Text = dgvChitietDonhang.Rows[e.RowIndex].Cells["Column9"].Value.ToString();
                txtSoluong.Text = dgvChitietDonhang.Rows[e.RowIndex].Cells["Column12"].Value.ToString();
            }
        }

        private void txtTongtienDonhang_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXoaDonhang_Click(object sender, EventArgs e)
        {
            if (dgvChitietDonhang.SelectedRows.Count > 0)
            {
                dgvChitietDonhang.Rows.RemoveAt(dgvChitietDonhang.SelectedRows[0].Index);
                txtTongtienDonhang.Text = _bus.TinhTongTien(dgvChitietDonhang).ToString("N0");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng để xóa.");
            }
        }

        private void btnLuuDonhang_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu dgvChitietDonhang không có dữ liệu
                if (dgvChitietDonhang.Rows.Count == 0)
                {
                    MessageBox.Show("Chi tiết đơn hàng trống. Vui lòng thêm sản phẩm trước khi lưu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng DonHang
                var donHang = new DonHang
                {
                    IdKhachHang = int.Parse(txtMaKH.Text),
                    TongTienBan = decimal.Parse(txtTongtienDonhang.Text),
                    NgayMuaHang = DateTime.Now
                };

                // Tạo danh sách chi tiết đơn hàng
                var chiTietDonHangs = new List<CT_DonHang>();
                foreach (DataGridViewRow row in dgvChitietDonhang.Rows)
                {
                    chiTietDonHangs.Add(new CT_DonHang
                    {
                        IdSach = Convert.ToInt32(row.Cells["Column9"].Value),
                        SoLuongBan = Convert.ToInt32(row.Cells["Column12"].Value),
                        DonGiaBan = Convert.ToDecimal(row.Cells["Column11"].Value),
                        IdTaiKhoan = int.Parse(txtMaTK.Text)
                    });
                }

                // Lưu dữ liệu vào cơ sở dữ liệu
                _bus.LuuDonHang(donHang, chiTietDonHangs);

                // Thông báo thành công
                MessageBox.Show("Lưu đơn hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa dữ liệu sau khi lưu
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtMaKH.Clear();
            txtMaTK.Clear();
            txtTongtienDonhang.Clear();
            txtMasach.Clear();
            txtSoluong.Clear();
            dgvChitietDonhang.Rows.Clear();
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
            try
            {
                string tuKhoa = txtTimkiemSach.Text.Trim();

                if (string.IsNullOrWhiteSpace(tuKhoa))
                {
                    MessageBox.Show("Vui lòng nhập từ khóa để tìm kiếm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi phương thức tìm kiếm từ BUS
                var danhSachSach = _bus.TimKiemSach(tuKhoa);

                if (danhSachSach == null || !danhSachSach.Any())
                {
                    MessageBox.Show("Không tìm thấy sách phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvDsSach.Rows.Clear(); // Xóa dữ liệu cũ
                    return;
                }

                // Xóa dữ liệu cũ trước khi thêm mới
                dgvDsSach.Rows.Clear();

                foreach (var sach in danhSachSach)
                {
                    // Lấy thông tin chi tiết của từng sách
                    var tenTacGia = sach.TacGias.Any() ? sach.TacGias.First().TenTG : "Không có tác giả";
                    var tenTheLoai = sach.TheLoais.Any() ? sach.TheLoais.First().TenTL : "Không có thể loại";
                    var tenNXB = sach.NhaXuatBan != null ? sach.NhaXuatBan.TenNXB : "Không có nhà xuất bản";
                    var giaBan = sach.Khoes.Any() ? sach.Khoes.First().DonGiaBan ?? 0 : 0;
                    var soLuongTon = sach.Khoes.Any() ? sach.Khoes.First().SoLuongTon : 0;

                    // Thêm dữ liệu vào DataGridView
                    dgvDsSach.Rows.Add(sach.Id, sach.TenSach, tenTacGia, tenTheLoai, tenNXB, sach.NamXuatBan, soLuongTon, giaBan);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            var danhSachSach = _bus.LayDanhSachSach();
            LoadSachData();
        }
    }
}
