﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Pdf;
using Spire.Pdf.Graphics;

namespace GUI
{
    public class InvoiceExporter
    {
        //public static void ExportToWord(DataGridView dgvDsSanPham, Label lblMaDH, Label lblNgayMuaHang, Label lblHoTenKH,
        //                                 Label lblEmail, Label lblDiaChi, Label lblSĐT, Label lblTongTien)
        //{
        //    // Tạo đối tượng Document
        //    Document document = new Document();

        //    // Thêm một Section vào Document
        //    Section section = document.AddSection();

        //    // Thêm tiêu đề cho hóa đơn
        //    Paragraph paragraph = section.AddParagraph();
        //    TextRange textRange = paragraph.AppendText("HÓA ĐƠN MUA HÀNG");
        //    // Định dạng font cho đoạn văn
        //    textRange.CharacterFormat.Bold = true; // Đặt chữ in đậm
        //    textRange.CharacterFormat.FontName = "Arial"; // Đặt tên font
        //    textRange.CharacterFormat.FontSize = 20; // Đặt kích thước font
        //    paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;



        //    //paragraph = section.AddParagraph();
        //    //TextRange textRange1 = paragraph.AppendText($"{lblMaDH.Text}\n");
        //    //TextRange textRange2 = paragraph.AppendText($"{lblHoTenKH.Text}\n");
        //    //TextRange textRange3 = paragraph.AppendText($"{lblDiaChi.Text}\n");

        //    ////paragraph.AppendText($"Mã Đơn Hàng: {lblMaDH.Text}\n");
        //    ////paragraph.AppendText($"Họ Tên Khách Hàng: {lblHoTenKH.Text}\n");
        //    ////paragraph.AppendText($"Địa Chỉ: {lblDiaChi.Text}\n");
        //    //paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
        //    //textRange1.CharacterFormat.FontName = "Time New Roman";
        //    //textRange1.CharacterFormat.FontSize = 12;
        //    //textRange2.CharacterFormat.FontName = "Time New Roman";
        //    //textRange2.CharacterFormat.FontSize = 12;
        //    //textRange3.CharacterFormat.FontName = "Time New Roman";
        //    //textRange3.CharacterFormat.FontSize = 12;
        //    ////paragraph.CharacterFormat.FontName = "Times New Roman";
        //    ////paragraph.CharacterFormat.FontSize = 12;

        //    //// Cột bên phải (được căn phải)
        //    //paragraph = section.AddParagraph();
        //    //TextRange textRange4 = paragraph.AppendText($"{lblNgayMuaHang.Text}\n");
        //    //TextRange textRange5 = paragraph.AppendText($"{lblEmail.Text}\n");
        //    //TextRange textRange6 = paragraph.AppendText($"{lblSĐT.Text}\n");
        //    ////paragraph.AppendText($"Ngày Mua Hàng: {lblNgayMuaHang.Text}\n");
        //    ////paragraph.AppendText($"Email: {lblEmail.Text}\n");
        //    ////paragraph.AppendText($"Số Điện Thoại: {lblSĐT.Text}\n");
        //    //paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
        //    //textRange4.CharacterFormat.FontName = "Time New Roman";
        //    //textRange4.CharacterFormat.FontSize = 12;
        //    //textRange5.CharacterFormat.FontName = "Time New Roman";
        //    //textRange5.CharacterFormat.FontSize = 12;
        //    //textRange6.CharacterFormat.FontName = "Time New Roman";
        //    //textRange6.CharacterFormat.FontSize = 12;
        //    ////paragraph.CharacterFormat.FontName = "Times New Roman";
        //    ////paragraph.CharacterFormat.FontSize = 12;

        //    paragraph = section.AddParagraph();
        //    paragraph.AppendText($"Mã Đơn Hàng: {lblMaDH.Text}    "); // Khoảng cách giữa các mục
        //    paragraph.AppendText($"Họ Tên Khách Hàng: {lblHoTenKH.Text}\n");
        //    paragraph.AppendText($"Địa Chỉ: {lblDiaChi.Text}");
        //    paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
        //    //paragraph.CharacterFormat.FontName = "Times New Roman";
        //    //paragraph.CharacterFormat.FontSize = 12;

