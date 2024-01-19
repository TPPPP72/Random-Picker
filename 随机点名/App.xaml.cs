using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 随机点名
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            DataAccess.InitializeDatabase();
            Settings.InitSetting();
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (string.Compare(localSettings.Values["Update"].ToString(), Info.CurrentVersion) == -1)
            {
                localSettings.Values["Update"] = Info.CurrentVersion;
            }
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string material = localSettings.Values["Material"].ToString();
            if (material == "云母")
                m_window.SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.BaseAlt };
            else
                m_window.SystemBackdrop = new DesktopAcrylicBackdrop();
        }

        public Window m_window;
    }
}
