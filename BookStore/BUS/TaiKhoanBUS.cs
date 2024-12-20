﻿using DAL;
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

        public bool CheckLogin(string email, string password, out string role)
        {
            // Lấy thông tin tài khoản từ DAL
            TaiKhoan taiKhoan = taiKhoanDAL.GetTaiKhoanByEmail(email);

            if (taiKhoan == null)
            {
                role = null;
                return false; // Không tìm thấy tài khoản
            }

            // Kiểm tra mật khẩu (Giả sử đã băm mật khẩu và so sánh)
            byte[] dbHash = taiKhoan.MatKhau;
            byte[] userHash = HashPassword(password);
            if (!CompareHashes(dbHash, userHash))
            {
                role = null;
                return false; // Mật khẩu sai
            }

            // Kiểm tra vai trò
            role = taiKhoan.VaiTro.TenVaiTro; // Giả sử thuộc tính NhomNguoiDung lưu thông tin vai trò
            return true;
        }        

        public bool CheckEmailExist(string email)
        {
            using (var context = new BookStoreDBEntities()) // Thay 'YourDbContext' bằng tên context của bạn
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
    }
}