        //    // Cột bên phải (được căn phải)
        //    paragraph = section.AddParagraph();
        //    paragraph.AppendText($"Ngày Mua Hàng: {lblNgayMuaHang.Text}    "); // Khoảng cách giữa các mục
        //    paragraph.AppendText($"Email: {lblEmail.Text}\n");
        //    paragraph.AppendText($"Số Điện Thoại: {lblSĐT.Text}");
        //    paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
        //    //paragraph.CharacterFormat.FontName = "Times New Roman";
        //    //paragraph.CharacterFormat.FontSize = 12;

        //    // Thêm bảng danh sách sản phẩm
        //    Table table = section.AddTable();
        //    table.ResetCells(dgvDsSanPham.RowCount + 1, dgvDsSanPham.ColumnCount);

        //    // Thêm tiêu đề cho các cột
        //    for (int i = 0; i < dgvDsSanPham.ColumnCount; i++)
        //    {
        //        table.Rows[0].Cells[i].AddParagraph().AppendText(dgvDsSanPham.Columns[i].HeaderText);
        //    }

        //    // Thêm dữ liệu sản phẩm vào bảng
        //    for (int i = 0; i < dgvDsSanPham.RowCount; i++)
        //    {
        //        for (int j = 0; j < dgvDsSanPham.ColumnCount; j++)
        //        {
        //            table.Rows[i + 1].Cells[j].AddParagraph().AppendText(dgvDsSanPham.Rows[i].Cells[j].Value.ToString());
        //        }
        //    }
        //    // Thêm phần tổng tiền dưới bảng danh sách sản phẩm
        //    paragraph = section.AddParagraph();
        //    //paragraph.AppendText($"\nTổng Tiền: {lblTongTien.Text}");
        //    TextRange totalTextRange = paragraph.AppendText($"\nTổng Tiền: {lblTongTien.Text}");
        //    totalTextRange.CharacterFormat.FontName = "Times New Roman";
        //    totalTextRange.CharacterFormat.FontSize = 12;

        //    // Đặt thư mục lưu file
        //    string folderPath = @"C:\HoaDon";  // Đặt đường dẫn thư mục mong muốn
        //    string filePath = System.IO.Path.Combine(folderPath, "HoaDon.docx");

        //    // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
        //    if (!System.IO.Directory.Exists(folderPath))
        //    {
        //        System.IO.Directory.CreateDirectory(folderPath);
        //    }

        //    // Lưu file Word
        //    document.SaveToFile(filePath, Spire.Doc.FileFormat.Docx);
        //    MessageBox.Show($"Hóa đơn đã được xuất thành file Word tại {filePath}");
        //}

        //public static void ExportToWord(DataGridView dgvDsSanPham, Label lblMaDH, Label lblNgayMuaHang, Label lblHoTenKH,
        //                         Label lblEmail, Label lblDiaChi, Label lblSĐT, Label lblTongTien)
        //{
        //    // Tạo đối tượng Document
        //    Document document = new Document();

        //    // Thêm một Section vào Document
        //    Section section = document.AddSection();

        //    // Tiêu đề hóa đơn
        //    Paragraph paragraph = section.AddParagraph();
        //    TextRange textRange = paragraph.AppendText("HÓA ĐƠN MUA HÀNG");
        //    textRange.CharacterFormat.Bold = true;
        //    textRange.CharacterFormat.FontName = "Arial";
        //    textRange.CharacterFormat.FontSize = 20;
        //    paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
        //    paragraph.Format.AfterSpacing = 12; // Thêm khoảng cách sau tiêu đề

        //    // Thêm thông tin khách hàng và đơn hàng dưới dạng bảng 2 cột
        //    Table infoTable = section.AddTable();
        //    infoTable.ResetCells(3, 2);  // 3 hàng, 2 cột

        //    // Cột bên trái (Thông tin khách hàng)
        //    infoTable.Rows[0].Cells[0].AddParagraph().AppendText($"Mã Đơn Hàng: {lblMaDH.Text}");
        //    infoTable.Rows[1].Cells[0].AddParagraph().AppendText($"Họ Tên Khách Hàng: {lblHoTenKH.Text}");
        //    infoTable.Rows[2].Cells[0].AddParagraph().AppendText($"Địa Chỉ: {lblDiaChi.Text}");

        //    // Cột bên phải (Thông tin đơn hàng)
        //    infoTable.Rows[0].Cells[1].AddParagraph().AppendText($"Ngày Mua Hàng: {lblNgayMuaHang.Text}");
        //    infoTable.Rows[1].Cells[1].AddParagraph().AppendText($"Email: {lblEmail.Text}");
        //    infoTable.Rows[2].Cells[1].AddParagraph().AppendText($"Số Điện Thoại: {lblSĐT.Text}");

