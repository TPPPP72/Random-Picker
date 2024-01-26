using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Storage;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 随机点名
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PeoplePage : Page
    {
        public PeoplePage()
        {
            InitializeComponent();
            Flash_Table();
        }

        private class ListData(int id, string name, ImageBrush headshot, int time)
        {
            private int _id = id;
            private string _name = name;
            private ImageBrush _headshot = headshot;
            private int _time = time;
            public int ID { get { return _id; } set { _id = value; } }
            public string NAME { get { return _name; } set { _name = value; } }
            public ImageBrush HEADSHOT { get { return _headshot; } set { _headshot = value; } }
            public int TIME { get { return _time; } set { _time = value; } }
        }

        private int GetCurrentID()
        {
            return (Memberlist.SelectedItem as ListData).ID;
        }

        private void Flash_Table()
        {
            List<ListData> datas = [];
            foreach (var item in DataAccess.GetData())
                datas.Add(new ListData(item.ID, item.NAME, PEIFunction.GetBrush(item.HEADSHOT), item.TIME));
            Memberlist.ItemsSource = datas.OrderBy(x => x.TIME);
        }

        private async void Addmember_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = new()
            {
                Width = 300,
                HorizontalAlignment = HorizontalAlignment.Left,
                PlaceholderText = "输入添加成员的姓名"
            };
            ContentDialog AddmemberDialog = new()
            {
                XamlRoot = XamlRoot,
                Title = "添加成员",
                Content = textBox,
                PrimaryButtonText = "确定",
                CloseButtonText = "关闭",
                DefaultButton = ContentDialogButton.Primary
            };
            ContentDialogResult result = await AddmemberDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (textBox.Text.Length > 0)
                {
                    DataAccess.AddData(textBox.Text);
                    Flash_Table();
                }
                else
                {
                    ContentDialog Edialog = new()
                    {
                        XamlRoot = XamlRoot,
                        Title = "错误",
                        Content = "未输入内容",
                        CloseButtonText = "关闭"
                    };
                    await Edialog.ShowAsync();
                }
            }
        }

        private async void Importmember_Click(object sender, RoutedEventArgs e)
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle((Application.Current as App).m_window);
            FileOpenPicker openPicker = new()
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hwnd);
            openPicker.FileTypeFilter.Add(".txt");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                StreamReader rd = File.OpenText(file.Path);
                DataAccess.ClearTable();
                while (!rd.EndOfStream)
                    DataAccess.AddData(rd.ReadLine());
                Flash_Table();
            }
        }

        private void Memberlist_RightTapped(object sender, Microsoft.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            var thi = ((FrameworkElement)e.OriginalSource).DataContext;
            Memberlist.SelectedItem = thi;
            if (thi != null)
                menuFlyout.ShowAt(Memberlist, e.GetPosition(Memberlist));
        }

        private async void EditHeadshot_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = new()
            {
                Width = 300,
                HorizontalAlignment = HorizontalAlignment.Left,
                PlaceholderText = "输入绑定的QQ号或者网址"
            };
            ContentDialog EditHeadshotDialog = new()
            {
                XamlRoot = XamlRoot,
                Title = "编辑头像",
                Content = textBox,
                PrimaryButtonText = "确定",
                SecondaryButtonText = "恢复默认",
                CloseButtonText = "关闭",
                DefaultButton = ContentDialogButton.Primary
            };
            ContentDialogResult result = await EditHeadshotDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (textBox.Text.Length > 0)
                {
                    string url;
                    if (PEIFunction.Isnumber(textBox.Text) == true)
                        url = $"http://q.qlogo.cn/headimg_dl?dst_uin={textBox.Text}&spec=640&img_type=jpg";
                    else
                        url = textBox.Text;
                    PEIFunction.GetBrush(url);
                    DataAccess.UpdateHeadshot(GetCurrentID(), url);
                    Flash_Table();
                }
                else
                {
                    ContentDialog Edialog = new()
                    {
                        XamlRoot = XamlRoot,
                        Title = "错误",
                        Content = "未输入内容",
                        CloseButtonText = "关闭"
                    };
                    await Edialog.ShowAsync();
                }
            }
            else if (result == ContentDialogResult.Secondary)
            {
                DataAccess.UpdateHeadshot(GetCurrentID(), Info.DefaultHeadshot);
                Flash_Table();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Memberlist.SelectedIndex != -1)
            {
                DataAccess.DeleteUser(GetCurrentID());
                Flash_Table();
            }
        }

        private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Memberlist.Height = ActualHeight - 75;
        }

        private async void Reset_Click(object sender, RoutedEventArgs e)
        {
            string password = $"{DateTime.Now.Hour + 1}{DateTime.Now.Minute + 1}";
            TextBox textBox = new()
            {
                Width = 300,
                HorizontalAlignment = HorizontalAlignment.Left,
                PlaceholderText = "输入密码"
            };
            ContentDialog EditHeadshotDialog = new()
            {
                XamlRoot = XamlRoot,
                Title = "重置次数",
                Content = textBox,
                PrimaryButtonText = "确定",
                CloseButtonText = "关闭",
                DefaultButton = ContentDialogButton.Primary
            };
            ContentDialogResult result = await EditHeadshotDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (textBox.Text == password)
                {
                    foreach (var item in DataAccess.GetData())
                        DataAccess.ResetTime(item.ID);
                    Flash_Table();
                }
                else
                {
                    ContentDialog Edialog = new()
                    {
                        XamlRoot = XamlRoot,
                        Title = "错误",
                        Content = "密码错误",
                        CloseButtonText = "关闭"
                    };
                    await Edialog.ShowAsync();
                }
            }
        }

        private void Ellipse_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Ellipse ellipse = sender as Ellipse;
            ImageBrush brush = ellipse.Fill as ImageBrush;
            HeadShotPreview headShotPreview = new(brush.ImageSource);
            headShotPreview.Activate();
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string material = localSettings.Values["Material"].ToString();
            if (material == "云母")
                headShotPreview.SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.BaseAlt };
            else
                headShotPreview.SystemBackdrop = new DesktopAcrylicBackdrop();
        }
    }
}
