using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using UiTestFramework;

namespace LifePayProject
{
    public class CommonOperations : UiTestSession
    {
        public CommonOperations()
        {
            LifePayUrl = "https://my.life-pos.ru/auth/login";
        }

        public void Login(string phone, string password, string name)
        {
            try
            {
                var phoneNumberField = Driver.FindElement(By.CssSelector("input[type='tel']"));
                phoneNumberField.SendKeys(phone);
                var passwordField = Driver.FindElement(By.CssSelector("input[type='password']"));
                passwordField.SendKeys(password);

                var loginButton = Driver.FindElement(By.CssSelector("button[id='login-lk']"));
                loginButton.Click();

                var welcomeTitle = TryingFindElementText(By.CssSelector("lp-select-organization>div>div>h2"));
                Assert.AreEqual($"С возвращением, {name}", welcomeTitle);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Test failed with unexpected exception. {ex.Message} {ex.StackTrace}");
            }
        }

        public string TryingFindElementText(By by)
        {
            string elementText = null;
            var attemps = 0;
            while (attemps <= 30)
            {
                try
                {
                    var element = WaitAndGetElement(Driver, by);
                    elementText = element.Text;
                    break;
                }
                catch (StaleElementReferenceException ex)
                {
                    Console.WriteLine(ex);
                }
                attemps++;
                Thread.Sleep(2000);
            }

            return elementText;
        }
    }
}