        //    // Định dạng cho bảng thông tin khách hàng và đơn hàng
        //    foreach (TableRow row in infoTable.Rows)
        //    {
        //        foreach (TableCell cell in row.Cells)
        //        {
        //            TextRange text = cell.Paragraphs[0].AppendText(cell.Paragraphs[0].Text);
        //            text.CharacterFormat.FontName = "Times New Roman";
        //            text.CharacterFormat.FontSize = 12;
        //        }
        //    }

        //    // Xóa viền của các ô trong bảng thông tin
        //    foreach (TableRow row in infoTable.Rows)
        //    {
        //        foreach (TableCell cell in row.Cells)
        //        {
        //            cell.CellFormat.Borders.BorderType = Spire.Doc.Documents.BorderStyle.None;
        //        }
        //    }

        //    // Thêm bảng danh sách sản phẩm
        //    Table productTable = section.AddTable();
        //    productTable.ResetCells(dgvDsSanPham.RowCount + 1, dgvDsSanPham.ColumnCount);

        //    // Thêm tiêu đề cột của bảng sản phẩm
        //    for (int i = 0; i < dgvDsSanPham.ColumnCount; i++)
        //    {
        //        productTable.Rows[0].Cells[i].AddParagraph().AppendText(dgvDsSanPham.Columns[i].HeaderText);
        //        productTable.Rows[0].Cells[i].CellFormat.BackColor = System.Drawing.Color.LightGray;  // Màu nền cho tiêu đề
        //    }

        //    // Thêm dữ liệu sản phẩm vào bảng
        //    for (int i = 0; i < dgvDsSanPham.RowCount; i++)
        //    {
        //        for (int j = 0; j < dgvDsSanPham.ColumnCount; j++)
        //        {
        //            productTable.Rows[i + 1].Cells[j].AddParagraph().AppendText(dgvDsSanPham.Rows[i].Cells[j].Value.ToString());
        //        }
        //    }

        //    // Thêm phần tổng tiền dưới bảng danh sách sản phẩm
        //    paragraph = section.AddParagraph();
        //    TextRange totalTextRange = paragraph.AppendText($"\nTổng Tiền: {lblTongTien.Text}");
        //    totalTextRange.CharacterFormat.FontName = "Times New Roman";
        //    totalTextRange.CharacterFormat.FontSize = 12;
        //    totalTextRange.CharacterFormat.Bold = true;  // Để tổng tiền nổi bật

        //    // Đặt thư mục lưu file
        //    string folderPath = @"C:\HoaDon";
        //    string filePath = System.IO.Path.Combine(folderPath, "HoaDon.docx");

        //    // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
        //    if (!System.IO.Directory.Exists(folderPath))
        //    {
        //        System.IO.Directory.CreateDirectory(folderPath);
        //    }

        //    // Lưu file Word
        //    document.SaveToFile(filePath, Spire.Doc.FileFormat.Docx);
        //    MessageBox.Show($"Hóa đơn đã được xuất thành file Word tại {filePath}");
        //}

        //public static void ExportToWord(DataGridView dgvDsSanPham, Label lblMaDH, Label lblNgayMuaHang, Label lblHoTenKH,
        //                         Label lblEmail, Label lblDiaChi, Label lblSĐT, Label lblTongTien)
        //{
        //    // Tạo đối tượng Document
        //    Document document = new Document();

        //    // Thêm một Section vào Document
        //    Section section = document.AddSection();

        //    // Thêm tiêu đề cho hóa đơn
        //    Paragraph paragraph = section.AddParagraph();
        //    TextRange textRange = paragraph.AppendText("HÓA ĐƠN MUA HÀNG");
        //    textRange.CharacterFormat.Bold = true; // Đặt chữ in đậm
        //    textRange.CharacterFormat.FontName = "Arial"; // Đặt tên font
        //    textRange.CharacterFormat.FontSize = 20; // Đặt kích thước font
        //    paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
        //    paragraph.Format.AfterSpacing = 12; // Thêm khoảng cách sau tiêu đề

        //    // Thêm thông tin khách hàng và đơn hàng
        //    paragraph = section.AddParagraph();
        //    paragraph.AppendText($"Mã Đơn Hàng: {lblMaDH.Text}");
        //    paragraph.AppendText($"    Họ Tên Khách Hàng: {lblHoTenKH.Text}\n");
        //    paragraph.AppendText($"Địa Chỉ: {lblDiaChi.Text}\n");
        //    paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;

