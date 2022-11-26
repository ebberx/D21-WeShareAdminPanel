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
            groupName.Text = group.name;
            groupDescription.Text = group.description;
            groupIsPublic.Text = group.isPublic.ToString();
            groupHasConcluded.Text = group.hasConcluded.ToString();

            // Query group details and display group details
            viewModel.GetGroupDetailsByID(group.groupId);
            
        }

        private void onSearchGroupName(object sender, RoutedEventArgs e) {

            // Stack panel container
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;

            // Add seperator inbetween results, but not for the first result
            if (resultView.Children.Count != 0)
                sp.Children.Add(new Separator() { Height = 10 });
            sp.Children.Add(new TextBlock() { Text = "This is a search result object" });

            // Delete button
            Button deleteBut = new Button() { Content = "X" };
            deleteBut.Click += (o,e) => {
                // delete sp
                resultView.Children.Remove(sp);
            };
            sp.Children.Add(deleteBut);
            resultView.Children.Add(sp);

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
    }
}
