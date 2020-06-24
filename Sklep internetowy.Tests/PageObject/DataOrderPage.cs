using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Sklep_internetowy.Tests.AutomaticTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.PageObject
{
    public class DataOrderPage
    {
        public IWebDriver Driver { get; set; }

        public DataOrderPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(this, new DefaultElementLocator(driver));
        }

        [FindsBy(How = How.Id, Using = "Firstname")]
        public IWebElement FirstnameText { get; set; }

        [FindsBy(How = How.Id, Using = "Lastname")]
        public IWebElement LastnameText { get; set; }

        [FindsBy(How = How.Id, Using = "Adress")]
        public IWebElement AdressText { get; set; }

        [FindsBy(How = How.Id, Using = "City")]
        public IWebElement CityText { get; set; }

        [FindsBy(How = How.Id, Using = "ZipCode")]
        public IWebElement ZipCodeText { get; set; }

        [FindsBy(How = How.Id, Using = "PhoneNumber")]
        public IWebElement PhonenumberText { get; set; }

        [FindsBy(How = How.Id, Using = "Email")]
        public IWebElement EmailText { get; set; }

        [FindsBy(How = How.Id, Using = "placeOrder")]
        public IWebElement PlaceOrderButton { get; set; }

        public void FillForm()
        {
            this.FirstnameText.Clear();
            this.FirstnameText.SendKeys(OrderDataHelper.Firstname);
            this.LastnameText.Clear();
            this.LastnameText.SendKeys(OrderDataHelper.Lastname);
            this.AdressText.Clear();
            this.AdressText.SendKeys(OrderDataHelper.Adress);
            this.CityText.Clear();
            this.CityText.SendKeys(OrderDataHelper.City);
            this.ZipCodeText.Clear();
            this.ZipCodeText.SendKeys(OrderDataHelper.ZipCode);
            this.PhonenumberText.Clear();
            this.PhonenumberText.SendKeys(OrderDataHelper.PhoneNumber);
            
        }

        public void NavigateToConfirmOrder()
        {
            this.PlaceOrderButton.Click();
        }
    }
}
