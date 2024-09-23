using CryptocurrencyApp.APIs;
using CryptocurrencyApp.Model;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using CryptocurrencyApp.View;
using Prism.Mvvm;
using Prism.Commands;
using System.Collections.ObjectModel;

namespace CryptocurrencyApp.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        private readonly CoinCapApiClient _coinCapApiClient = new CoinCapApiClient();
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

        public MainWindowViewModel()
        {
            RefreshDataCommand = new DelegateCommand(LoadDataAsync, () => true);
            OpenDetailsWindowCommand = new DelegateCommand<string>(OpenDetailsWindow, _ => true);

            CryptoCurrency = new ObservableCollection<CryptoData>();

            LoadDataAsync();
        }
        private void OpenDetailsWindow(string id)
        {
            Debug.WriteLine("OpenDetailsWindow command executed");
            DetailsWindow detailsWindow = new DetailsWindow();
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
