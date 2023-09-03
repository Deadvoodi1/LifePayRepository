using System;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using UiTestFramework;

namespace LifePayProject.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class LP_1_SignUpSignInTests : CommonOperations
    {
        private string smsServiceUrl = "https://online-sms.org/ru/free-phone-number-79662025009";
        private string smsMessageTemplate = "ваш код подтверждения в life pos";
        private string pinCodeMessageTemplate = "Используйте PIN-код ";
        private string password = "qwerty123";
        private string name = "Billy Zain";
        
        [Test]
        [Retry(3)]
        public void AccountCreation()
        {
            try
            {
                var signUpLink = Driver.FindElement(By.CssSelector("a[href='/auth/sign-up']"));
                signUpLink.Click();
                
                var phoneNumberField = Driver.FindElement(By.CssSelector("input[type='tel']"));    
                phoneNumberField.SendKeys(phoneNumber);

                var sendSmsCodeButton = Driver.FindElement(By.Id("send-sms-verification"));
                sendSmsCodeButton.Click();

                SmsWebService smsWebService = new SmsWebService(smsServiceUrl, Driver);
                var smsCode = smsWebService.GetCode(smsMessageTemplate, "\\D+");
                
                var smsCodeField = 
                    Driver.FindElement(By.CssSelector("input[type='text'][class='classList ng-untouched ng-pristine ng-star-inserted ng-valid']"));
                smsCodeField.SendKeys(smsCode);

                var nameField = Driver.FindElement(By.CssSelector("input[maxlength='64']"));
                nameField.SendKeys(name);

                var passwordField = Driver.FindElement(By.CssSelector("input[type='password']"));
                passwordField.SendKeys(password);

                var createAccount = Driver.FindElement(By.Id("create-lk"));
                createAccount.Click();

                var skipButton = Uit.WaitAndGetElement(Driver, By.Id("skip-inn"));
                skipButton.Click();

                var activationCode = Uit.WaitAndGetElement(Driver, By.CssSelector("div[class='code']"));
                Assert.IsNotNull(activationCode.Text);

                var getPinCodeButton = Driver.FindElement(By.CssSelector("div[class='button-wrapper']"));
                getPinCodeButton.Click();

                var pinCode = smsWebService.GetCode(pinCodeMessageTemplate, "\\d{6}");
                Assert.IsNotNull(pinCode);

                var continueButton = Driver.FindElement(By.CssSelector("button[id='continue-outlet']"));
                continueButton.Click();
                
                Assert.IsNotNull(WaitAndGetElement(Driver, By.Id("cdk-overlay-1")));
            }
            catch (Exception ex)
            {
                Assert.Fail($"Test failed with unexpected exception. {ex.Message} {ex.StackTrace}");
            }
        }

        [Test]
        [Retry(3)]
        public void Authentication()
        {
            Login(phoneNumber, password, name);
        }
    }
}