        //    // Thêm thông tin bên phải (Ngày Mua Hàng, Email, Số Điện Thoại)
        //    paragraph = section.AddParagraph();
        //    paragraph.AppendText($"Ngày Mua Hàng: {lblNgayMuaHang.Text}");
        //    paragraph.AppendText($"    Email: {lblEmail.Text}\n");
        //    paragraph.AppendText($"Số Điện Thoại: {lblSĐT.Text}");
        //    paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
        //    paragraph.Format.AfterSpacing = 12; // Thêm khoảng cách sau thông tin khách hàng

        //    // Thêm bảng danh sách sản phẩm
        //    Table table = section.AddTable();
        //    table.ResetCells(dgvDsSanPham.RowCount + 1, dgvDsSanPham.ColumnCount);

        //    // Thêm tiêu đề cho các cột trong bảng
        //    for (int i = 0; i < dgvDsSanPham.ColumnCount; i++)
        //    {
        //        table.Rows[0].Cells[i].AddParagraph().AppendText(dgvDsSanPham.Columns[i].HeaderText);
        //    }

        //    // Thêm dữ liệu sản phẩm vào bảng
        //    for (int i = 0; i < dgvDsSanPham.RowCount; i++)
        //    {
        //        for (int j = 0; j < dgvDsSanPham.ColumnCount; j++)
        //        {
        //            table.Rows[i + 1].Cells[j].AddParagraph().AppendText(dgvDsSanPham.Rows[i].Cells[j].Value.ToString());
        //        }
        //    }

        //    // Xóa viền của tất cả các ô trong bảng
        //    foreach (TableRow row in table.Rows)
        //    {
        //        foreach (TableCell cell in row.Cells)
        //        {
        //            cell.CellFormat.Borders.BorderType = Spire.Doc.Documents.BorderStyle.None;
        //        }
        //    }

        //    // Thêm phần tổng tiền dưới bảng danh sách sản phẩm
        //    paragraph = section.AddParagraph();
        //    TextRange totalTextRange = paragraph.AppendText($"\nTổng Tiền: {lblTongTien.Text}");
        //    totalTextRange.CharacterFormat.FontName = "Times New Roman";
        //    totalTextRange.CharacterFormat.FontSize = 12;
        //    paragraph.Format.AfterSpacing = 12; // Thêm khoảng cách dưới phần tổng tiền

        //    // Đặt thư mục lưu file
        //    string folderPath = @"C:\HoaDon";  // Đặt đường dẫn thư mục mong muốn
        //    string filePath = System.IO.Path.Combine(folderPath, "HoaDon.docx");

        //    // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
        //    if (!System.IO.Directory.Exists(folderPath))
        //    {
        //        System.IO.Directory.CreateDirectory(folderPath);
        //    }

        //    // Lưu file Word
        //    document.SaveToFile(filePath, Spire.Doc.FileFormat.Docx);
        //    MessageBox.Show($"Hóa đơn đã được xuất thành file Word tại {filePath}");
        //}


