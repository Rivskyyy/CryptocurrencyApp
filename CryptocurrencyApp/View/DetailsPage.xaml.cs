using CryptocurrencyApp.ViewModel;
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
using Wpf.Ui.Controls;

namespace CryptocurrencyApp.View
{
 
    public partial class DetailsPage : Page
    {
        public DetailsPage(string id)
        {
            InitializeComponent();
            DetailsWindowViewModel detailsWindowViewModel = new DetailsWindowViewModel(id);
            DataContext = detailsWindowViewModel;
        }
    }
}
