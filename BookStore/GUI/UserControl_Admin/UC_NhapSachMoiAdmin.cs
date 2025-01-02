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
        private List<NhapSachViewModel> _phieuNhapList;

        public UC_NhapSachMoiAdmin()
        {
            InitializeComponent();
            _bus = new NhapSachMoiBUS();
            _phieuNhapList = new List<NhapSachViewModel>();
        }

        private void numSLNhap_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnThemVaoPhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new NhapSachViewModel
                {
                    TenSach = txtTenSach.Text.Trim(),
                    TenTG = txtTacGia.Text.Trim(),
                    TenTL = txtTheLoai.Text.Trim(),
                    TenNXB = txtNhaXB.Text.Trim(),
                    NamXuatBan = int.Parse(txtNamXB.Text.Trim()),
                    SoLuongNhap = (int)numSLNhap.Value,
                    DonGiaNhap = decimal.Parse(txtDonGiaNhap.Text.Trim()),
                    DonGiaBan = decimal.Parse(txtDonGiaBan.Text.Trim()),
                    TenNCC = txtNCC.Text.Trim()
                };

                model.ThanhTien = model.SoLuongNhap * model.DonGiaNhap;
                _phieuNhapList.Add(model);
                dgvChitietPhieunhap.Rows.Add(model.TenSach, model.TenTG, model.TenTL, model.TenNXB, model.NamXuatBan, model.SoLuongNhap, model.DonGiaNhap, model.DonGiaBan, model.TenNCC, model.ThanhTien);
                lblTinhTongTien.Text = _phieuNhapList.Sum(x => x.ThanhTien).ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


                int phieuNhapId = LuuPhieuNhapSach();
                foreach (DataGridViewRow row in dgvChitietPhieunhap.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string tenSach = row.Cells["Column1"].Value?.ToString();
                        string tenTacGia = row.Cells["Column2"].Value?.ToString();
                        string tenTheLoai = row.Cells["Column3"].Value?.ToString();
                        string tenNXB = row.Cells["Column4"].Value?.ToString();

                        int namXuatBan = 0;
                        if (!int.TryParse(row.Cells["Column5"].Value?.ToString(), out namXuatBan))
                        {
                            MessageBox.Show("Năm xuất bản không hợp lệ.");
                            continue;
                        }

                        int soLuongNhap = 0;
                        if (!int.TryParse(row.Cells["Column6"].Value?.ToString(), out soLuongNhap))
                        {
                            MessageBox.Show("Số lượng nhập không hợp lệ.");
                            continue;
                        }

                        decimal giaNhap = 0;
                        if (!decimal.TryParse(row.Cells["Column7"].Value?.ToString(), out giaNhap))
                        {
                            MessageBox.Show("Giá nhập không hợp lệ.");
                            continue;
                        }

                        decimal giaBan = 0;
                        if (!decimal.TryParse(row.Cells["Column8"].Value?.ToString(), out giaBan))
                        {
                            MessageBox.Show("Giá bán không hợp lệ.");
                            continue;
                        }

                        string tenNhaCungCap = row.Cells["Column9"].Value?.ToString();
                        if (string.IsNullOrEmpty(tenNhaCungCap))
                        {
                            MessageBox.Show("Tên nhà cung cấp không được để trống.");
                            continue;
                        }

                        decimal thanhTien = 0;
                        if (!decimal.TryParse(row.Cells["Column10"].Value?.ToString(), out thanhTien))
                        {
                            MessageBox.Show("Thành tiền không hợp lệ.");
                            continue;
                        }
                        
                        int tacGiaId = LuuTacGia(tenTacGia);
                        int theLoaiId = LuuTheLoai(tenTheLoai);
                        int nhaXbId = LuuNhaXuatBan(tenNXB);
                        int nhaCungCapId = LuuNhaCungCap(tenNhaCungCap);

                        List<int> tacGiaIds = new List<int> { tacGiaId };
                        List<int> theLoaiIds = new List<int> { theLoaiId };
                        List<int> nhaCungCapIds = new List<int> { nhaCungCapId };

                        int sachId = LuuSach(tenSach, namXuatBan, nhaXbId, tacGiaIds, theLoaiIds, nhaCungCapIds);

                        LuuCTPhieuNhap(phieuNhapId, sachId, nhaCungCapId, soLuongNhap, giaNhap, giaBan, thanhTien);
                        LuuKho(sachId, soLuongNhap, giaNhap, giaBan);

                    }
                }

                

                MessageBox.Show("Phiếu nhập đã được lưu thành công!");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show("Lỗi khi lưu phiếu nhập: " + errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int LuuTacGia(string tenTacGia)
        {
            // Thêm tác giả vào bảng TacGia và trả về Id
            using (var context = new BookStoreDBEntities())
            {
                var tacGia = new TacGia { TenTG = tenTacGia };
                context.TacGias.Add(tacGia);
                context.SaveChanges();
                return tacGia.Id;
            }
        }

        private int LuuTheLoai(string tenTheLoai)
        {
            // Thêm thể loại vào bảng TheLoai và trả về Id
            using (var context = new BookStoreDBEntities())
            {
                var theLoai = new TheLoai { TenTL = tenTheLoai };
                context.TheLoais.Add(theLoai);
                context.SaveChanges();
                return theLoai.Id;
            }
        }

        private int LuuNhaXuatBan(string tenNhaXuatBan)
        {
            // Thêm nhà xuất bản vào bảng NhaXuatBan và trả về Id
            using (var context = new BookStoreDBEntities())
            {
                var nhaXb = new NhaXuatBan { TenNXB = tenNhaXuatBan };
                context.NhaXuatBans.Add(nhaXb);
                context.SaveChanges();
                return nhaXb.Id;
            }
        }

        private int LuuNhaCungCap(string tenNhaCungCap)
        {
            // Thêm nhà cung cấp vào bảng NhaCungCap và trả về Id
            using (var context = new BookStoreDBEntities())
            {
                var nhaCungCap = new NhaCungCap { TenNCC = tenNhaCungCap };
                context.NhaCungCaps.Add(nhaCungCap);
                context.SaveChanges();
                return nhaCungCap.Id;
            }
        }

        private int LuuSach(string tenSach, int namXuatBan, int nhaXbId, List<int> tacGiaIds, List<int> theLoaiIds, List<int> nhaCungCapIds)
        {
            // Thêm sách vào bảng Sach và trả về Id
            using (var context = new BookStoreDBEntities())
            {
                var sach = new Sach
                {
                    TenSach = tenSach,
                    NamXuatBan = namXuatBan,
                    IdNXB = nhaXbId
                };
                context.Saches.Add(sach);
                context.SaveChanges();

                // Liên kết tác giả, thể loại và nhà cung cấp
                foreach (var tacGiaId in tacGiaIds)
                {
                    sach.TacGias.Add(context.TacGias.FirstOrDefault(t => t.Id == tacGiaId)); // Tác giả liên kết
                }

                foreach (var theLoaiId in theLoaiIds)
                {
                    sach.TheLoais.Add(context.TheLoais.FirstOrDefault(t => t.Id == theLoaiId)); // Thể loại liên kết
                }

                foreach (var nhaCungCapId in nhaCungCapIds)
                {
                    sach.NhaCungCaps.Add(context.NhaCungCaps.FirstOrDefault(n => n.Id == nhaCungCapId)); // Nhà cung cấp liên kết
                }

                context.SaveChanges();
                return sach.Id;
            }
        }

        private void LuuKho(int sachId, int soLuongTon, decimal donGiaNhap, decimal donGiaBan)
        {
            // Lưu thông tin vào bảng Kho
            using (var context = new BookStoreDBEntities())
            {
                var kho = new Kho
                {
                    IdSach = sachId,
                    SoLuongTon = soLuongTon,
                    DonGiaNhap = donGiaNhap,
                    DonGiaBan = donGiaBan
                };
                context.Khoes.Add(kho);
                context.SaveChanges();
            }
        }

        private int LuuPhieuNhapSach()
        {
            // Lấy giá trị từ lblTinhTongTien
            if (!decimal.TryParse(lblTinhTongTien.Text, out decimal tongTien))
            {
                throw new Exception("Giá trị tổng tiền không hợp lệ. Vui lòng kiểm tra lại.");
            }

            // Lưu phiếu nhập vào bảng PhieuNhapSach và trả về Id
            using (var context = new BookStoreDBEntities())
            {
                var phieuNhap = new PhieuNhapSach
                {
                    TongTienNhap = tongTien,
                    NgayNhapSach = DateTime.Now // Lưu ngày hiện tại
                };
                context.PhieuNhapSaches.Add(phieuNhap);
                context.SaveChanges();
                return phieuNhap.Id;
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
                    MaNCC = nhaCungCapId,
                    SoLuongNhap = soLuongNhap,
                    DonGiaNhap = donGiaNhap,
                    DonGiaBan = donGiaBan,
                    ThanhTien = thanhTien
                };
                context.CT_PhieuNhap.Add(ctPhieuNhap);
                context.SaveChanges();
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            
        }

        private void txtTacGia_Leave(object sender, EventArgs e)
        {
            string tacGia = txtTacGia.Text.Trim();
            if (!_bus.KiemTraTacGiaTonTai(tacGia))
            {
                DialogResult result = MessageBox.Show("Tác giả chưa tồn tại. Bạn có muốn thêm mới?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
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
                
            }
        }

        private void txtNhaXB_Leave(object sender, EventArgs e)
        {
            string nhaXB = txtNhaXB.Text.Trim();
            if (!_bus.KiemTraNhaXBTonTai(nhaXB))
            {
                DialogResult result = MessageBox.Show("Nhà xuất bản chưa tồn tại. Bạn có muốn thêm mới?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
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
                
            }
        }

        private void dgvChitietPhieunhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
