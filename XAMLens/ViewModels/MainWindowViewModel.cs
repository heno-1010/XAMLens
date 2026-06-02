using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XAMLens.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string _xamlText;

        public Control? _previewControl;
        public Control? PreviewControl
        {
            get  => _previewControl;
            set
            {
                _previewControl = value;
                OnPropertyChanged();
            }
        }

        public string XAMLText
        {
            get => _xamlText;
            set
            {
                if (_xamlText != value)
                {
                    _xamlText = value;
                    var xaml = value;
                    OnPropertyChanged();

                    try
                    {
                        if (!xaml.Contains("xmlns="))
                        {
                            var firstSpace = xaml.IndexOf(' ');
                            if(firstSpace > 0)
                            {
                                xaml = xaml.Insert(firstSpace, " xmlns=\"https://github.com/avaloniaui\"");
                            }
                        }
                        PreviewControl = AvaloniaRuntimeXamlLoader.Parse<Control>(xaml);
                    }
                    catch
                    {
                        PreviewControl = null;
                    }
                }
            }
        }
    }
}
