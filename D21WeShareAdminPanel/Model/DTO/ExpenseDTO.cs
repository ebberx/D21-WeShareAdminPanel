using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D21WeShareAdminPanel.Model.DTO
{
    public class ExpenseDTO
    {
        public int expenseId { get; set; }
        public int userGroupId { get; set; }
        public int amount { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? datePaid { get; set; }
        public string? receiptPicture { get; set; }
        public string? userGroup { get; set; }
        
    }
}
