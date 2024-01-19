using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 随机点名
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HelpPage : Page
    {
        public HelpPage()
        {
            InitializeComponent();
            Code.Text = """
                int Maxtime = datas.Last().TIME;
                List<Tuple<int, int>> pranges = [];
                int start = 1;
                foreach (var i in datas)
                {
                    int step = (Maxtime - i.TIME + 1) * (Maxtime - i.TIME + 1);
                    pranges.Add(new Tuple<int, int>(start, start + step));
                    start += step + 1;
                }
                Random rand = new();
                int index = rand.Next(1, start);
                for (int i = 0; i < pranges.Count; i++)
                    if (index >= pranges[i].Item1 && index <= pranges[i].Item2)
                        return i;
                return 0;
                """;
            Version.Text = $"当前版本:{Info.CurrentVersion}";
        }

        private void LOG_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            UpdateLog updateLog = new();
            updateLog.Activate();
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string material = localSettings.Values["Material"].ToString();
            if (material == "云母")
                updateLog.SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.BaseAlt };
            else
                updateLog.SystemBackdrop = new DesktopAcrylicBackdrop();
        }
    }
}
