using Microsoft.UI.Xaml.Controls;
using System.Linq;
using WinUIEx;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 随机点名
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WindowEx
    {
        public MainWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            Title = Windows.ApplicationModel.Package.Current.DisplayName;
            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems.OfType<NavigationViewItem>().First();
            Window_Navigation.Navigate(typeof(HomePage));
            this.CenterOnScreen();
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                Window_Navigation.Navigate(typeof(SettingPage));
                return;
            }
            var SelectedItem = (NavigationViewItem)args.SelectedItem;
            switch (SelectedItem.Tag as string)
            {
                case "HomePage":
                    Window_Navigation.Navigate(typeof(HomePage));
                    break;

                case "PeoplePage":
                    Window_Navigation.Navigate(typeof(PeoplePage));
                    break;

                case "HelpPage":
                    Window_Navigation.Navigate(typeof(HelpPage));
                    break;
            }
        }
    }
}
