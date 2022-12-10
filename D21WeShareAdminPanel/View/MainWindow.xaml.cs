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
using D21WeShareAdminPanel.Model;

namespace D21WeShareAdminPanel.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel;
        string sessionToken;
        bool didLogout = false;


        public MainWindow(String _sessionToken) {
            InitializeComponent();

            viewModel = new MainWindowViewModel(_sessionToken);
            this.DataContext = viewModel;

            // Update UI delegates
            viewModel.DisplayGroup += DisplayGroup;
            viewModel.DisplayUser += DisplayUser;

            // Run code on window close
            this.Closed += (o, e) => { if (!didLogout) { viewModel.Logout(); didLogout = true; } };

            // Save session token for later use
            this.sessionToken = _sessionToken;
        }

        public void DisplayGroup() {

            if (viewModel.groupInTab == null)
                return;

            // Display group info
            groupID.Text = viewModel.groupInTab.groupId.ToString();
            groupName.Text = viewModel.groupInTab.name;
            groupDescription.Text = viewModel.groupInTab.description;
            groupIsPublic.IsChecked = viewModel.groupInTab.isPublic;
            groupHasConcluded.IsChecked = viewModel.groupInTab.hasConcluded;

            // Query users in group
            viewModel.GetGroupDetailsByID(viewModel.groupInTab.groupId);

            // Display expenses in group
            viewModel.GetExpensesForGroup(viewModel.groupInTab.groupId);
            // Display In Payments in group
            viewModel.GetInPaymentsForGroup(viewModel.groupInTab.groupId);
        }

        public void DisplayUser() {

            if (viewModel.userInTab == null)
                return;

            // Display group info
            userUserID.Text = viewModel.userInTab.userId.ToString();
            userUserName.Text = viewModel.userInTab.userName;
            userFirstName.Text = viewModel.userInTab.firstName;
            userLastName.Text = viewModel.userInTab.lastName;
            userPhoneNumber.Text = viewModel.userInTab.phoneNumber;
            userEmail.Text = viewModel.userInTab.email;
            userIsAdminCheck.IsChecked = viewModel.userInTab.isAdmin;
            userQuestionBox.Text = viewModel.userInTab.question;
            userSecurityAnswerBox.Text = viewModel.userInTab.securityAnswer;
            userIsDisabledCheck.IsChecked = viewModel.userInTab.isDisabled;
            userIsBlacklistedCheck.IsChecked = viewModel.userInTab.isBlacklisted;

            // Query groups that user is a part of
            viewModel.GetAllGroupsForUser(viewModel.userInTab.userId);

            // Display expenses for user
            viewModel.GetInPaymentsForUser(viewModel.userInTab.userId);
            // Display in payments for user
            viewModel.GetExpensesForUser(viewModel.userInTab.userId);
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
                TextBlock title = new TextBlock() { Text = "-= Group Name =-" };
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
                    SwitchToGroupInfo(group);
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
                SwitchToGroupInfo(groupInfo);
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
                    SwitchToUserInfo(user);
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
                SwitchToUserInfo(user);
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
            MessageBox.Show("Unimplemented");
        }

        private void onSearchTransactionID(object sender, RoutedEventArgs e) {
            MessageBox.Show("Unimplemented");
        }

        private void onLogOut(object sender, RoutedEventArgs e) {

            // Logout from backend
            if (!didLogout) {
                viewModel.Logout();
                didLogout = true;
            }

            // Show login dialog
            LoginDialog loginDialog = new LoginDialog();
            loginDialog.Show();

            // Close this window 
            this.Close();
        }

        private void onUpdateTOS(object sender, RoutedEventArgs e) {
            TermsOfServiceDialog view = new TermsOfServiceDialog();
            view.Show();
        }

        private void onGroupUpdate(object sender, RoutedEventArgs e) {

            if (viewModel.groupInTab == null) {
                MessageBox.Show("No group selected");
                return;
            }

            GroupDTO updatedGroup = new GroupDTO();

            // Fill out group with form data
            updatedGroup.groupId = int.Parse(groupID.Text);
            updatedGroup.name = groupName.Text;
            updatedGroup.description = groupDescription.Text;
            updatedGroup.isPublic = groupIsPublic.IsChecked!.Value;
            updatedGroup.hasConcluded = groupHasConcluded.IsChecked!.Value;

            updatedGroup.creationDate = viewModel.groupInTab.creationDate;
            updatedGroup.conclusionDate = viewModel.groupInTab.conclusionDate;
            updatedGroup.lastActiveDate = viewModel.groupInTab.lastActiveDate;

            viewModel.UpdateGroup(updatedGroup);

            MessageBox.Show("Group updated");
        }

        private void onGroupAdd(object sender, RoutedEventArgs e) {
            AddGroupDialog view = new AddGroupDialog();
            view.Show();
        }

        private void onGroupDelete(object sender, RoutedEventArgs e) {
            if (viewModel.groupInTab == null) {
                MessageBox.Show("No group selected");
                return;
            }

            viewModel.DeleteGroup();

            // Reset form data
            groupID.Text = "";
            groupName.Text = "";
            groupDescription.Text = "";
            groupIsPublic.IsChecked = false;
            groupHasConcluded.IsChecked = false;

            MessageBox.Show("Group deleted");
        }

        private void onUserPassReset(object sender, RoutedEventArgs e) {
            viewModel.ResetPasswordOfUser();
            MessageBox.Show("Password reset sent");
        }

        private void onUserUpdate(object sender, RoutedEventArgs e) {

            if (viewModel.userInTab == null) {
                MessageBox.Show("No user selected");
                return;
            }

            UserDTO updatedUser = new UserDTO();

            // Fill updatedUser with form data
            updatedUser.userId = Int32.Parse(userUserID.Text);
            updatedUser.userName = userUserName.Text;
            updatedUser.firstName = userFirstName.Text;
            updatedUser.lastName = userLastName.Text;
            updatedUser.phoneNumber = userPhoneNumber.Text;
            updatedUser.email = userEmail.Text;
            updatedUser.isAdmin = (bool)userIsAdminCheck.IsChecked!;
            updatedUser.questionId = viewModel.userInTab.questionId;
            updatedUser.securityAnswer = userSecurityAnswerBox.Text;
            updatedUser.isDisabled = (bool)userIsDisabledCheck.IsChecked!;
            updatedUser.isBlacklisted = (bool)userIsBlacklistedCheck.IsChecked!;
            updatedUser.question = userQuestionBox.Text;

            updatedUser.password = viewModel.userInTab.password;
            updatedUser.address = viewModel.userInTab.address;
            updatedUser.question = viewModel.userInTab.question;

            viewModel.UpdateUser(updatedUser);

            MessageBox.Show("User updated");
        }

        private void onUserAdd(object sender, RoutedEventArgs e) {
            AddUserDialog view = new AddUserDialog();
            view.Show();
        }

        private void onUserDelete(object sender, RoutedEventArgs e) {
            if (viewModel.userInTab == null) {
                MessageBox.Show("No user selected");
                return;
            }

            viewModel.DeleteUser();

            // Reset form data
            userUserID.Text = "";
            userUserName.Text = "";
            userFirstName.Text = "";
            userLastName.Text = "";
            userPhoneNumber.Text = "";
            userEmail.Text = "";
            userIsAdminCheck.IsChecked = false;
            userSecurityAnswerBox.Text = "";
            userIsDisabledCheck.IsChecked = false;
            userIsBlacklistedCheck.IsChecked = false;
            userQuestionBox.Text = "";

            MessageBox.Show("Deleted user");
        }

        private void onGroupOfUserMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount >= 2) {
                TextBlock tb = (TextBlock)((Grid)sender).Children[0];
                GroupDTO group = (GroupDTO)BindingOperations.GetBindingExpression(tb, TextBlock.TextProperty).DataItem;

                SwitchToGroupInfo(group);
            }
        }

        private void onUsersInGroupMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount >= 2) {
                TextBlock tb = (TextBlock)((Grid)sender).Children[0];
                UserDTO user = (UserDTO)BindingOperations.GetBindingExpression(tb, TextBlock.TextProperty).DataItem;

                SwitchToUserInfo(user);
            }
        }

        public void SwitchToUserInfo(UserDTO user) {
            tabControl.SelectedIndex = 2;
            viewModel.userInTab = user;
            DisplayUser();
        }

        public void SwitchToGroupInfo(GroupDTO group) {
            tabControl.SelectedIndex = 1;
            viewModel.groupInTab = group;
            DisplayGroup();
        }

        private void onExpenseDelete(object sender, RoutedEventArgs e) {
            MessageBox.Show("Delete :-)");
        }

        private void onExpensesOfGroupMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                Debug.WriteLine("dobule click");
            }
        }

        private void onInPaymentsOfGroupMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                Debug.WriteLine("dobule click");
            }
        }

        private void onExpensesOfUserMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                Debug.WriteLine("dobule click");
            }
        }

        private void onInPaymentsOfUserMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                Debug.WriteLine("dobule click");
            }
        }
        private async void onGroupExpenseDelete(object sender, RoutedEventArgs e) {
            TextBlock tb = (TextBlock)((Grid)((Button)sender).Parent).Children[0];
            ExpenseDTO expense = (ExpenseDTO)BindingOperations.GetBindingExpression(tb, TextBlock.TextProperty).DataItem;

            await APIRequester.Delete("https://api-wan-kenobi.ovh/api/Expense/DeleteExpenditure/" + expense.expenseId);

            viewModel.ocExpensesOfGroup.Remove(expense);
        }

        private async void onGroupInPaymentDelete(object sender, RoutedEventArgs e) {

            TextBlock tb = (TextBlock)((Grid)((Button)sender).Parent).Children[0];
            InPaymentDTO inpayment = (InPaymentDTO)BindingOperations.GetBindingExpression(tb, TextBlock.TextProperty).DataItem;

            await APIRequester.Delete("https://api-wan-kenobi.ovh/api/InPayment/DeleteInPayment/" + inpayment.transactionID);

            viewModel.ocInPaymentsOfGroup.Remove(inpayment);
        }

        private async void onUserExpenseDelete(object sender, RoutedEventArgs e) {
            TextBlock tb = (TextBlock)((Grid)((Button)sender).Parent).Children[0];
            ExpenseDTO expense = (ExpenseDTO)BindingOperations.GetBindingExpression(tb, TextBlock.TextProperty).DataItem;

            await APIRequester.Delete("https://api-wan-kenobi.ovh/api/Expense/DeleteExpenditure/" + expense.expenseId);

            viewModel.ocExpensesOfUser.Remove(expense);
        }

        private async void onUserInPaymentDelete(object sender, RoutedEventArgs e) {
            TextBlock tb = (TextBlock)((Grid)((Button)sender).Parent).Children[0];
            InPaymentDTO inpayment = (InPaymentDTO)BindingOperations.GetBindingExpression(tb, TextBlock.TextProperty).DataItem;

            await APIRequester.Delete("https://api-wan-kenobi.ovh/api/InPayment/DeleteInPayment/" + inpayment.transactionID);

            viewModel.ocInPaymentsOfUser.Remove(inpayment);
        }
    }
}
