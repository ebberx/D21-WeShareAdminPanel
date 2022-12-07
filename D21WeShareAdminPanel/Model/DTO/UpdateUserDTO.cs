using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D21WeShareAdminPanel.Model.DTO
{
    public class UpdateUserDTO {
        public int userId { get; set; }
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
        public bool isDisabled { get; set; }
        public bool isBlacklisted { get; set; }

        public UpdateUserDTO SetUpdateUserDTO(UserDTO user) {
            // Fill out UpdateUserDTO fields with UserDTO fields
            userId = user.userId;
            userName = user.userName;
            phoneNumber = user.phoneNumber;
            firstName = user.firstName;
            lastName = user.lastName;
            email = user.email;
            password = user.password;
            isAdmin = user.isAdmin;
            address = user.address;
            questionId = user.questionId;
            securityAnswer = user.securityAnswer;
            isDisabled = user.isDisabled;
            isBlacklisted = user.isBlacklisted;

            return this;
        }
    }
}
