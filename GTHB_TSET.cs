using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace GTHB_TEST
{

    [TestFixture]
    public class Chrome_Sample_test
    {
        private IWebDriver driver;
        public string homeURL;


        [Test(Description = "Check SauceLabs Homepage for Login Link")]
        public void Login_is_on_home_page()
        {
            homeURL = "https://www.SauceLabs.com";
            driver.Navigate().GoToUrl(homeURL);
            WebDriverWait wait = new WebDriverWait(driver,System.TimeSpan.FromSeconds(15));
            wait.Until(driver =>driver.FindElement(By.XPath("//a[@href='https://app.saucelabs.com/login']")));
            IWebElement element = driver.FindElement(By.XPath("//a[@href='https://app.saucelabs.com/login']"));
            Assert.AreEqual("Sign in", element.GetAttribute("text"));

        }

        [Test(Description = "Check Nordicmaster github io")]
        public void gtp_storage()
        {
            homeURL = "https://nordicmaster.github.io";
            driver.Navigate().GoToUrl(homeURL);
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.Id("bodycenter")));
            IWebElement element = driver.FindElement(By.Id("bodycenter"));
            driver.Navigate().GoToUrl("https://nordicmaster.github.io/about.html");
            WebDriverWait wait2 = new WebDriverWait(driver, System.TimeSpan.FromSeconds(10));
            wait2.Until(driver => driver.FindElement(By.Id("bodycenter")));
            driver.Navigate().Back();
            element = driver.FindElement(By.Id("bodycenter"));
            Assert.AreNotEqual(element, null);
            Assert.AreNotEqual(element.Size.Width, null);
            Assert.Greater(1080, element.Size.Width);
            var lst = driver.FindElements(By.ClassName("justrow"));
            var logs = driver.Manage().Logs.GetLog(LogType.Browser);
            foreach (var entry in logs)
            {
                Console.WriteLine(entry.ToString());
            }
            foreach (var item in lst)
            {
                Assert.AreNotEqual(item, null);
                var childs = item.FindElements(By.ClassName("itempart2"));
                foreach (var dd in childs)
                { 
                    Assert.AreNotEqual(dd, null);
                    dd.FindElement(By.XPath("descendant::button")).Click();
                    Assert.AreNotEqual(dd.Text, null);
                    Console.WriteLine(dd.Text);
                }
            }
            Console.ReadKey();
        }


        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }


        [SetUp]
        public void SetupTest()
        {
            homeURL = "http://SauceLabs.com";
            driver = new ChromeDriver(@"C:\Users\dryabuhin\path");

        }


    }
}
