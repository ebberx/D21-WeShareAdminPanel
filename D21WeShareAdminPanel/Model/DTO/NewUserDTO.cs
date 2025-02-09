﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D21WeShareAdminPanel.Model.DTO
{
    public class NewUserDTO {

        public string? userName { get; set; }
        public string? phoneNumber { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public bool isAdmin { get; set; }
        public string? address { get; set; }
        public int questionId { get; set; }
        public string? securityAnswer { get; set; }
    }
}
