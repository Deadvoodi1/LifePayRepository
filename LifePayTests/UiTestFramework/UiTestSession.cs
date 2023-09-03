using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace UiTestFramework
{
    public class UiTestSession
    {
        public string LifePayUrl { get; set; }
        
        public string phoneNumber = "79662025009";

        public IWebDriver Driver;

        [SetUp]
        public void RunBeforeAnyTest()
        {
            Driver?.Quit();
            Driver = new ChromeDriver();
            OpenWebsite();
            MaximizeWindow();
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            Driver.Quit();
        }

        public void OpenWebsite()
        {
            Driver.Navigate().GoToUrl(LifePayUrl);
        }

        public void OpenWebsite(string website)
        {
            Driver.Navigate().GoToUrl(website);
        }

        public void MaximizeWindow()
        {
            Driver.Manage().Window.Maximize();
        }
        
        public void StopLoadingPage()
        {
            var actions = new Actions(Driver);
            actions.SendKeys(Keys.Escape);
        }
        
        public void OpenNewTab(string website, IWebDriver driver)
        {
            if (Driver == null)
                Driver = driver;
            
            Driver.SwitchTo().NewWindow(WindowType.Tab);
            Driver.Navigate().GoToUrl(website);
        }
        
        public static IWebElement WaitAndGetElement(IWebDriver webDriver, By locator, int sec = 30)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(sec));
            IWebElement element = wait.Until(driver =>
            {
                var tempElement = driver.FindElement(locator);
                return (tempElement.Displayed && tempElement.Enabled) ? tempElement : null;
            });

            return element;
        }
    }
}