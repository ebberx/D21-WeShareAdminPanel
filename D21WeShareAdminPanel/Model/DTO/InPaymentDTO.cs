using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D21WeShareAdminPanel.Model.DTO
{
    public class InPaymentDTO {

        public int transactionID { get; set; }
        public int paidAmount { get; set; }
        public int userGroupID { get; set; }
        public int groupID { get; set; }
        public string? groupName { get; set; }
        public string? groupDescription { get; set; }
        public int userID { get; set; }
        public string? userName { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? phoneNumber { get; set; }
        public string? email { get; set; }
    }
}
