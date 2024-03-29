﻿using WinUIEx;
using System.IO;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 随机点名
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdateLog : WindowEx
    {
        public UpdateLog()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            Title = "更新日志";
            this.SetTaskBarIcon(Icon.FromFile(Path.Combine(Windows.ApplicationModel.Package.Current.InstalledPath, "Assets\\Icon.ico")));
        }
    }
}
