using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D21WeShareAdminPanel.Model.DTO
{
    public class GroupDetailsDTO {
        public int userGroupId { get; set; }
        public int userId { get; set; }
        public int groupId { get; set; }
        public bool isOwner { get; set; }
        public GroupDTO? group { get; set; }
        public UserDTO? user { get; set; }
    }
}
