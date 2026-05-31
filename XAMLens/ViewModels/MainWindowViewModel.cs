using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XAMLens.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string _xamlText;

        public string XAMLText
        {
            get => _xamlText;
            set
            {
                if (_xamlText != value)
                {
                    _xamlText = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
