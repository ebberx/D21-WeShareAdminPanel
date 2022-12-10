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
    /// Interaction logic for TermsOfServiceDialog.xaml
    /// </summary>
    public partial class TermsOfServiceDialog : Window
    {
        TermsOfServiceDialogViewModel viewModel;

        public TermsOfServiceDialog() {
            InitializeComponent();

            viewModel = new TermsOfServiceDialogViewModel();
            this.DataContext = viewModel;

            viewModel.GetTOS();
        }

        private void OnUpdate(object sender, RoutedEventArgs e) {
            viewModel.UpdateTOS();
            this.Close();
        }

        private void OnCancel(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
