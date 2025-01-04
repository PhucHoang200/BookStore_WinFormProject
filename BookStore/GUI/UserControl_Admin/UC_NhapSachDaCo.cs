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
    public partial class UC_NhapSachDaCo : UserControl
    {
        private NhapSachDaCoBUS _bus = new NhapSachDaCoBUS();
        private List<CT_PhieuNhap> _chiTietPhieuNhapList = new List<CT_PhieuNhap>();

        public UC_NhapSachDaCo()
        {
            InitializeComponent();
        }


        private void UC_NhapSachDaCo_Load(object sender, EventArgs e)
        {
            LoadSachToComboBox();
        }

        private void LoadSachToComboBox()
        {
            var sachList = _bus.LoadSachList();
            cbbTenSach.DataSource = sachList;
            cbbTenSach.DisplayMember = "TenSach";
            cbbTenSach.ValueMember = "Id";
        }
        private void cbbTenSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTenSach.SelectedValue is int selectedId)
            {
                var sachDetails = _bus.GetSachDetails(selectedId);
                if (sachDetails != null)
                {
                    txtTacGia.Text = sachDetails.TenTG;
                    txtTheLoai.Text = sachDetails.TenTL;
                    txtNXB.Text = sachDetails.TenNXB;
                    txtNamXB.Text = sachDetails.NamXuatBan.ToString();
                    txtDonGiaNhap.Text = sachDetails.DonGiaNhap.ToString();
                    txtDonGiaBan.Text = sachDetails.DonGiaBan.ToString();
                    txtNCC.Text = sachDetails.TenNCC;
                }
            }
        }

        private void txtTacGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTheLoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNXB_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNamXB_TextChanged(object sender, EventArgs e)
        {

        }

        private void numSLNhap_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtDonGiaNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDonGiaBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNCC_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvChitietPhieunhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThemVaoPhieuNhap_Click(object sender, EventArgs e)
        {
            if (cbbTenSach.SelectedValue is int selectedId)
            {
                var chiTiet = new CT_PhieuNhap
                {
                    SoLuongNhap = (int)numSLNhap.Value,
                    DonGiaNhap = Convert.ToDecimal(txtDonGiaNhap.Text),
                    DonGiaBan = Convert.ToDecimal(txtDonGiaBan.Text)
                };

                // Tính thành tiền cho sách này
                decimal thanhTien = chiTiet.SoLuongNhap * chiTiet.DonGiaNhap;

                _chiTietPhieuNhapList.Add(chiTiet);

                // Tính toán tổng tiền cho phiếu nhập
                decimal tongTien = _chiTietPhieuNhapList.Sum(ct => ct.SoLuongNhap * ct.DonGiaNhap);

                // Cập nhật tổng tiền vào label lblTinhTongTien
                lblTinhTongTien.Text = $"{tongTien:N2}";  // Định dạng tiền tệ

                dgvChitietPhieunhap.Rows.Add(
                    cbbTenSach.Text,
                    txtTacGia.Text,
                    txtTheLoai.Text,
                    txtNXB.Text,
                    txtNamXB.Text,
                    numSLNhap.Value,
                    txtDonGiaNhap.Text,
                    txtDonGiaBan.Text,
                    txtNCC.Text,
                    thanhTien
                );

                MessageBox.Show("Thêm sách vào phiếu nhập thành công!");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

        }

        private void lblTinhTongTien_Click(object sender, EventArgs e)
        {

        }

        private void btnLuuPhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                // Loại bỏ phần không phải số trong lblTinhTongTien.Text
                string totalText = lblTinhTongTien.Text.Replace("Tổng Tiền: ", "").Replace(",", "").Trim();

                // Kiểm tra tổng tiền có hợp lệ hay không
                if (!decimal.TryParse(totalText, out decimal tongTien) || tongTien <= 0)
                {
                    MessageBox.Show("Tổng tiền không hợp lệ hoặc bằng 0. Vui lòng kiểm tra lại.");
                    return;
                }

                // Kiểm tra danh sách chi tiết phiếu nhập
                if (dgvChitietPhieunhap.Rows.Count == 0)
                {
                    MessageBox.Show("Danh sách chi tiết phiếu nhập trống. Vui lòng thêm sách trước khi lưu.");
                    return;
                }

                // Lưu phiếu nhập và lấy ID của phiếu nhập vừa lưu
                var phieuNhapId = _bus.SavePhieuNhap(tongTien);

                // Lưu chi tiết phiếu nhập
                foreach (DataGridViewRow row in dgvChitietPhieunhap.Rows)
                {
                    if (row.IsNewRow) continue;

                    // Lấy sachId từ cbbTenSach
                    int sachId = Convert.ToInt32(cbbTenSach.SelectedValue);

                    // Lấy TenNCC từ txtNCC và tìm nhaCungCapId từ cơ sở dữ liệu
                    string tenNCC = txtNCC.Text.Trim();
                    int nhaCungCapId = GetNhaCungCapIdByName(tenNCC);

                    if (nhaCungCapId == -1)
                    {
                        MessageBox.Show("Không tìm thấy nhà cung cấp với tên đã nhập.");
                        return;
                    }

                    // Lấy các giá trị khác từ DataGridView
                    int soLuongNhap = Convert.ToInt32(row.Cells["Column6"].Value);
                    decimal donGiaNhap = Convert.ToDecimal(row.Cells["Column7"].Value);
                    decimal donGiaBan = Convert.ToDecimal(row.Cells["Column8"].Value);
                    decimal thanhTien = Convert.ToDecimal(row.Cells["Column10"].Value);

                    // Gọi phương thức lưu chi tiết phiếu nhập
                    LuuCTPhieuNhap(phieuNhapId, sachId, nhaCungCapId, soLuongNhap, donGiaNhap, donGiaBan, thanhTien);
                }

                // Hiển thị thông báo thành công
                MessageBox.Show($"Lưu phiếu nhập thành công! Mã phiếu nhập: {phieuNhapId}");

                // Xóa dữ liệu trong DataGridView và danh sách chi tiết phiếu nhập
                dgvChitietPhieunhap.Rows.Clear();
                lblTinhTongTien.Text = "0.00"; // Reset tổng tiền về 0
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show($"Lỗi trong quá trình lưu phiếu nhập: {ex.Message}");
            }
        }

        private void LuuCTPhieuNhap(int phieuNhapId, int sachId, int nhaCungCapId, int soLuongNhap, decimal donGiaNhap, decimal donGiaBan, decimal thanhTien)
        {
            // Lưu chi tiết phiếu nhập vào bảng CT_PhieuNhap
            using (var context = new BookStoreDBEntities())
            {
                var ctPhieuNhap = new CT_PhieuNhap
                {
                    MaPhieuNhap = phieuNhapId,
                    MaSach = sachId,
                    //MaNCC = nhaCungCapId,
                    SoLuongNhap = soLuongNhap,
                    DonGiaNhap = donGiaNhap,
                    DonGiaBan = donGiaBan,
                    ThanhTien = thanhTien
                };
                context.CT_PhieuNhap.Add(ctPhieuNhap);
                context.SaveChanges();
            }
        }

        private int GetNhaCungCapIdByName(string tenNCC)
        {
            using (var context = new BookStoreDBEntities())
            {
                // Tìm nhà cung cấp theo tên
                var nhaCungCap = context.NhaCungCaps
                    .Where(ncc => ncc.TenNCC.Equals(tenNCC, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                // Nếu tìm thấy, trả về Id của nhà cung cấp
                if (nhaCungCap != null)
                {
                    return nhaCungCap.Id;
                }

                // Nếu không tìm thấy, trả về -1 để báo lỗi
                return -1;
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

    }
}
