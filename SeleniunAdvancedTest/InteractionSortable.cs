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
    class InteractionSortable
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private Actions _builder;

        [SetUp]
        [Obsolete]
         public void Setup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Url = "http://www.demoqa.com/sortable";
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(100));
            _builder = new Actions(_driver);
        }

        [Obsolete]
        private void ScroolPage()
        {
            IWebElement page = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html")));
            page.SendKeys(Keys.PageDown);
        }

        [Test]
        [Obsolete]
        public void Sortable_MoveFirstElement_ExpactedFirstElement_YpositionToBeDifferent_AfterMovement()
        {
            IWebElement sourceBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div/div[1]/div/div[1]")));
            IWebElement targetBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div/div[1]/div/div[6]")));

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
            IWebElement page = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html")));
            page.SendKeys(Keys.F5);

            IWebElement sourceBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div/div[1]/div/div[1]")));
            IWebElement targetBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div/div[1]/div/div[6]")));

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
