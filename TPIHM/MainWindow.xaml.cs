using System;
using System.Text.RegularExpressions;
using System.Windows;
using TPIHM.Events;
using TPIHM.ViewModels;

namespace TPIHM
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListFilmViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ListFilmViewModel();
            DataContext = _viewModel;
        }

    }
}

