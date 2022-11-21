﻿using System;
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

            viewModel = new MainWindowViewModel();
            
            // Save session token for later use
            this.sessionToken = _sessionToken;
        }
    }
}
