using CryptocurrencyApp.APIs;
using CryptocurrencyApp.Model;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using CryptocurrencyApp.View;
using Prism.Mvvm;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CryptocurrencyApp.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        private readonly CoinCapApiClient _coinCapApiClient = new CoinCapApiClient();
        private readonly Frame _frame;
        private ObservableCollection<CryptoData> _cryptoCurrency;
        private bool _isLoading;
        private string _buttonText = "Refresh";

        public ICommand OpenDetailsWindowCommand { get; set; }
        public ICommand RefreshDataCommand { get; set; }

        public ObservableCollection<CryptoData> CryptoCurrency
        {
            get => _cryptoCurrency;
            set => SetProperty(ref _cryptoCurrency, value);
        }

        public string ButtonText
        {
            get => _buttonText;
            set => SetProperty(ref _buttonText, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public MainWindowViewModel(Frame frame)
        {
            _frame = frame;
            RefreshDataCommand = new DelegateCommand(LoadDataAsync, () => true);
            OpenDetailsWindowCommand = new DelegateCommand<string>(OpenDetailsWindow, _ => true);

            CryptoCurrency = new ObservableCollection<CryptoData>();

            LoadDataAsync();
        }
        private void OpenDetailsWindow(string id)
        {
            var detailsViewModel = new DetailsWindowViewModel(id);
            DetailsWindow detailsWindow = new DetailsWindow(id);
            detailsWindow.Show();
        }

        private async void LoadDataAsync()
        {
            IsLoading = true;

            CryptoCurrency.Clear();

            try
            {
                var result = await _coinCapApiClient.GetCurrenciesAsync();
                CryptoCurrency.AddRange(result);

                Debug.WriteLine($"Loaded {CryptoCurrency} items.");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
