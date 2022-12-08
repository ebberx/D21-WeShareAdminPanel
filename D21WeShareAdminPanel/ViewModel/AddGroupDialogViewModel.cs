using D21WeShareAdminPanel.Model.DTO;
using D21WeShareAdminPanel.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace D21WeShareAdminPanel.ViewModel
{
    public class AddGroupDialogViewModel
    {
        public async void AddGroup(NewGroupDTO group) {
            await APIRequester.AddGroup(group);
        }

        public async void GetQuestions() {
            string res = await APIRequester.Get("https://api-wan-kenobi.ovh/api/SecurityQuestion/GetAllSecurityQuestions", "");

        }
    }
}
