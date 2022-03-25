﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;

namespace NUnitSelenium
{
    [TestFixture("Galaxy S9", "10", "Android", "lt://APP10020921644598251044639")]
    [TestFixture("OnePlus 6T", "9", "Android", "lt://APP10020921644598251044639")]
    //[TestFixture("firefox", "60", "Windows 7")]
    //[TestFixture("chrome", "71", "Windows 7")]
    //[TestFixture("internet explorer", "11", "Windows 10")]
   // [TestFixture("firefox", "58", "Windows 7")]
    //[TestFixture("chrome", "67", "Windows 7")]
   // [TestFixture("internet explorer", "10", "Windows 7")]
   // [TestFixture("firefox", "55", "Windows 7")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class NUnitSeleniumSample
    {
        public static string LT_USERNAME = Environment.GetEnvironmentVariable("LT_USERNAME") ==null ? "your username" : Environment.GetEnvironmentVariable("LT_USERNAME");
        public static string LT_ACCESS_KEY = Environment.GetEnvironmentVariable("LT_ACCESS_KEY") == null ? "your accessKey" : Environment.GetEnvironmentVariable("LT_ACCESS_KEY");
        public static bool tunnel = Boolean.Parse(Environment.GetEnvironmentVariable("LT_TUNNEL")== null ? "false" : Environment.GetEnvironmentVariable("LT_TUNNEL"));       
        public static string build = Environment.GetEnvironmentVariable("LT_BUILD") == null ? "your build name" : Environment.GetEnvironmentVariable("LT_BUILD");
        public static string seleniumUri = "https://beta-hub.lambdatest.com:443/wd/hub";





        AndroidDriver<AndroidElement> driver;


        private String deviceName;
        private String platformVersion;
        private String platformName;
        private String app;

       // public object process;

        public NUnitSeleniumSample(String deviceName, String platformVersion, String platformName, String app)
        {
            this.deviceName = deviceName;
            this.platformVersion = platformVersion;
            this.platformName = platformName;
            this.app = app;
        }

        [SetUp]
        public void Init()
        {

            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("user", "shubhamr");
            capabilities.AddAdditionalCapability("accessKey", "M1hTTfelp95y0WKq0MSKORBzWD7xpFGOTv5KlMTZ18qnAcGjId");
            capabilities.AddAdditionalCapability("app",app);
            capabilities.AddAdditionalCapability("deviceName", deviceName);
            capabilities.AddAdditionalCapability("platformVersion", platformVersion);
            capabilities.AddAdditionalCapability("platformName", platformName);
            capabilities.AddAdditionalCapability("build", "App Automation2");
            capabilities.AddAdditionalCapability("name", "First App testing2");
            capabilities.AddAdditionalCapability("isRealMobile", true);

            
                 driver = new AndroidDriver<AndroidElement> (new Uri(seleniumUri), capabilities, TimeSpan.FromSeconds(600));
            
            Console.Out.WriteLine(driver);
            Console.Out.WriteLine(deviceName+" "+platformVersion+" "+platformName);
            Console.Out.WriteLine("CAPABILITIES=======" + capabilities);
        }

        [Test]
        public void Todotest()
        {
            {
                Console.Out.WriteLine("driver"+driver);
                Console.WriteLine("Navigating to todos app.");
                AndroidElement searchElement = (AndroidElement)new WebDriverWait(
                 driver, TimeSpan.FromSeconds(30)).Until(
                 SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    MobileBy.AccessibilityId("Search Wikipedia"))
             );
                searchElement.Click();

                AndroidElement insertTextElement = (AndroidElement)new WebDriverWait(
                driver, TimeSpan.FromSeconds(30)).Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    MobileBy.Id("org.wikipedia.alpha:id/search_src_text"))
            );
                insertTextElement.SendKeys("Cristiano Ronaldo");
                System.Threading.Thread.Sleep(5000);

                driver.PressKeyCode(AndroidKeyCode.Enter);

            }
        }

        [TearDown]
        public void Cleanup()

        {
            
            bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
            try
            {
                // Logs the result to LambdaTest
                ((IJavaScriptExecutor)driver).ExecuteScript("lambda-status=" + (passed ? "passed" : "failed"));
               
               // Console.WriteLine("Tunnel Stopped");
            }
            finally
            {
                
                driver.Quit();
            }

            

         






        }
    }
}
