using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LifePayProject.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class LP_2_GreetingChange : CommonOperations
    {
        
        
        [Test]
        [Retry(3)]
        public void GreetingChange()
        {
            SetDateTime(2023, 9, 1, 21, 30);
            Assert.IsTrue(IsGreetingTitleCorrect(Greetings.GoodNight));
            
            SetDateTime(2023, 9, 1, 6, 30);
            Assert.IsTrue(IsGreetingTitleCorrect(Greetings.GoodMorning));
            
            SetDateTime(2023, 9, 1, 9, 30);
            Assert.IsTrue(IsGreetingTitleCorrect(Greetings.GoodAfternoon));
            
            SetDateTime(2023, 9, 1, 15, 30);
            Assert.IsTrue(IsGreetingTitleCorrect(Greetings.GoodEvening));
        }

        public bool IsGreetingTitleCorrect(string pattern, int maximumWaitTimeMs = 5 * 60 * 1000)
        {
            var watch = Stopwatch.StartNew();
            var greetingTitle = Driver.FindElement(By.CssSelector("h2[class='title']"));
            while (!greetingTitle.Text.Contains(pattern))
            {
                Driver.Navigate().Refresh();
                Thread.Sleep(5 * 1000);
                greetingTitle = Driver.FindElement(By.CssSelector("h2[class='title']"));

                if (watch.ElapsedMilliseconds > maximumWaitTimeMs)
                    return false;
            }

            return true;
        }

        public void SetDateTime(short year, short month, short day, short hour, short minute)
        {
            SystemTime systemTime = new SystemTime();
            systemTime.wYear = year;
            systemTime.wMonth = month;
            systemTime.wDay = day;
            systemTime.wHour = hour;
            systemTime.wMinute = minute;

            SetSystemTime(ref systemTime);
        }
        
        
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SystemTime st);
    }
}