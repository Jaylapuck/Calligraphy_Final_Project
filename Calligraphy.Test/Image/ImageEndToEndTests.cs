using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace Calligraphy.Test.Image
{
    public class ImageEndToEndTests
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl;
        
        public ImageEndToEndTests()
        {
            _baseUrl = "http://localhost:3000";
            _driver = new FirefoxDriver("C:\\Program Files\\Mozilla Firefox");
        }

        [Fact]
        public void Image_addImageAtLocationOne_ReturnOk ()
        {
            _driver.Navigate().GoToUrl(_baseUrl + "/admin");
            //click on image link with href /admin/portfolio/image/1
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[1]/a[1]")).Click();
            _driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = _driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys("C:\\Users\\ethyl\\RiderProjects\\Calligraphy_Final_Project\\Calligraphy.Test\\Images\\Calligraphy.jpg");
            //click the upload button
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            
        }
        
        [Fact]
        
        public void Image_addImageAtLocationTwo_ReturnOk ()
        {
            _driver.Navigate().GoToUrl(_baseUrl + "/admin");
            //click on image link with href /admin/portfolio/image/2
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[1]/a[2]/img")).Click();
            _driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = _driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys("C:\\Users\\ethyl\\RiderProjects\\Calligraphy_Final_Project\\Calligraphy.Test\\Images\\Calligraphy.jpg");
            //click the upload button
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            
        }

        [Fact]
        public void Image_addImageAtLocationThree_ReturnOk()
        {
            _driver.Navigate().GoToUrl(_baseUrl + "/admin");
            //click on image link with href /admin/portfolio/image/3
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[2]/a[1]")).Click();
            _driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = _driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys("C:\\Users\\ethyl\\RiderProjects\\Calligraphy_Final_Project\\Calligraphy.Test\\Images\\Calligraphy.jpg");
            //click the upload button
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            
        }
        
        [Fact]
        public void Image_addImageAtLocationFour_ReturnOk()
        {
            _driver.Navigate().GoToUrl(_baseUrl + "/admin");
            //click on image link with href /admin/portfolio/image/4
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[2]/a[2]")).Click();
            _driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = _driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys("C:\\Users\\ethyl\\RiderProjects\\Calligraphy_Final_Project\\Calligraphy.Test\\Images\\Calligraphy.jpg");
            //click the upload button
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            
        }
        
        [Fact]
        public void Image_addImageAtLocationFive_ReturnOk()
        {
            _driver.Navigate().GoToUrl(_baseUrl + "/admin");
            //click on image link with href /admin/portfolio/image/5
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[3]/a[1]/img")).Click();
            _driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = _driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys("C:\\Users\\ethyl\\RiderProjects\\Calligraphy_Final_Project\\Calligraphy.Test\\Images\\Calligraphy.jpg");
            //click the upload button
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            
        }
        
        [Fact]
        public void Image_addImageAtLocationSix_ReturnOk()
        {
            _driver.Navigate().GoToUrl(_baseUrl + "/admin");
            //click on image link with href /admin/portfolio/image/6
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[3]/a[2]/img")).Click();
            _driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = _driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys("C:\\Users\\ethyl\\RiderProjects\\Calligraphy_Final_Project\\Calligraphy.Test\\Images\\Calligraphy.jpg");
            //click the upload button
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
        }
        
        //TODO: Add tests for the following:
        //Test above need to be fully implemented
        //update image at location 1, don't need to do all of them since they are the same as the add tests

        //verify that the images are there
        [Fact]
        public void Image_AccessPortfolioPage()
        {
            _driver.Navigate().GoToUrl(_baseUrl + "/portfolio");
        }
    }
}