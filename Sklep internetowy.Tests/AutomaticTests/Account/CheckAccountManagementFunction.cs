using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.AutomaticTests.Account
{
    public class CheckAccountManagementFunction
    {
        IWebDriver driver;
        ChromeOptions options;

        [SetUp]
        public void SetUp()
        {
            options = new ChromeOptions();
            options.AddArgument("--window-size=1300,1000");
            driver = new ChromeDriver(options);
        }

        [Test]
        public void CheckModifyData_WhenDataIsCorrect_DataModify()
        {

        }

        [Test]
        public void CheckModifyData_WhenDataIsNotCorrect_DisplayTextAboutError()
        {

        }

        [Test]
        public void CheckChangePassword_WhenDataIsCorrent_ChangePassword()
        {

        }

        [Test]
        public void CheckChangePassword_WhenDataIsNotCorrect_DisplayTextAboutError()
        {

        }
    }
}
