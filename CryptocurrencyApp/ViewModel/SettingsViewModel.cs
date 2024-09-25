using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CryptocurrencyApp.ViewModel
{
    public class SettingsViewModel : BindableBase
    {
        private string _language = "English";

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
    }
}
