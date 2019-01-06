using OpenQA.Selenium;
using System.Threading;
using AgeRangerWebUi.Utilities;
using OpenQA.Selenium.Support.UI;
using System;

namespace AgeRangerWebUi.Steps
{
    public class BaseClass
    {
        public static IWebDriver Driver { get; set; }

        public static void Sleep(int Seconds)
        {
            Thread.Sleep(Seconds * 1000);
        }
       public bool ElementIsPresent(IWebElement element, int waitSeconds = CommonConstants.DriverSettings.DefaultWaitTime)
        {
            int i = 0;
            while (i < waitSeconds)
            {
                if (element.Displayed && element.Enabled)
                {
                    return true;
                }
                Sleep(1);
                i++;
            }

            return false;
        }
    }
}