        public static void ExportToWord(DataGridView dgvDsSanPham, Label lblMaDH, Label lblNgayMuaHang, Label lblHoTenKH,
                                 Label lblEmail, Label lblDiaChi, Label lblSĐT, Label lblTongTien)
        {
            // Tạo đối tượng Document
            Document document = new Document();

            // Thêm một Section vào Document
            Section section = document.AddSection();

            // Thêm tiêu đề cho hóa đơn
            Paragraph paragraph = section.AddParagraph();
            TextRange textRange = paragraph.AppendText("HÓA ĐƠN MUA HÀNG");
            textRange.CharacterFormat.Bold = true; // Đặt chữ in đậm
            textRange.CharacterFormat.FontName = "Arial"; // Đặt tên font
            textRange.CharacterFormat.FontSize = 20; // Đặt kích thước font
            paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            paragraph.Format.AfterSpacing = 12; // Thêm khoảng cách sau tiêu đề

            // Thêm thông tin đơn hàng và khách hàng vào cột trái và phải
            // Thêm mã đơn hàng và ngày mua hàng vào cột trái và phải
            paragraph = section.AddParagraph();
            paragraph.AppendText($"Mã Đơn Hàng: {lblMaDH.Text}");
            paragraph.AppendText("\t\t\t\t"); // Khoảng cách giữa các thông tin
            paragraph.AppendText($"Ngày Mua Hàng: {lblNgayMuaHang.Text}");
            paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
            paragraph.Format.AfterSpacing = 12;
            paragraph = section.AddParagraph();
            paragraph.AppendText($"Họ Tên KH: {lblHoTenKH.Text}");
            paragraph.AppendText("\t\t\t");
            paragraph.AppendText($"Email: {lblEmail.Text}");
            paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
            paragraph.Format.AfterSpacing = 12;
            paragraph = section.AddParagraph();
            paragraph.AppendText($"Địa Chỉ: {lblDiaChi.Text}");
            paragraph.AppendText("\t\t\t\t");
            paragraph.AppendText($"Số Điện Thoại: {lblSĐT.Text}");
            paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
            paragraph.Format.AfterSpacing = 12;

            // Thêm bảng danh sách sản phẩm
            Table table = section.AddTable();
            table.ResetCells(dgvDsSanPham.RowCount + 1, dgvDsSanPham.ColumnCount);

            // Thêm tiêu đề cho các cột trong bảng
            for (int i = 0; i < dgvDsSanPham.ColumnCount; i++)
            {
                table.Rows[0].Cells[i].AddParagraph().AppendText(dgvDsSanPham.Columns[i].HeaderText);
            }

            // Thêm dữ liệu sản phẩm vào bảng
            for (int i = 0; i < dgvDsSanPham.RowCount; i++)
            {
                for (int j = 0; j < dgvDsSanPham.ColumnCount; j++)
                {
                    table.Rows[i + 1].Cells[j].AddParagraph().AppendText(dgvDsSanPham.Rows[i].Cells[j].Value.ToString());
                }
            }
            paragraph.Format.AfterSpacing = 12;

            // Thêm phần tổng tiền dưới bảng danh sách sản phẩm
            paragraph = section.AddParagraph();
            TextRange totalTextRange = paragraph.AppendText($"\nTổng Tiền: {lblTongTien.Text}");
            totalTextRange.CharacterFormat.FontName = "Times New Roman";
            totalTextRange.CharacterFormat.FontSize = 12;
            paragraph.Format.AfterSpacing = 12; // Thêm khoảng cách dưới phần tổng tiền

            // Đặt thư mục lưu file
            string folderPath = @"C:\HoaDon";  // Đặt đường dẫn thư mục mong muốn
            string filePath = System.IO.Path.Combine(folderPath, "HoaDon.docx");

            // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            // Lưu file Word
            document.SaveToFile(filePath, Spire.Doc.FileFormat.Docx);
            MessageBox.Show($"Hóa đơn đã được xuất thành file Word tại {filePath}");
        }

        //public static void ExportToPdf(DataGridView dgvDsSanPham, Label lblMaDH, Label lblNgayMuaHang, Label lblHoTenKH,
        //                                Label lblEmail, Label lblDiaChi, Label lblSĐT, Label lblTongTien)
        //{
        //    // Tạo đối tượng PdfDocument
        //    PdfDocument pdf = new PdfDocument();

        //    // Tạo một trang mới
        //    PdfPageBase page = pdf.Pages.Add();

        //    // Thêm tiêu đề cho hóa đơn
        //    page.Canvas.DrawString("HÓA ĐƠN MUA HÀNG", new PdfFont(PdfFontFamily.Helvetica, 18), PdfBrushes.Black, 200, 30);

        //    // Thêm thông tin khách hàng và đơn hàng
        //    page.Canvas.DrawString($"Mã Đơn Hàng: {lblMaDH.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 60);
        //    page.Canvas.DrawString($"Ngày Mua Hàng: {lblNgayMuaHang.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 80);
        //    page.Canvas.DrawString($"Họ Tên Khách Hàng: {lblHoTenKH.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 100);
        //    page.Canvas.DrawString($"Email: {lblEmail.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 120);
        //    page.Canvas.DrawString($"Địa Chỉ: {lblDiaChi.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 140);
        //    page.Canvas.DrawString($"Số Điện Thoại: {lblSĐT.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 160);
        //    page.Canvas.DrawString($"Tổng Tiền: {lblTongTien.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 180);

        //    // Thêm bảng danh sách sản phẩm
        //    float yPos = 220;
        //    foreach (DataGridViewRow row in dgvDsSanPham.Rows)
        //    {
        //        page.Canvas.DrawString($"{row.Cells[0].Value} - {row.Cells[1].Value} x {row.Cells[2].Value} = {row.Cells[3].Value}",
        //            new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, yPos);
        //        yPos += 20;
        //    }

