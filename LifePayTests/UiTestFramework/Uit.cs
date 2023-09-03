using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace UiTestFramework
{
    public class Uit
    {
        public static IWebElement WaitAndGetElement(IWebDriver webDriver, By locator, int sec = 60)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(sec));
            IWebElement element = wait.Until(driver =>
            {
                var tempElement = driver.FindElement(locator);
                return (tempElement.Displayed && tempElement.Enabled) ? tempElement : null;
            });

            return element;
        }

        public static void SwitchToAnotherWinHandle(IWebDriver webDriver)
        {
            ReadOnlyCollection<string> collection = webDriver.WindowHandles;
            var currentHandle = webDriver.CurrentWindowHandle;
            
            for (int i = 0; i < collection.Count; i++)
            {
                if (!(currentHandle == collection[i]))
                    webDriver.SwitchTo().Window(collection[i]);
            }
        }

        public static void ScrollToTheElement(IWebDriver webDriver, IWebElement element)
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(element);
            actions.Perform();
        }
    }
}