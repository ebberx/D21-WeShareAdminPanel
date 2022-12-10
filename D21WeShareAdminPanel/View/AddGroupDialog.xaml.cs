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
    /// Interaction logic for AddGroupDialog.xaml
    /// </summary>
    public partial class AddGroupDialog : Window
    {
        AddGroupDialogViewModel viewModel;
        public AddGroupDialog() {
            InitializeComponent();

            viewModel = new AddGroupDialogViewModel();
            this.DataContext = viewModel;
        }

        private void OnCreate(object sender, RoutedEventArgs e) {
            NewGroupDTO group = new NewGroupDTO();

            group.name = inputName.Text;
            group.description = inputDescription.Text;
            group.userID = int.Parse(inputOwnerID.Text);
            group.isPublic = (bool)inputIsPublic.IsChecked!;

            viewModel.AddGroup(group);

            this.Close();
        }

        private void onCancel(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