        //    // Đặt thư mục lưu file
        //    string folderPath = @"C:\HoaDon";  // Đặt đường dẫn thư mục mong muốn
        //    string filePath = System.IO.Path.Combine(folderPath, "HoaDon.pdf");

        //    // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
        //    if (!System.IO.Directory.Exists(folderPath))
        //    {
        //        System.IO.Directory.CreateDirectory(folderPath);
        //    }

        //    // Lưu file PDF
        //    pdf.SaveToFile(filePath);
        //    MessageBox.Show($"Hóa đơn đã được xuất thành file PDF tại {filePath}");
        //}

        //public static void ExportToPdf(DataGridView dgvDsSanPham, Label lblMaDH, Label lblNgayMuaHang, Label lblHoTenKH,
        //                        Label lblEmail, Label lblDiaChi, Label lblSĐT, Label lblTongTien)
        //{
        //    // Tạo đối tượng PdfDocument
        //    PdfDocument pdf = new PdfDocument();

        //    // Tạo một trang mới
        //    PdfPageBase page = pdf.Pages.Add();

        //    // Thêm tiêu đề cho hóa đơn
        //    page.Canvas.DrawString("HÓA ĐƠN MUA HÀNG", new PdfFont(PdfFontFamily.Helvetica, 20), PdfBrushes.Black, 200, 30);

        //    // Thêm thông tin khách hàng và đơn hàng
        //    page.Canvas.DrawString($"Mã Đơn Hàng: {lblMaDH.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 60);
        //    page.Canvas.DrawString($"Ngày Mua Hàng: {lblNgayMuaHang.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 80);
        //    page.Canvas.DrawString($"Họ Tên Khách Hàng: {lblHoTenKH.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 100);
        //    page.Canvas.DrawString($"Email: {lblEmail.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 120);
        //    page.Canvas.DrawString($"Địa Chỉ: {lblDiaChi.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 140);
        //    page.Canvas.DrawString($"Số Điện Thoại: {lblSĐT.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, 160);

        //    // Thêm bảng danh sách sản phẩm
        //    float yPos = 200;
        //    float xPos = 10;
        //    float rowHeight = 20;

        //    // Tạo các cột cho bảng
        //    page.Canvas.DrawString("Tên Sản Phẩm", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, xPos, yPos);
        //    page.Canvas.DrawString("Số Lượng", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, xPos + 150, yPos);
        //    page.Canvas.DrawString("Giá", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, xPos + 200, yPos);
        //    page.Canvas.DrawString("Tổng Tiền", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, xPos + 250, yPos);
        //    yPos += rowHeight;

        //    // Thêm dữ liệu sản phẩm vào bảng
        //    foreach (DataGridViewRow row in dgvDsSanPham.Rows)
        //    {
        //        page.Canvas.DrawString(row.Cells[0].Value.ToString(), new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, xPos, yPos);
        //        page.Canvas.DrawString(row.Cells[1].Value.ToString(), new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, xPos + 150, yPos);
        //        page.Canvas.DrawString(row.Cells[2].Value.ToString(), new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, xPos + 200, yPos);
        //        page.Canvas.DrawString(row.Cells[3].Value.ToString(), new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, xPos + 250, yPos);
        //        yPos += rowHeight;
        //    }

        //    // Thêm phần tổng tiền dưới bảng danh sách sản phẩm
        //    page.Canvas.DrawString($"Tổng Tiền: {lblTongTien.Text}", new PdfFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, 10, yPos + 20);

        //    // Đặt thư mục lưu file
        //    string folderPath = @"C:\HoaDon";  // Đặt đường dẫn thư mục mong muốn
        //    string filePath = System.IO.Path.Combine(folderPath, "HoaDon.pdf");

        //    // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
        //    if (!System.IO.Directory.Exists(folderPath))
        //    {
        //        System.IO.Directory.CreateDirectory(folderPath);
        //    }

        //    // Lưu file PDF
        //    pdf.SaveToFile(filePath);
        //    MessageBox.Show($"Hóa đơn đã được xuất thành file PDF tại {filePath}");
        //}

        //public static void ExportToPdf(DataGridView dgvDsSanPham, Label lblMaDH, Label lblNgayMuaHang, Label lblHoTenKH,
        //                        Label lblEmail, Label lblDiaChi, Label lblSĐT, Label lblTongTien)
        //{
        //    // Tạo đối tượng PdfDocument
        //    PdfDocument pdf = new PdfDocument();

