﻿using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class VaiTroBUS
    {
        private VaiTroDAL vaiTroDAL = new VaiTroDAL();

        public List<VaiTro> GetAllVaiTro()
        {
            return vaiTroDAL.GetAllVaiTro();
        }
    }
}
