using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using D21WeShareAdminPanel.ViewModel;


namespace D21WeShareAdminPanel.View
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        LoginDialogViewModel viewModel;
        public LoginDialog() {
            InitializeComponent();
            viewModel = new LoginDialogViewModel();
        }

        private void OnLogin(object sender, RoutedEventArgs e) {
            viewModel.Login(userBox.Text, passwordBox.Text);                                                                                                                                                                                                                                                                                                                                                                                                                                     
            
            if (viewModel.sessionToken != null) {
                MainWindow mainWindow = new MainWindow(viewModel.sessionToken);
                mainWindow.Show();
                this.Close();
            }
            
        }
    }
}