        //    // Tạo một trang mới
        //    PdfPageBase page = pdf.Pages.Add();

        //    // Thêm tiêu đề cho hóa đơn
        //    page.Canvas.DrawString("HÓA ĐƠN MUA HÀNG", new PdfFont("Times New Roman", 18), PdfBrushes.Black, 200, 30);

        //    // Thêm thông tin khách hàng và đơn hàng
        //    page.Canvas.DrawString($"Mã Đơn Hàng: {lblMaDH.Text}", new PdfFont("Times New Roman", 12), PdfBrushes.Black, 10, 60);
        //    page.Canvas.DrawString($"Ngày Mua Hàng: {lblNgayMuaHang.Text}", new PdfFont("Times New Roman", 12), PdfBrushes.Black, 300, 60);
        //    page.Canvas.DrawString($"Họ Tên KH: {lblHoTenKH.Text}", new PdfFont("Times New Roman", 12), PdfBrushes.Black, 10, 80);
        //    page.Canvas.DrawString($"Email: {lblEmail.Text}", new PdfFont("Times New Roman", 12), PdfBrushes.Black, 300, 80);
        //    page.Canvas.DrawString($"Địa Chỉ: {lblDiaChi.Text}", new PdfFont("Times New Roman", 12), PdfBrushes.Black, 10, 100);
        //    page.Canvas.DrawString($"Số Điện Thoại: {lblSĐT.Text}", new PdfFont("Times New Roman", 12), PdfBrushes.Black, 300, 100);

        //    // Thêm tiêu đề cho bảng danh sách sản phẩm
        //    float yPos = 140;
        //    float xPos = 10;
        //    float rowHeight = 20;
        //    float colWidth1 = 150; // Độ rộng cột 1
        //    float colWidth2 = 100; // Độ rộng cột 2
        //    float colWidth3 = 100; // Độ rộng cột 3
        //    float colWidth4 = 100; // Độ rộng cột 4

        //    // Tạo các cột cho bảng
        //    page.Canvas.DrawString("Tên Sách", new PdfFont("Times New Roman", 12), PdfBrushes.Black, xPos, yPos);
        //    page.Canvas.DrawString("Số Lượng", new PdfFont("Times New Roman", 12), PdfBrushes.Black, xPos + colWidth1, yPos);
        //    page.Canvas.DrawString("Đơn Giá", new PdfFont("Times New Roman", 12), PdfBrushes.Black, xPos + colWidth1 + colWidth2, yPos);
        //    page.Canvas.DrawString("Thành Tiền", new PdfFont("Times New Roman", 12), PdfBrushes.Black, xPos + colWidth1 + colWidth2 + colWidth3, yPos);

        //    yPos += rowHeight;

        //    // Thêm dữ liệu sản phẩm vào bảng
        //    foreach (DataGridViewRow row in dgvDsSanPham.Rows)
        //    {
        //        page.Canvas.DrawString(row.Cells[0].Value.ToString(), new PdfFont("Times New Roman", 12), PdfBrushes.Black, xPos, yPos);
        //        page.Canvas.DrawString(row.Cells[1].Value.ToString(), new PdfFont("Times New Roman", 12), PdfBrushes.Black, xPos + colWidth1, yPos);
        //        page.Canvas.DrawString(row.Cells[2].Value.ToString(), new PdfFont("Times New Roman", 12), PdfBrushes.Black, xPos + colWidth1 + colWidth2, yPos);
        //        page.Canvas.DrawString(row.Cells[3].Value.ToString(), new PdfFont("Times New Roman", 12), PdfBrushes.Black, xPos + colWidth1 + colWidth2 + colWidth3, yPos);
        //        yPos += rowHeight;
        //    }

        //    // Thêm phần tổng tiền dưới bảng danh sách sản phẩm
        //    page.Canvas.DrawString($"Tổng Tiền: {lblTongTien.Text}", new PdfFont("Times New Roman", 12), PdfBrushes.Black, 10, yPos + 20);

        //    // Đặt thư mục lưu file
        //    string folderPath = @"C:\HoaDon";  // Đặt đường dẫn thư mục mong muốn
        //    string filePath = System.IO.Path.Combine(folderPath, "HoaDon.pdf");

        //    // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
        //    if (!System.IO.Directory.Exists(folderPath))
        //    {
        //        System.IO.Directory.CreateDirectory(folderPath);
        //    }

