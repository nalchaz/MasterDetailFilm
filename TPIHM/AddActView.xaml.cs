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
using TPIHM.ViewModels;

namespace TPIHM
{
    /// <summary>
    /// Interaction logic for AddActView.xaml
    /// </summary>
    public partial class AddActView : Window
    {
        public ActViewModel ActView;

        public AddActView()
        {
            ActView = new ActViewModel();
            DataContext = ActView;
            InitializeComponent();
        }

    }
}
