using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace D21WeShareAdminPanel.Model.DTO
{
    public class ExpenseDTO
    {
        public int expenseId { get; set; }
        public int amount { get; set; }
        public int userId { get; set; }
        public int groupId { get; set; }
        public string? groupName { get; set; }
        public string? userName { get; set; }
        public string? phoneNumber { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? expenseName { get; set; }
        public string? expenseDescription { get; set; }
        public string? groupDescription { get; set; }
        public string? datePaid { get; set; }
        public string? receiptPicture { get; set; }
    }
}
