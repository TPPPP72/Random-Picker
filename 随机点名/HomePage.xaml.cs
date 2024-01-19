using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 随机点名
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private Userdata now;
        private readonly List<Userdata> data = DataAccess.GetData();
        private readonly Random random = new();
        private readonly Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public HomePage()
        {
            InitializeComponent();
        }
        private void Calling_Click(object sender, RoutedEventArgs e)
        {
            if (data.Count > 0)
            {
                data.Sort((x, y) => x.TIME.CompareTo(y.TIME));
                int index = 0;
                if (localSettings.Values["Engine"].ToString() == "默认")
                    index = random.Next(data.Count);
                else
                    index = PEIFunction.GetUser(data);
                now = data[index];
                if (data.Count > 1)
                {
                    while (now.NAME == Name.Text)
                    {
                        if (localSettings.Values["Engine"].ToString() == "默认")
                            index = random.Next(data.Count);
                        else
                            index = PEIFunction.GetUser(data);
                        now = data[index];
                    }
                }
                DataAccess.AddTime(now.ID);
                ++data[index].TIME;
                if (localSettings.Values["Animation"].ToString() == "true")
                {
                    FullScreenPlayer player = new(PEIFunction.QueryQuality(index, PEIFunction.GetRanges(data.Count)));
                    player.Activate();
                }
                Name.Text = now.NAME;
                Headshot.Fill = PEIFunction.GetBrush(now.HEADSHOT);
            }
            else
            {
                Name.Text = "没有成员，请添加";
            }
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var min = Math.Min(ActualHeight / 2, ActualWidth) - 50;
            Headshot.Width = Headshot.Height = min;
            if (now != null)
                Headshot.Fill = PEIFunction.GetBrush(now.HEADSHOT);
        }
    }
}
