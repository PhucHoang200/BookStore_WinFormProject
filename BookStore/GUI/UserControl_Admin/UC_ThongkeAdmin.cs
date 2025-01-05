using DTO;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI.UserControl_Admin
{
    public partial class UC_ThongkeAdmin : UserControl
    {
        public UC_ThongkeAdmin()
        {
            InitializeComponent();

        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            // Lấy ngày bắt đầu và kết thúc từ DateTimePicker
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            // Lấy dữ liệu doanh thu
            var doanhThuData = GetDoanhThuTheoSanPham(startDate, endDate);
            var theLoaiData = GetThongKeTheLoai(startDate, endDate); // Dữ liệu thể loại bán ra
            var nhanVienData = GetThongKeNhanVien(startDate, endDate); // Dữ liệu nhân viên

            // Tính tổng doanh thu
            decimal tongDoanhThu = doanhThuData.Sum(item => item.TongDoanhThu);
            label1.Text = $"Tổng Doanh Thu: {tongDoanhThu:C}";

            // Xóa dữ liệu cũ trên biểu đồ
            chartRevenue.Series.Clear();
            chartRevenue.ChartAreas.Clear();

            // Biểu đồ thống kê sách bán ra (Biểu đồ cột)
            ChartArea chartArea = new ChartArea("ChartArea1");
            chartRevenue.ChartAreas.Add(chartArea);
            Series series = new Series("Sách Bán Ra")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                IsValueShownAsLabel = true
            };
            chartRevenue.Series.Add(series);

            foreach (var item in doanhThuData)
            {
                series.Points.AddXY(item.TenSach, item.TongDoanhThu);
            }

            chartRevenue.Titles.Clear();
            chartRevenue.Titles.Add("Báo Cáo Doanh Thu Theo Sách");
            chartRevenue.Titles[0].Font = new Font("Segoe UI", 14, FontStyle.Bold);
            chartRevenue.ChartAreas[0].AxisX.Title = "Tên Sách";
            chartRevenue.ChartAreas[0].AxisY.Title = "Tổng Doanh Thu (VNĐ)";

            // Biểu đồ tròn thống kê thể loại bán ra
            chartCategoryRevenue.Series.Clear();
            chartCategoryRevenue.ChartAreas.Clear();
            ChartArea chartAreaCategory = new ChartArea("ChartArea2");
            chartCategoryRevenue.ChartAreas.Add(chartAreaCategory);
            Series seriesCategory = new Series("Thể Loại")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };
            chartCategoryRevenue.Series.Add(seriesCategory);

            // Tính tổng doanh thu
            decimal totalRevenue = theLoaiData.Sum(item => item.TongDoanhThu);

            foreach (var item in theLoaiData)
            {
                // Tính tỷ lệ phần trăm
                decimal percentage = (item.TongDoanhThu / totalRevenue) * 100;

                // Thêm điểm vào biểu đồ với giá trị phần trăm
                seriesCategory.Points.AddXY(item.TenTL, item.TongDoanhThu);
                seriesCategory.Points[seriesCategory.Points.Count - 1].Label = $"{percentage:F2}%"; // Hiển thị phần trăm với 2 chữ số thập phân
            }


            chartCategoryRevenue.Titles.Clear();
            chartCategoryRevenue.Titles.Add("Thống Kê Thể Loại Bán Ra");

            // Biểu đồ đường thống kê hiệu suất bán hàng của nhân viên
            chartEmployeePerformance.Series.Clear();
            chartEmployeePerformance.ChartAreas.Clear();
            ChartArea chartAreaEmployee = new ChartArea("ChartArea3");
            chartEmployeePerformance.ChartAreas.Add(chartAreaEmployee);
            Series seriesEmployee = new Series("Hiệu Suất Bán Hàng")
            {
                ChartType = SeriesChartType.Line,
                XValueType = ChartValueType.Date,
                IsValueShownAsLabel = true
            };
            chartEmployeePerformance.Series.Add(seriesEmployee);

            foreach (var item in nhanVienData)
            {
                seriesEmployee.Points.AddXY(item.Ngay, item.TongDoanhThu);
            }

            chartEmployeePerformance.Titles.Clear();
            chartEmployeePerformance.Titles.Add("Hiệu Suất Bán Hàng Của Nhân Viên");
        }

        // Lấy dữ liệu hiệu suất bán hàng của nhân viên
        public List<BaoCaoDoanhThuTheoNhanVien> GetThongKeNhanVien(DateTime startDate, DateTime endDate)
        {
            using (var context = new BookStoreDBEntities())
            {
                var nhanVien = context.CT_DonHang
                    .Where(ct => ct.DonHang.NgayMuaHang >= startDate && ct.DonHang.NgayMuaHang <= endDate)
                    .GroupBy(ct => ct.TaiKhoan.HoTen) // Group by employee name (TaiKhoan)
                    .Select(g => new BaoCaoDoanhThuTheoNhanVien
                    {
                        TenNhanVien = g.Key,
                        Ngay = g.FirstOrDefault().DonHang.NgayMuaHang.Value,
                        TongDoanhThu = g.Sum(x => x.SoLuongBan * x.DonGiaBan)
                    })
                    .ToList();
                return nhanVien;
            }
        }

        // Lấy dữ liệu thể loại bán ra
        public List<BaoCaoDoanhThuTheoTheLoai> GetThongKeTheLoai(DateTime startDate, DateTime endDate)
        {
            using (var context = new BookStoreDBEntities())
            {
                var theLoai = context.CT_DonHang
                    .Where(ct => ct.DonHang.NgayMuaHang >= startDate && ct.DonHang.NgayMuaHang <= endDate)
                    .GroupBy(ct => ct.Sach.TheLoais.FirstOrDefault().TenTL) // Giả sử Sach có thuộc tính TheLoais
                    .Select(g => new BaoCaoDoanhThuTheoTheLoai
                    {
                        TenTL = g.Key,
                        TongDoanhThu = g.Sum(x => x.SoLuongBan * x.DonGiaBan)
                    })
                    .ToList();

                return theLoai;
            }
        }

        // Các lớp DTO cho thể loại và nhân viên
        public class BaoCaoDoanhThuTheoTheLoai
        {
            public string TenTL { get; set; }
            public decimal TongDoanhThu { get; set; }
        }

        public class BaoCaoDoanhThuTheoNhanVien
        {
            public string TenNhanVien { get; set; }
            public DateTime Ngay { get; set; }
            public decimal TongDoanhThu { get; set; }
        }

        public List<BaoCaoDoanhThuTheoSanPham> GetDoanhThuTheoSanPham(DateTime startDate, DateTime endDate)
        {
            using (var context = new BookStoreDBEntities())
            {
                var doanhThu = context.CT_DonHang
                    .Where(ct => ct.DonHang.NgayMuaHang >= startDate && ct.DonHang.NgayMuaHang <= endDate)
                    .GroupBy(ct => ct.Sach.TenSach) // Group by product name (Sach)
                    .Select(g => new BaoCaoDoanhThuTheoSanPham
                    {
                        TenSach = g.Key,
                        TongDoanhThu = g.Sum(x => x.SoLuongBan * x.DonGiaBan)
                    })
                    .ToList();

                return doanhThu;
            }
        }

        public class BaoCaoDoanhThuTheoSanPham
        {
            public string TenSach { get; set; }
            public decimal TongDoanhThu { get; set; }
        }

        private void btnXuatThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date;

                // Đường dẫn tới thư mục lưu báo cáo
                string folderPath = @"C:\BaoCao";

                // Gọi hàm xuất báo cáo
                ExportReportHelper.ExportToWordAndPdf(startDate, endDate, folderPath);

                MessageBox.Show("Đã xuất báo cáo thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}");
            }
        }
    }
}
