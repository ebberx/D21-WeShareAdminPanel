using D21WeShareAdminPanel.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D21WeShareAdminPanel.ViewModel
{
    public class LoginDialogViewModel
    {
        public string? sessionToken;
        
        public void Login(string user, string password) {
            sessionToken = APIRequester.Login(user, password);
            Debug.WriteLine(sessionToken);
        }

    }
}
