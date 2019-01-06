namespace AgeRangerWebUi.Utilities
{
    public static class CommonConstants
    {
        public static class DriverSettings
        {
            public static string BinaryLocation = @"C:\Automation\Drivers\chromedriver.exe";

            public static string FireFoxBrowser = "FireFox";
            public static string ChromeBrowser = "Chrome";
            public static string IEBrowser = "IE";
            public static string WindowsPlatform = "Windows";

            public const int DefaultWaitTime = 30;
        }

        public static class ApplicationSettings
        {
            public static string BaseUrl = "http://ageranger.azurewebsites";
        }
    }
}
