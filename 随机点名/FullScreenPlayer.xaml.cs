using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Media.Core;
using Windows.Media.Playback;
using WinUIEx;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 随机点名
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FullScreenPlayer : Window
    {
        public readonly AppWindow appWindow;
        public MediaPlayerElement player;
        public FullScreenPlayer(int x)
        {
            InitializeComponent();
            WindowManager manager = WindowManager.Get(this);
            appWindow = manager.AppWindow;
            appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            player = new()
            {
                Source = MediaSource.CreateFromUri(new($"ms-appx:///Assets/{5 - x}star-single.mp4")),
                AreTransportControlsEnabled = false,
                AutoPlay = false
            };
            player.MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            player.MediaPlayer.Play();
            grid.Children.Add(player);
        }

        private void MediaPlayer_MediaEnded(MediaPlayer sender, object args)
        {
            DispatcherQueue.TryEnqueue(Close);
        }
    }
}
