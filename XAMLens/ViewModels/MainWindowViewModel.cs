using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

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
                    OnPropertyChanged();

                    try
                    {
                        var xaml = NormalizeXaml(value);
                        PreviewControl = AvaloniaRuntimeXamlLoader.Parse<Control>(xaml);
                    }
                    catch
                    {
                        PreviewControl = null;
                    }
                }
            }
        }
        private string NormalizeXaml(string xaml)
        {
            if(string.IsNullOrWhiteSpace(xaml))
                return xaml;

            var root = XElement.Parse(xaml);

            if (!string.IsNullOrEmpty(root.Name.NamespaceName))
                return xaml;

            var newRoot = new XElement(XNamespace.Get("https://github.com/avaloniaui") + root.Name.LocalName, root.Attributes(),root.Nodes());
            return newRoot.ToString();

        }
    }
}
