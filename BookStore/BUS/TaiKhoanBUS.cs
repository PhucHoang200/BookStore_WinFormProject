using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DTO;

namespace BUS
{
    public class TaiKhoanBUS
    {
        private TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();

        public bool CheckLogin(string email, string password, out string role, out int idTaiKhoan)
        {
            // Lấy thông tin tài khoản từ DAL
            TaiKhoan taiKhoan = taiKhoanDAL.GetTaiKhoanByEmail(email);

            if (taiKhoan == null)
            {
                role = null;
                idTaiKhoan = 0;
                return false; // Không tìm thấy tài khoản
            }

            // Kiểm tra mật khẩu
            byte[] dbHash = taiKhoan.MatKhau;
            byte[] userHash = HashPassword(password);
            if (!CompareHashes(dbHash, userHash))
            {
                role = null;
                idTaiKhoan = 0;
                return false; // Mật khẩu sai
            }

            // Lấy vai trò và Id tài khoản
            role = taiKhoan.VaiTro.TenVaiTro; // Giả sử thuộc tính VaiTro lưu thông tin vai trò
            idTaiKhoan = taiKhoan.Id; // Lấy Id tài khoản
            return true;
        }

        public bool CheckEmailExist(string email)
        {
            using (var context = new BookStoreDBEntities()) 
            {
                // Kiểm tra xem email có tồn tại trong bảng TaiKhoan không
                return context.TaiKhoans.Any(tk => tk.Email == email);
            }
        }

        // Cập nhật mật khẩu mới (băm SHA256)
        public bool UpdatePassword(string email, string newPasswordHash)
        {
            using (var context = new BookStoreDBEntities())
            {
                var taiKhoan = context.TaiKhoans.SingleOrDefault(tk => tk.Email == email);
                if (taiKhoan == null)
                {
                    return false; // Không tìm thấy tài khoản
                }

                taiKhoan.MatKhau = Convert.FromBase64String(newPasswordHash);
                context.SaveChanges();
                return true;
            }
        }

        // Băm mật khẩu bằng SHA256
        public string HashPasswordToBase64(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }

        private byte[] HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                return sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool CompareHashes(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length) return false;
            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i]) return false;
            }
            return true;
        }

        public int GetIdNhanVienByEmail(string email)
        {
            return taiKhoanDAL.GetIdNhanVienByEmail(email);
        }

        public List<TaiKhoan> GetAllTaiKhoan()
        {
            return taiKhoanDAL.GetAllTaiKhoan();
        }

        public List<TaiKhoan> GetTaiKhoanChuaCoNguoiDung()
        {
            return taiKhoanDAL.GetTaiKhoanChuaCoNguoiDung();
        }

        public string AddTaiKhoan(string email, string matKhau, int idVaiTro)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return "Email không được để trống!";
            }

            if (string.IsNullOrWhiteSpace(matKhau))
            {
                return "Mật khẩu không được để trống!";
            }

            // Kiểm tra trùng lặp email
            var existingTaiKhoan = taiKhoanDAL.GetAllTaiKhoan().FirstOrDefault(i => i.Email == email);
            if (existingTaiKhoan != null)
            {
                return "Email đã tồn tại.";
            }

            // Băm mật khẩu
            byte[] passHash = HashPassword(matKhau);

            // Tạo tài khoản mới
            var taiKhoan = new TaiKhoan
            {
                Email = email,
                MatKhau = passHash,
                IdVaiTro = idVaiTro
            };

            // Thêm vào cơ sở dữ liệu
            taiKhoanDAL.AddTaiKhoan(taiKhoan);

            return "Thêm tài khoản thành công!";

        }

        public string UpdateTaiKhoan(int id, string email, int idVaiTro)
        {
            var existingTaiKhoan = taiKhoanDAL.GetAllTaiKhoan().FirstOrDefault(i => i.Id == id);
            if (existingTaiKhoan != null)
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return "Không được để trống thông tin!";
                }

                var existingEmail = taiKhoanDAL.GetAllTaiKhoan()
                                            .FirstOrDefault(i => i.Email == email && i.Id != id);
                if (existingEmail != null)
                {
                    return "Tên tài khoản đã tồn tại.";
                }

                var taiKhoan = new TaiKhoan
                {
                    Id = id,
                    Email = email,
                    IdVaiTro = idVaiTro
                };

                bool kq = taiKhoanDAL.UpdateTaiKhoan(taiKhoan);
                if (kq)
                {
                    return "Cập nhật thông tin tài khoản thành công";
                }
                else
                {
                    return "Cập nhật thông tin tài khoản thất bại";
                }
            }
            else
            {
                return "Mã tài khoản không tồn tại";
            }
        }

        public string DeleteTaiKhoan(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id) || !int.TryParse(Id, out int parsedId))
            {
                return "Mã tài khoản không hợp lệ";
            }

            bool kq = taiKhoanDAL.DeleteTaiKhoan(parsedId);
            if (kq)
            {
                return "Xóa tài khoản thành công";
            }
            else
            {
                return "Xóa tài khoản thất bại!";
            }
        }

        public List<TaiKhoan> FindTaiKhoanByNameOfEmployee(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new List<TaiKhoan>();
            }
            return taiKhoanDAL.FindTaiKhoanByNameOfEmployee(name);
        }
    }
}
