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
using TPIHM.ViewModels;

namespace TPIHM
{
    /// <summary>
    /// Interaction logic for Validation.xaml
    /// </summary>
    public partial class Validation : Window
    {
        public ValidationViewModel ValidView;
        public Validation(string text)
        {
            ValidView = new ValidationViewModel(text);
            DataContext = ValidView;
            InitializeComponent();
        }
    }
}
