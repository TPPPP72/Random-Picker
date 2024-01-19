using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 随机点名
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        private readonly Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public SettingPage()
        {
            InitializeComponent();
            MaterialComboBox.SelectedItem = localSettings.Values["Material"];
            EngineComboBox.SelectedItem = localSettings.Values["Engine"];
            if (localSettings.Values["Animation"].ToString() == "true")
                AnimationSwitch.IsOn = true;
        }

        private void MaterialComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            localSettings.Values["Material"] = MaterialComboBox.SelectedItem;
            MainWindow appwindow = (MainWindow)(Application.Current as App).m_window;
            string material = localSettings.Values["Material"].ToString();
            if (material == "云母")
                appwindow.SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.BaseAlt };
            else
                appwindow.SystemBackdrop = new DesktopAcrylicBackdrop();
        }

        private void AnimationSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (AnimationSwitch.IsOn)
                localSettings.Values["Animation"] = "true";
            else
                localSettings.Values["Animation"] = "false";
        }

        private void EngineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            localSettings.Values["Engine"] = EngineComboBox.SelectedItem;
        }
    }
}
