using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calligraphy.Test.Mailer
{
    public class MailerEndToEndTests
    {
        IWebDriver driver = new FirefoxDriver("C:\\Program Files\\Mozilla Firefox");
        TimeSpan time = TimeSpan.FromSeconds(5);
        bool status = false;

        [Fact]
        public void SendEmailWithNoAttachmentsTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Url = "http://localhost:3000/form";
            IWebElement fNameFeild = driver.FindElement(By.Name("firstName"));
            IWebElement lNameFeild = driver.FindElement(By.Name("lastName"));
            IWebElement emailField = driver.FindElement(By.Name("email"));
            IWebElement streetField = driver.FindElement(By.Name("street"));
            IWebElement postalFeild = driver.FindElement(By.Name("postal"));
            IWebElement cityFeild = driver.FindElement(By.Name("city"));
            IWebElement countryFeild = driver.FindElement(By.Name("country"));
            IWebElement serviceFeild = driver.FindElement(By.Name("service"));
            IWebElement commentsFeild = driver.FindElement(By.Name("comments"));
            IWebElement attachmentsField = driver.FindElement(By.Name("attachments"));
            IWebElement submitBtn = driver.FindElement(By.Name("submit-btn"));

            fNameFeild.SendKeys("Tristan");
            lNameFeild.SendKeys("Lafleur");
            emailField.SendKeys("tristanblacklafleur@hotmail.ca");
            streetField.SendKeys("32 rue René");
            postalFeild.SendKeys("J2X 5S8");
            cityFeild.SendKeys("Saint-Jean-sur-Richelieu");
            countryFeild.SendKeys("Canada");
            serviceFeild.Click();
            serviceFeild.FindElement(By.Name("calligraphy-select")).Click();
            commentsFeild.SendKeys("pls god, i'm begging through text write some fancy text");

            var projectRoot = Path.GetFullPath(@"..\..\..\");
            attachmentsField.SendKeys(projectRoot + @"Mailer\TestFiles\23784.png");

            submitBtn.Click();

            driver.SwitchTo().Alert().Accept();

            string emailRequestSentAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (emailRequestSentAlertText.Equals("Thank you for your request, an email has been sent your way!"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();
        }
    }
}
