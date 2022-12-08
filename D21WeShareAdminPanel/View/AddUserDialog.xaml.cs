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
using D21WeShareAdminPanel.Model.DTO;
using D21WeShareAdminPanel.ViewModel;

namespace D21WeShareAdminPanel.View
{
    /// <summary>
    /// Interaction logic for AddUserDialog.xaml
    /// </summary>
    public partial class AddUserDialog : Window
    {
        AddUserDialogViewModel viewModel;

        public AddUserDialog() {
            InitializeComponent();

            viewModel = new AddUserDialogViewModel();
            this.DataContext = viewModel;

            viewModel.GetQuestions();
        }

        private void onCreateUser(object sender, RoutedEventArgs e) {

            if (String.IsNullOrEmpty(inputName.Text)) {
                MessageBox.Show("Invalid user name");
                return;
            }
            if (String.IsNullOrEmpty(inputPhone.Text)) {
                MessageBox.Show("Invalid phone number");
                return;
            }
            if (String.IsNullOrEmpty(inputFirstName.Text)) {
                MessageBox.Show("Invalid first name");
                return;
            }
            if (String.IsNullOrEmpty(inputLastName.Text)) {
                MessageBox.Show("Invalid last name");
                return;
            }
            if (String.IsNullOrEmpty(inputEmail.Text)) {
                MessageBox.Show("Invalid email");
                return;
            }
            if (String.IsNullOrEmpty(inputPassword.Text)) {
                MessageBox.Show("Invalid password");
                return;
            }
            if (String.IsNullOrEmpty(inputAddress.Text)) {
                MessageBox.Show("Invalid address");
                return;
            }
            if (String.IsNullOrEmpty(inputAnswer.Text)) {
                MessageBox.Show("Invalid security answer");
                return;
            }

            NewUserDTO user = new NewUserDTO();
            
            user.userName = inputName.Text;
            user.phoneNumber = inputPhone.Text;
            user.firstName = inputFirstName.Text;
            user.lastName = inputLastName.Text;
            user.email = inputEmail.Text;
            user.password = inputPassword.Text;
            user.isAdmin = (bool)inputIsAdmin.IsChecked!;
            user.address = inputAddress.Text;
            user.questionId = ((SecurityQuestionDTO)inputQuestion.SelectedItem).questionId;
            user.securityAnswer = inputAnswer.Text;

            viewModel.AddUser(user);
            this.Close();
        }

        private void onCancelDialog(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
