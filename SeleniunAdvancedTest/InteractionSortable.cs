using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace SeleniunAdvancedTest
{
    [TestFixture]
    class InteractionSelectable
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private Actions _builder;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Url = "https://demoqa.com/";
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _builder = new Actions(_driver);
        }

        [Test]
        [Obsolete]
        public void Sortable_MoveFirstElement_ExpactedFirstElement_YpositionToBeDifferent_AfterMovement()
        {

            IWebElement sortableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[1]/ul/li[1]/a")));
            sortableLink.Click();

            IWebElement sourceBox = _driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div[2]/ul/li[1]"));
            IWebElement targetBox = _driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div[2]/ul/li[7]"));
            
            int targetPosYBefore = targetBox.Location.Y;

            _builder
                .Click(sourceBox)
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .MoveByOffset(0, 10)
                .Perform();

            int sourcePosYAfter = sourceBox.Location.Y;

            Assert.AreNotEqual(targetPosYBefore, sourcePosYAfter);
        }

        [Test]
        [Obsolete]
        public void Sortable_MoveFirstElement_ExpactedXpositionToBeTheSame_AfterMovement()
        {

            IWebElement sortableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[1]/ul/li[1]/a")));
            sortableLink.Click();

            IWebElement sourceBox = _driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div[2]/ul/li[1]"));
            IWebElement targetBox = _driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div[2]/ul/li[7]"));
           
            int sourcePosXBefore = sourceBox.Location.X;

            _builder
                .Click(sourceBox)
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .MoveByOffset(0, 10)
                .Perform();

            int sourcePosXAfter = sourceBox.Location.X;

            Assert.AreEqual(sourcePosXBefore, sourcePosXAfter);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
