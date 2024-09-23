using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CryptocurrencyApp.Navigation
{
    class NavigationService
    {
        private readonly Frame _frame;

        public NavigationService(Frame frame)
        {
            _frame = frame;
        }

        public void NavigateTo<TView>() where TView : Page, new()
        {
            _frame.Navigate(typeof(TView));
        }

    }
}
