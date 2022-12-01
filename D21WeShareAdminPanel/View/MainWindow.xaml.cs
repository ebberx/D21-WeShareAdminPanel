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
using System.Windows.Navigation;
using System.Windows.Shapes;
using D21WeShareAdminPanel.ViewModel;
using D21WeShareAdminPanel.Model.DTO;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace D21WeShareAdminPanel.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel;
        string sessionToken;

        public MainWindow(String _sessionToken) {
            InitializeComponent();

            viewModel = new MainWindowViewModel(_sessionToken);
            this.DataContext = viewModel;
            
            // Save session token for later use
            this.sessionToken = _sessionToken;
        }

        public void DisplayGroup(GroupDTO group) {
            // Display group info
            groupID.Text = group.groupId.ToString();
            groupName.Text = group.name;
            groupDescription.Text = group.description;
            groupIsPublic.IsChecked = group.isPublic;
            groupHasConcluded.IsChecked = group.hasConcluded;

            // Query users in group
            viewModel.GetGroupDetailsByID(group.groupId);

        }

        public void DisplayUser(UserDTO user) {
            // Display group info
            userUserID.Text = user.userId.ToString();
            userUserName.Text = user.userName;
            userFirstName.Text = user.firstName;
            userLastName.Text = user.lastName;
            userPhoneNumber.Text = user.phoneNumber;
            userEmail.Text = user.email;
            userIsAdminCheck.IsChecked = user.isAdmin;

            // Query groups that user is a part of
            //viewModel.GetGroupDetailsByID(user.userID);
        }

        private async void onSearchGroupName(object sender, RoutedEventArgs e) {

            List<GroupDTO> groups = await viewModel.GetGroupsByName(searchGroupNameBox.Text);

            if (groups == null)
                return;

            foreach (GroupDTO group in groups) {
                // Stack panel container
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Vertical;

                sp.Children.Add(new Separator() { Height = 1 });
                TextBlock title = new TextBlock() { Text = "-= Group Name =- " };
                title.TextAlignment = TextAlignment.Center;
                sp.Children.Add(title);
                sp.Children.Add(new TextBlock() { Text = group.name });
                sp.Children.Add(new TextBlock() { Text = group.description });
                sp.Children.Add(new Separator() { Height = 1 });

                // Button stackpanel
                StackPanel buttonSP = new StackPanel();
                buttonSP.Orientation = Orientation.Horizontal;
                buttonSP.HorizontalAlignment = HorizontalAlignment.Center;

                // Open button
                Button openBut = new Button() { Content = "Open" };
                openBut.Width = 40;
                openBut.Margin = new Thickness(10, 0, 10, 0);
                openBut.Click += (o, e) => {
                    // Change to group tab
                    tabControl.SelectedIndex = 1;
                    DisplayGroup(group);
                };
                buttonSP.Children.Add(openBut);

                // Delete button
                Button deleteBut = new Button() { Content = "X" };
                deleteBut.Width = 20;
                deleteBut.Margin = new Thickness(10, 0, 10, 0);
                deleteBut.Click += (o, e) => {
                    // delete sp
                    resultView.Children.Remove(sp);
                };
                buttonSP.Children.Add(deleteBut);

                sp.Children.Add(buttonSP);
                sp.Children.Add(new Separator() { Height = 1 });
                resultView.Children.Add(sp);
            }
        }

        private async void onSearchGroupID(object sender, RoutedEventArgs e) {
            GroupDTO groupInfo = await viewModel.GetGroupByID(Int32.Parse(searchGroupIDBox.Text));

            if (groupInfo == null)
                return;

            // Stack panel container
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;

            sp.Children.Add(new Separator() { Height = 1 });
            TextBlock title = new TextBlock() { Text = "-= Group ID =- " };
            title.TextAlignment = TextAlignment.Center;
            sp.Children.Add(title);
            sp.Children.Add(new TextBlock() { Text = groupInfo.name });
            sp.Children.Add(new TextBlock() { Text = groupInfo.description });
            sp.Children.Add(new Separator() { Height = 1 });

            // Button stackpanel
            StackPanel buttonSP = new StackPanel();
            buttonSP.Orientation = Orientation.Horizontal;
            buttonSP.HorizontalAlignment = HorizontalAlignment.Center;

            // Open button
            Button openBut = new Button() { Content = "Open" };
            openBut.Width = 40;
            openBut.Margin = new Thickness(10, 0, 10, 0);
            openBut.Click += (o, e) => {
                // Change to group tab
                tabControl.SelectedIndex = 1;
                DisplayGroup(groupInfo);
            };
            buttonSP.Children.Add(openBut);

            // Delete button
            Button deleteBut = new Button() { Content = "X" };
            deleteBut.Width = 20;
            deleteBut.Margin = new Thickness(10, 0, 10, 0);
            deleteBut.Click += (o, e) => {
                // delete sp
                resultView.Children.Remove(sp);
            };
            buttonSP.Children.Add(deleteBut);
            
            sp.Children.Add(buttonSP);
            sp.Children.Add(new Separator() { Height = 1 });
            resultView.Children.Add(sp);
        }

        private void onGroupUpdate(object sender, RoutedEventArgs e) {
            MessageBox.Show("Group updated");
        }

        private void onGroupAdd(object sender, RoutedEventArgs e) {
            MessageBox.Show("Group added");
        }

        private void onGroupDelete(object sender, RoutedEventArgs e) {
            MessageBox.Show("Group deleted");
        }

        private async void onSearchUserName(object sender, RoutedEventArgs e) {
            List<UserDTO> users = await viewModel.GetUsersByName(searchUserNameBox.Text);

            if (users == null)
                return;

            foreach (UserDTO user in users) {
                // Stack panel container
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Vertical;

                sp.Children.Add(new Separator() { Height = 1 });
                TextBlock title = new TextBlock() { Text = "-= User Name =- " };
                title.TextAlignment = TextAlignment.Center;
                sp.Children.Add(title);
                sp.Children.Add(new TextBlock() { Text = user.userName });
                sp.Children.Add(new TextBlock() { Text = user.firstName + " " + user.lastName });
                sp.Children.Add(new Separator() { Height = 1 });

                // Button stackpanel
                StackPanel buttonSP = new StackPanel();
                buttonSP.Orientation = Orientation.Horizontal;
                buttonSP.HorizontalAlignment = HorizontalAlignment.Center;

                // Open button
                Button openBut = new Button() { Content = "Open" };
                openBut.Width = 40;
                openBut.Margin = new Thickness(10, 0, 10, 0);
                openBut.Click += (o, e) => {
                    // Change to group tab
                    tabControl.SelectedIndex = 2;
                    DisplayUser(user);
                };
                buttonSP.Children.Add(openBut);

                // Delete button
                Button deleteBut = new Button() { Content = "X" };
                deleteBut.Width = 20;
                deleteBut.Margin = new Thickness(10, 0, 10, 0);
                deleteBut.Click += (o, e) => {
                    // delete sp
                    resultView.Children.Remove(sp);
                };
                buttonSP.Children.Add(deleteBut);

                sp.Children.Add(buttonSP);
                sp.Children.Add(new Separator() { Height = 1 });
                resultView.Children.Add(sp);
            }
        }

        private async void onSearchUserID(object sender, RoutedEventArgs e) {
            UserDTO user = await viewModel.GetUsersByID(searchUserIDBox.Text);

            if (user == null)
                return;

            // Stack panel container
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;

            sp.Children.Add(new Separator() { Height = 1 });
            TextBlock title = new TextBlock() { Text = "-= User Name =- " };
            title.TextAlignment = TextAlignment.Center;
            sp.Children.Add(title);
            sp.Children.Add(new TextBlock() { Text = user.userName });
            sp.Children.Add(new TextBlock() { Text = user.firstName + " " + user.lastName });
            sp.Children.Add(new Separator() { Height = 1 });

            // Button stackpanel
            StackPanel buttonSP = new StackPanel();
            buttonSP.Orientation = Orientation.Horizontal;
            buttonSP.HorizontalAlignment = HorizontalAlignment.Center;

            // Open button
            Button openBut = new Button() { Content = "Open" };
            openBut.Width = 40;
            openBut.Margin = new Thickness(10, 0, 10, 0);
            openBut.Click += (o, e) => {
                // Change to group tab
                tabControl.SelectedIndex = 2;
                DisplayUser(user);
            };
            buttonSP.Children.Add(openBut);

            // Delete button
            Button deleteBut = new Button() { Content = "X" };
            deleteBut.Width = 20;
            deleteBut.Margin = new Thickness(10, 0, 10, 0);
            deleteBut.Click += (o, e) => {
                // delete sp
                resultView.Children.Remove(sp);
            };
            buttonSP.Children.Add(deleteBut);

            sp.Children.Add(buttonSP);
            sp.Children.Add(new Separator() { Height = 1 });
            resultView.Children.Add(sp);
        }

        private void onSearchTransactionName(object sender, RoutedEventArgs e) {

        }

        private void onSearchTransactionID(object sender, RoutedEventArgs e) {

        }

        private void onLogOut(object sender, RoutedEventArgs e) {
            // Show login dialog
            LoginDialog loginDialog = new LoginDialog();
            loginDialog.Show();

            // Close this window 
            this.Close();
        }

        private void onUpdateTOS(object sender, RoutedEventArgs e) {

        }
    }
}
