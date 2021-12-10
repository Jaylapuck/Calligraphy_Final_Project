using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Calligraphy.Tests.FormTests
{
    [TestClass]
    public class FormEndToEndTests
    {
        IWebDriver driver;

        [TestInitialize]
        public void Initialize()
        {
           driver  = new FirefoxDriver("C:\\Program Files\\Mozilla Firefox");
           
        }

        [TestMethod]
        public void TestHomePageVisit()
        {
            driver.Url = "http://localhost:3000/";
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
        }

        [TestMethod]
        public void TestHomePageRequestServiceButton()
        {
            driver.Url = "http://localhost:3000/";
            System.Threading.Thread.Sleep(2000);
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            System.Threading.Thread.Sleep(2000);
            search.Click();
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
            System.Threading.Thread.Sleep(2000);

        }
        [TestMethod]
        public void TestHomePageImageButtons()
        {
            driver.Url = "http://localhost:3000/";
            System.Threading.Thread.Sleep(2000);

            //Press calligraphy img then go back
            IWebElement search = driver.FindElement(By.Name("calligraphy-img"));
            System.Threading.Thread.Sleep(2000);
            search.Click();
            System.Threading.Thread.Sleep(2000);
            driver.Navigate().Back();
            System.Threading.Thread.Sleep(2000);

            //Press engraving img then go back
            search = driver.FindElement(By.Name("engraving-img"));
            System.Threading.Thread.Sleep(2000);
            search.Click();
            System.Threading.Thread.Sleep(2000);
            driver.Navigate().Back();
            System.Threading.Thread.Sleep(2000);

            //Press event img then go back and close the page
            search = driver.FindElement(By.Name("event-img"));
            System.Threading.Thread.Sleep(2000);
            search.Click();
            System.Threading.Thread.Sleep(2000);
            driver.Navigate().Back();
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
            System.Threading.Thread.Sleep(2000);

        }
        [TestMethod]
        public void TestCalligraphyFormPost_CalligraphyAndComment()
        {
            driver.Url = "http://localhost:3000/";
            System.Threading.Thread.Sleep(2000);
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("service"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("calligraphy-select"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("comments"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search.SendKeys("Test Comment Calligraphy!");
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
            System.Threading.Thread.Sleep(2000);

        }
        [TestMethod]
        public void TestCalligraphyFormPost_EngravingAndComment()
        {
            driver.Url = "http://localhost:3000/";
            System.Threading.Thread.Sleep(2000);
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("service"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("engraving-select"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("comments"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search.SendKeys("Test Comment Engraving!");
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
            System.Threading.Thread.Sleep(2000);

        }
        [TestMethod]
        public void TestCalligraphyFormPost_EventAndComment()
        {
            driver.Url = "http://localhost:3000/";
            System.Threading.Thread.Sleep(2000);
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("service"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("event-select"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("comments"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search.SendKeys("Test Comment Event!");
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
            System.Threading.Thread.Sleep(2000);

        }

        [TestMethod]
        public void TestCalligraphyFormPost_CalligraphyNoComment()
        {
            driver.Url = "http://localhost:3000/";
            System.Threading.Thread.Sleep(2000);
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("service"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("calligraphy-select"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
            System.Threading.Thread.Sleep(2000);

        }
        [TestMethod]
        public void TestCalligraphyFormPost_EngravingNoComment()
        {
            driver.Url = "http://localhost:3000/";
            System.Threading.Thread.Sleep(2000);
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("service"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("engraving-select"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
            System.Threading.Thread.Sleep(2000);

        }
        [TestMethod]
        public void TestCalligraphyFormPost_EventNoComment()
        {
            driver.Url = "http://localhost:3000/";
            System.Threading.Thread.Sleep(2000);
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("service"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("event-select"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
            System.Threading.Thread.Sleep(2000);

        }
    }
}
