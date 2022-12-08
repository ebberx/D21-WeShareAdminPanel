using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D21WeShareAdminPanel.Model.DTO;
using D21WeShareAdminPanel.Model;
using System.Diagnostics;
using System.Text.Json;
using NNTPClient.Model;

namespace D21WeShareAdminPanel.ViewModel
{
    public class AddUserDialogViewModel : Bindable
    {
        public List<SecurityQuestionDTO> questions { get { return _questions!; } set { _questions = value; propertyIsChanged(); } }
        private List<SecurityQuestionDTO>? _questions;

        public async void AddUser(NewUserDTO user) {
            await APIRequester.AddUser(user);
        }

        public async void GetQuestions() {
            string res = await APIRequester.Get("https://api-wan-kenobi.ovh/api/SecurityQuestion/GetAllSecurityQuestions", "");

            try {
                questions = JsonSerializer.Deserialize<List<SecurityQuestionDTO>>(res)!;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
