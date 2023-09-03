using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using LifePayProject.Tests;
using NUnit.Framework;
using OpenQA.Selenium;
using UiTestFramework;

namespace LifePayProject
{
    public class SmsWebService : UiTestSession
    {
        private string smsServiceUrl;
        public IWebDriver driver; 
        public SmsWebService(string smsServiceUrl, IWebDriver driver)
        {
            this.smsServiceUrl = smsServiceUrl;
            this.driver = driver;
        }
        
        public string GetCode(string messageTemplate, string pattern)
        {
            string smsCode = null;
           
            try
            {
                OpenNewTab(smsServiceUrl, driver);
                
                var lifePosElement = WaitForSmSMessage(messageTemplate);
                var lifePosMessage = lifePosElement.Text;
                smsCode = Regex.Replace(lifePosMessage, pattern, "");
                Uit.SwitchToAnotherWinHandle(Driver);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Test failed with unexpected exception. {ex.Message} {ex.StackTrace}");
            }
            return smsCode;
        }
        
        private IWebElement WaitForSmSMessage(string messageTemplate, int maximumWaitTimeMs = 5 * 60 * 1000)
        {
            var watch = Stopwatch.StartNew();

            var lifePosElement = GetLifePosElement(messageTemplate);
            while (lifePosElement == null)
            {
                Thread.Sleep(10 * 1000);
                Driver.Navigate().Refresh();
                lifePosElement = GetLifePosElement(messageTemplate);
                
                if (watch.ElapsedMilliseconds > maximumWaitTimeMs)
                    Assert.Fail();
            }

            return lifePosElement;
        }

        private IWebElement GetLifePosElement(string messageTemplate)
        {
            var messages = Driver.FindElements(By.CssSelector("td:nth-child(2)"));
            var lifePosElement = 
                messages.FirstOrDefault(message => message.Text.ToLower().Contains(messageTemplate));
            
            return lifePosElement;
        }
    }
}