using System.IO;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 随机点名
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HeadShotPreview : WindowEx
    {
        public HeadShotPreview(ImageSource headshot)
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            Title = "头像预览";
            this.SetTaskBarIcon(Icon.FromFile(Path.Combine(Windows.ApplicationModel.Package.Current.InstalledPath, "Assets\\Icon.ico")));
            Image.Source = headshot;
        }
    }
}
