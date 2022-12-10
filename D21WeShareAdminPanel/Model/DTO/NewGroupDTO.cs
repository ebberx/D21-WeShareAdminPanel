using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D21WeShareAdminPanel.Model.DTO
{
    public class NewGroupDTO
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public int userID { get; set; }
        public bool isPublic { get; set; }
        
    }
}
