using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace CryptocurrencyApp.ViewModel
{
    public class SettingsViewModel : BindableBase
    {
        private string _language = "English";
        private bool _isDarkTheme = true;

        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                SetProperty(ref _isDarkTheme, value);
                ApplyTheme();
            }
        }
        public string Language
        {
            get => _language;
            set
            {
                ChangeUILanguage(_language, value);
                SetProperty(ref _language, value);
            }
        }

        public ObservableCollection<string> Languages { get; set; }

        public SettingsViewModel()
        {
            Languages = new ObservableCollection<string>()
            {
                "English",
                "Ukrainian"
            };
        }
        private void ChangeUILanguage(string oldLanguage, string newLanguage)
        {
            var oldDictPath = new Uri($"pack://application:,,,/CryptocurrencyApp;component/Resources/{oldLanguage}.xaml");
            var oldResDic = Application.Current.Resources.MergedDictionaries.First(_ => _.Source.OriginalString == oldDictPath.OriginalString);
            
            var newResDic = new ResourceDictionary();
            newResDic.Source = new Uri($"pack://application:,,,/CryptocurrencyApp;component/Resources/{newLanguage}.xaml");

            Application.Current.Resources.MergedDictionaries.Remove(oldResDic);
            Application.Current.Resources.MergedDictionaries.Add(newResDic);
        }
        private void ApplyTheme()
        {
            if (_isDarkTheme)
            {
                Wpf.Ui.Appearance.ApplicationThemeManager.Apply(
                    Wpf.Ui.Appearance.ApplicationTheme.Dark,
                    Wpf.Ui.Controls.WindowBackdropType.Mica,
                    true
                );
                Application.Current.Resources["PrimaryTextColor"] = new SolidColorBrush(Colors.White);
                Application.Current.Resources["BackgroundColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#292929"));
            }
            else
            {
                Wpf.Ui.Appearance.ApplicationThemeManager.Apply(
                    Wpf.Ui.Appearance.ApplicationTheme.Light,
                    Wpf.Ui.Controls.WindowBackdropType.Mica,
                    true
                );
                Application.Current.Resources["PrimaryTextColor"] = new SolidColorBrush(Colors.Black);
                Application.Current.Resources["BackgroundColor"] = new SolidColorBrush(Colors.White);
            }
        }
    }
}
