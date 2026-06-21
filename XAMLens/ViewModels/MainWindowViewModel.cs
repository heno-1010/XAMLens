using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace XAMLens.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private string _xamlText =
@"<Window xmlns=""https://github.com/avaloniaui"">

</Window>";

        private Control? _previewControl;
        private string? _errorMessage;

        public Control? PreviewControl
        {
            get => _previewControl;
            set
            {
                _previewControl = value;
                OnPropertyChanged();
            }
        }

        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
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
                        // プレビュー用にWindowをUserControlに変換
                        var xaml = ConvertWindowToUserControl(value);
                        // XAMLの名前空間を補完
                        xaml = NormalizeXaml(xaml);
                        // バインディングを検索
                        FindBindings(xaml);

                        PreviewControl = AvaloniaRuntimeXamlLoader.Parse<Control>(xaml);
                        ErrorMessage = null;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                        PreviewControl = null;
                        ErrorMessage = ex.Message;
                    }
                }
            }
        }
        // Windowをプレビュー用のUserControlに変換
        private string ConvertWindowToUserControl(string xaml)
        {
            if (string.IsNullOrWhiteSpace(xaml))
                return xaml;

            var root = XElement.Parse(xaml);

            if (root.Name.LocalName != "Window")
                return xaml;

            var newRoot = new XElement(root.Name.Namespace + "UserControl", root.Attributes(), root.Nodes());
            return newRoot.ToString();
        }
        // XAMLの名前空間を補完
        private string NormalizeXaml(string xaml)
        {
            if (string.IsNullOrWhiteSpace(xaml))
                return xaml;

            var root = XElement.Parse(xaml);

            if (!string.IsNullOrEmpty(root.Name.NamespaceName))
                return xaml;

            var newRoot = new XElement(
                XNamespace.Get("https://github.com/avaloniaui") + root.Name.LocalName,
                root.Attributes(),
                root.Nodes()
                );

            return newRoot.ToString();

        }

        private void FindBindings(string xaml)
        {
            var root = XElement.Parse(xaml);
            FindBindings(root);
        }

        private void FindBindings(XElement element)
        {
            foreach (var attribute in element.Attributes()) // XAMLの属性をチェック
            {
                if (attribute.Value.StartsWith("{Binding"))
                {
                    System.Diagnostics.Debug.WriteLine(attribute.Value); // Bindingの内容を出力
                }
            }
            foreach (var child in element.Elements())
            {
                FindBindings(child);
            }
        }
    }
}
