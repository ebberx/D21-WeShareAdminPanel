using D21WeShareAdminPanel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D21WeShareAdminPanel.ViewModel
{
    public class LoginDialogViewModel
    {
        public void Login(string user, string password) {
            APIRequester.RequestToken(user, password);
        }

    }
}