        //    // Lưu file PDF
        //    pdf.SaveToFile(filePath);
        //    MessageBox.Show($"Hóa đơn đã được xuất thành file PDF tại {filePath}");
        //}

        public static void ExportToPdf(DataGridView dgvDsSanPham, Label lblMaDH, Label lblNgayMuaHang, Label lblHoTenKH,
                                Label lblEmail, Label lblDiaChi, Label lblSĐT, Label lblTongTien)
        {
            // Tạo đối tượng PdfDocument
            PdfDocument pdf = new PdfDocument();

            // Tạo một trang mới
            PdfPageBase page = pdf.Pages.Add();

            // Sử dụng PdfFont với PdfFontFamily và kích thước font
            PdfFont titleFont = new PdfFont(PdfFontFamily.Helvetica, 18);
            PdfFont normalFont = new PdfFont(PdfFontFamily.Helvetica, 12);

            // Thêm tiêu đề cho hóa đơn
            page.Canvas.DrawString("HÓA ĐƠN MUA HÀNG", titleFont, PdfBrushes.Black, 200, 30);

            // Thêm thông tin khách hàng và đơn hàng
            page.Canvas.DrawString($"Mã Đơn Hàng: {lblMaDH.Text}", normalFont, PdfBrushes.Black, 10, 60);
            page.Canvas.DrawString($"Ngày Mua Hàng: {lblNgayMuaHang.Text}", normalFont, PdfBrushes.Black, 300, 60);
            page.Canvas.DrawString($"Họ Tên KH: {lblHoTenKH.Text}", normalFont, PdfBrushes.Black, 10, 80);
            page.Canvas.DrawString($"Email: {lblEmail.Text}", normalFont, PdfBrushes.Black, 300, 80);
            page.Canvas.DrawString($"Địa Chỉ: {lblDiaChi.Text}", normalFont, PdfBrushes.Black, 10, 100);
            page.Canvas.DrawString($"Số Điện Thoại: {lblSĐT.Text}", normalFont, PdfBrushes.Black, 300, 100);

            // Thêm tiêu đề cho bảng danh sách sản phẩm
            float yPos = 140;
            float xPos = 10;
            float rowHeight = 20;
            float colWidth1 = 150; // Độ rộng cột 1
            float colWidth2 = 100; // Độ rộng cột 2
            float colWidth3 = 100; // Độ rộng cột 3
            float colWidth4 = 100; // Độ rộng cột 4

            // Tạo các cột cho bảng
            page.Canvas.DrawString("Tên Sách", normalFont, PdfBrushes.Black, xPos, yPos);
            page.Canvas.DrawString("Số Lượng", normalFont, PdfBrushes.Black, xPos + colWidth1, yPos);
            page.Canvas.DrawString("Đơn Giá", normalFont, PdfBrushes.Black, xPos + colWidth1 + colWidth2, yPos);
            page.Canvas.DrawString("Thành Tiền", normalFont, PdfBrushes.Black, xPos + colWidth1 + colWidth2 + colWidth3, yPos);

            yPos += rowHeight;

            // Thêm dữ liệu sản phẩm vào bảng
            foreach (DataGridViewRow row in dgvDsSanPham.Rows)
            {
                page.Canvas.DrawString(row.Cells[0].Value.ToString(), normalFont, PdfBrushes.Black, xPos, yPos);
                page.Canvas.DrawString(row.Cells[1].Value.ToString(), normalFont, PdfBrushes.Black, xPos + colWidth1, yPos);
                page.Canvas.DrawString(row.Cells[2].Value.ToString(), normalFont, PdfBrushes.Black, xPos + colWidth1 + colWidth2, yPos);
                page.Canvas.DrawString(row.Cells[3].Value.ToString(), normalFont, PdfBrushes.Black, xPos + colWidth1 + colWidth2 + colWidth3, yPos);
                yPos += rowHeight;
            }

            // Thêm phần tổng tiền dưới bảng danh sách sản phẩm
            page.Canvas.DrawString($"Tổng Tiền: {lblTongTien.Text}", normalFont, PdfBrushes.Black, 10, yPos + 20);

            // Đặt thư mục lưu file
            string folderPath = @"C:\HoaDon";  // Đặt đường dẫn thư mục mong muốn
            string filePath = System.IO.Path.Combine(folderPath, "HoaDon.pdf");

            // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            // Lưu file PDF
            pdf.SaveToFile(filePath);
            MessageBox.Show($"Hóa đơn đã được xuất thành file PDF tại {filePath}");
        }


    }
}
