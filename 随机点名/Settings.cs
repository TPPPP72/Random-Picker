namespace 随机点名
{
    public static class Settings
    {
        public static void InitSetting()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (!localSettings.Values.ContainsKey("Material"))
                localSettings.Values["Material"] = "亚克力";
            if (!localSettings.Values.ContainsKey("Engine"))
                localSettings.Values["Engine"] = "默认";
            if (!localSettings.Values.ContainsKey("Animation"))
                localSettings.Values["Animation"] = "true";
            if (!localSettings.Values.ContainsKey("Update"))
                localSettings.Values["Update"] = Info.CurrentVersion;
        }
    }
}