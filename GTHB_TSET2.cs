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
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(15));
            wait.Until(driver => driver.FindElement(By.XPath("//a[@href='https://app.saucelabs.com/login']")));
            IWebElement element = driver.FindElement(By.XPath("//a[@href='https://app.saucelabs.com/login']"));
            Assert.AreEqual("Sign in", element.GetAttribute("text"));

        }

        [Test(Description = "Check Nordicmaster github io")]
        public void gtp_storage()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\ryabukhinda\source\repos\ConsoleApp413\GTHB.txt"))
            {
                homeURL = "https://nordicmaster.github.io";
                driver.Navigate().GoToUrl(homeURL);
                IWebElement element = driver.FindElement(By.ClassName("bodytype"));
                driver.Navigate().GoToUrl(homeURL + "/about.html");
                WebDriverWait wait2 = new WebDriverWait(driver, System.TimeSpan.FromSeconds(10));
                wait2.Until(driver => driver.FindElement(By.ClassName("bodytype")));
                driver.Navigate().Back();
                driver.Navigate().Refresh();
                element = driver.FindElement(By.ClassName("bodytype"));
                Assert.AreNotEqual(element, null);
                Assert.AreNotEqual(element.Size.Width, null);
                Assert.Greater(1080, element.Size.Width);
                var lst = driver.FindElements(By.ClassName("solid"));

                double sumall = 0;
                foreach (var item in lst)
                {
                    Assert.AreNotEqual(item, null);
                    var childs = item.FindElements(By.XPath("descendant::div[2]"));
                    foreach (var dd in childs)
                    {
                        Assert.AreNotEqual(dd, null);
                        dd.FindElement(By.XPath("descendant::summary")).Click();
                        Assert.AreNotEqual(dd.Text, null);
                        file.WriteLine(dd.FindElement(By.CssSelector("p")).Text);
                    }
                    childs = item.FindElements(By.CssSelector("div[class='inlineblock vertical-align marginleft halfwidth smalltext']"));
                    foreach (var dd in childs)
                    {
                        Assert.AreNotEqual(dd, null);
                        var len = dd.FindElement(By.ClassName("ng-scope"));
                        double numlen = 0;
                        try
                        {
                            numlen = Convert.ToDouble(len.Text.Remove(len.Text.IndexOf(' ')), System.Globalization.CultureInfo.InvariantCulture);
                            sumall += numlen;
                        }
                        catch (Exception e)
                        {
                            file.WriteLine("EXCEPTION  " + len.Text);
                            file.WriteLine(len.Text.Remove(len.Text.IndexOf(' ')));
                            continue;
                        }
                        Assert.AreNotEqual(numlen, null);
                        Assert.Greater(numlen, 0.0);
                        file.WriteLine(numlen);
                    }
                }
                file.WriteLine("total: " + Convert.ToInt32(sumall) / 60 + " : " + Convert.ToInt32(sumall) % 60);
            }
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
            driver = new ChromeDriver(@"C:\Users\ryabukhinda\source\repos\ConsoleApp413");

        }

        public static void Main()
        {
            ;
        }
    }
}
