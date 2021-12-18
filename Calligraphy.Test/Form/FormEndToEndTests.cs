using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace Calligraphy.Test.Form
{
    public class FormEndToEndTests
    {
        IWebDriver driver = new FirefoxDriver("C:\\Program Files\\Mozilla Firefox");
        TimeSpan time = TimeSpan.FromSeconds(5);
        bool status = false;

            [Fact]
            public void TestHomePageVisit()
            {
                WebDriverWait wait = new WebDriverWait(driver, time);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                driver.Url = "http://localhost:3000/";
                driver.Quit();
            }

            [Fact]
            public void TestHomePageRequestServiceButton()
            {
                WebDriverWait wait = new WebDriverWait(driver, time);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                driver.Url = "http://localhost:3000/";
                IWebElement search = driver.FindElement(By.Name("nbForm"));
                search.Click();
                driver.Quit();


            }
            [Fact]
            public void TestHomePageImageButtons()
            {
                WebDriverWait wait = new WebDriverWait(driver, time);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                driver.Url = "http://localhost:3000/";

                //Press calligraphy img then go back
                IWebElement search = driver.FindElement(By.Name("calligraphy-img"));
                search.Click();
                driver.Navigate().Back();

                //Press engraving img then go back
                search = driver.FindElement(By.Name("engraving-img"));
                search.Click();
                driver.Navigate().Back();

                //Press event img then go back and close the page
                search = driver.FindElement(By.Name("event-img"));
                search.Click();
                driver.Navigate().Back();
                driver.Quit();

            }
            [Fact]
            public void TestCalligraphyFormPost_FieldsFilled()
            {
                WebDriverWait wait = new WebDriverWait(driver, time);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                driver.Url = "http://localhost:3000/";
                IWebElement search = driver.FindElement(By.Name("nbForm"));
                search.Click();

                search = driver.FindElement(By.Name("firstName"));
                search.Click();
                search.SendKeys("John");

                search = driver.FindElement(By.Name("lastName"));
                search.Click();
                search.SendKeys("Doe");

                search = driver.FindElement(By.Name("street"));
                search.Click();
                search.SendKeys("SexyStreet");

                search = driver.FindElement(By.Name("postal"));
                search.Click();
                search.SendKeys("J4Y1P1");

                search = driver.FindElement(By.Name("city"));
                search.Click();
                search.SendKeys("CoolCity");

                search = driver.FindElement(By.Name("country"));
                search.Click();
                search.SendKeys("CockCountry");

                search = driver.FindElement(By.Name("service"));
                search.Click();

                search = driver.FindElement(By.Name("calligraphy-select"));
                search.Click();

                search = driver.FindElement(By.Name("comments"));
                search.Click();

                search.SendKeys("Test Comment Calligraphy!");

                search = driver.FindElement(By.Name("submit-btn"));
                search.Click();


                if (driver.FindElement(By.Name("success")).Displayed)
                {
                    status = true;
                }
                Assert.True(status);
            
                driver.Quit();

        }
            [Fact]
            public void TestCalligraphyFormPost_EngravingAndComment()
            {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();

            search = driver.FindElement(By.Name("firstName"));
            search.Click();
            search.SendKeys("John");

            search = driver.FindElement(By.Name("lastName"));
            search.Click();
            search.SendKeys("Doe");

            search = driver.FindElement(By.Name("street"));
            search.Click();
            search.SendKeys("SexyStreet");

            search = driver.FindElement(By.Name("postal"));
            search.Click();
            search.SendKeys("J4Y1P1");

            search = driver.FindElement(By.Name("city"));
            search.Click();
            search.SendKeys("CoolCity");

            search = driver.FindElement(By.Name("country"));
            search.Click();
            search.SendKeys("CockCountry");

            search = driver.FindElement(By.Name("service"));
            search.Click();

            search = driver.FindElement(By.Name("engraving-select"));
            search.Click();

            search = driver.FindElement(By.Name("comments"));
            search.Click();

            search.SendKeys("Test Comment Engraving!");

            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();


            if (driver.FindElement(By.Name("success")).Displayed)
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }

            [Fact]
            public void TestCalligraphyFormPost_EventAndComment()
            {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();

            search = driver.FindElement(By.Name("firstName"));
            search.Click();
            search.SendKeys("John");

            search = driver.FindElement(By.Name("lastName"));
            search.Click();
            search.SendKeys("Doe");

            search = driver.FindElement(By.Name("street"));
            search.Click();
            search.SendKeys("SexyStreet");

            search = driver.FindElement(By.Name("postal"));
            search.Click();
            search.SendKeys("J4Y1P1");

            search = driver.FindElement(By.Name("city"));
            search.Click();
            search.SendKeys("CoolCity");

            search = driver.FindElement(By.Name("country"));
            search.Click();
            search.SendKeys("CockCountry");

            search = driver.FindElement(By.Name("service"));
            search.Click();

            search = driver.FindElement(By.Name("event-select"));
            search.Click();

            search = driver.FindElement(By.Name("comments"));
            search.Click();

            search.SendKeys("Test Comment Event!");

            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();


            if (driver.FindElement(By.Name("success")).Displayed)
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }

        [Fact]
            public void TestCalligraphyFormPost_CalligraphyNoComment()
            {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();

            search = driver.FindElement(By.Name("firstName"));
            search.Click();
            search.SendKeys("John");

            search = driver.FindElement(By.Name("lastName"));
            search.Click();
            search.SendKeys("Doe");

            search = driver.FindElement(By.Name("street"));
            search.Click();
            search.SendKeys("SexyStreet");

            search = driver.FindElement(By.Name("postal"));
            search.Click();
            search.SendKeys("J4Y1P1");

            search = driver.FindElement(By.Name("city"));
            search.Click();
            search.SendKeys("CoolCity");

            search = driver.FindElement(By.Name("country"));
            search.Click();
            search.SendKeys("CockCountry");

            search = driver.FindElement(By.Name("service"));
            search.Click();

            search = driver.FindElement(By.Name("calligraphy-select"));
            search.Click();

            search = driver.FindElement(By.Name("comments"));
            search.Click();


            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();


            if (driver.FindElement(By.Name("error")).Displayed)
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }
            [Fact]
            public void TestCalligraphyFormPost_EngravingNoComment()
            {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();

            search = driver.FindElement(By.Name("firstName"));
            search.Click();
            search.SendKeys("John");

            search = driver.FindElement(By.Name("lastName"));
            search.Click();
            search.SendKeys("Doe");

            search = driver.FindElement(By.Name("street"));
            search.Click();
            search.SendKeys("SexyStreet");

            search = driver.FindElement(By.Name("postal"));
            search.Click();
            search.SendKeys("J4Y1P1");

            search = driver.FindElement(By.Name("city"));
            search.Click();
            search.SendKeys("CoolCity");

            search = driver.FindElement(By.Name("country"));
            search.Click();
            search.SendKeys("CockCountry");

            search = driver.FindElement(By.Name("service"));
            search.Click();

            search = driver.FindElement(By.Name("engraving-select"));
            search.Click();

            search = driver.FindElement(By.Name("comments"));
            search.Click();


            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();


            if (driver.FindElement(By.Name("error")).Displayed)
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }
            [Fact]
            public void TestCalligraphyFormPost_EventNoComment()
            {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();

            search = driver.FindElement(By.Name("firstName"));
            search.Click();
            search.SendKeys("John");

            search = driver.FindElement(By.Name("lastName"));
            search.Click();
            search.SendKeys("Doe");

            search = driver.FindElement(By.Name("street"));
            search.Click();
            search.SendKeys("SexyStreet");

            search = driver.FindElement(By.Name("postal"));
            search.Click();
            search.SendKeys("J4Y1P1");

            search = driver.FindElement(By.Name("city"));
            search.Click();
            search.SendKeys("CoolCity");

            search = driver.FindElement(By.Name("country"));
            search.Click();
            search.SendKeys("CockCountry");

            search = driver.FindElement(By.Name("service"));
            search.Click();

            search = driver.FindElement(By.Name("event-select"));
            search.Click();

            search = driver.FindElement(By.Name("comments"));
            search.Click();


            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();


            if (driver.FindElement(By.Name("error")).Displayed)
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }
        }
    }
