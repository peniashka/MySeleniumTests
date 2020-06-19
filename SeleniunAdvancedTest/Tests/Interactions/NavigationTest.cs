using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SeleniunAdvancedTest
{
    [TestFixture]
    class NavigationTest : BaseVariables
    {

        [SetUp]
        public void Setup()
        {
            Initialize();
            Driver.Navigate().GoToUrl("http://www.demoqa.com");
        }

        [Test]
        [TestCase("Sortable")]
        [TestCase("Selectable")]
        [TestCase("Resizable")]
        [TestCase("Dragabble")]
        [TestCase("Droppable")]
        public void NavigatoToInteractionMenu(string menubuttonName)
        {
            IWebElement menuInteractions = Driver.FindElement(By.XPath("//h5[normalize-space(text())='Interactions']/ancestor::*[@class='card mt-4 top-card']"));
            menuInteractions.Click();

            IWebElement optionalButtons = Driver.FindElement(By.XPath($"//*[normalize-space(text())='{menubuttonName}']"));
            Driver.ScrollTo(optionalButtons);
            optionalButtons.Click();

            IWebElement pageHeader = Driver.FindElement(By.ClassName("main-header"));
            Assert.AreEqual(menubuttonName, pageHeader.Text);
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}