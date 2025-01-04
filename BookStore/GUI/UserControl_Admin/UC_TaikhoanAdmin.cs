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
    public partial class UC_TaikhoanAdmin : UserControl
    {
        TaiKhoanBUS taiKhoanBUS = new TaiKhoanBUS();
        private string Id;

        public UC_TaikhoanAdmin()
        {
            InitializeComponent();
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {

        }

        

        private void btnTimkiemTK_Click(object sender, EventArgs e)
        {

        }

        private void datagridviewTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
       
    }
}
