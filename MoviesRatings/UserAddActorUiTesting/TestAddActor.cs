using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace UserAddActorUiTesting
{
    [TestClass]
    public class TestAddActor
    {
        private TestContext testContextInstance;
        private IWebDriver driver;
        private string appURL;
        private WebDriverWait wait;
        

        private const string expectedFirstNameEmptyErrMsg = "First Name is requried";
        private const string expectedLastNameEmptyErrMsg = "Last Name is requried";
        private const string expectedFirstNameOutOfBoundErrMsg = "First Name must be between 2 and 50 characters long";
        private const string expectedLastNameOutOfBoundErrMsg = "Last Name must be between 2 and 50 characters long";
        private const string expectedGenderRequiredErrMsg = "Gender is required";
        private const string expectedAlertAddedMsg = "You have successfully added new Actor/Actress";
        private const string expectedAlertEditdMsg = "You have successfully updated Actor/Actress";

        [TestMethod]
        [TestCategory("Chrome")]
        public void TestValidAddNewActor()
        {            
            ///navigate to the actor page
            driver.Navigate().GoToUrl(appURL);         
            //Wait for 4 second    
            WaitFor(2);
            //Calling helper method to add new actor
            AddActor("Morgan", "Freeman", "Male");
            var modelBtnAdd = wait.Until(e => e.FindElement(By.Id("modelBtnAdd"))); ;
            modelBtnAdd.Click();
            WaitFor(1);
            //Check for successful added message
            var alert = driver.SwitchTo().Alert();
            Assert.AreEqual(expectedAlertAddedMsg, alert.Text);
            alert.Accept();
            //Removing Morgan Freeman
            RemoveActor(3);
        }
        [TestMethod]
        [TestCategory("Chrome")]
        public void TestExceptionCreateActorWithoutFirstName()
        {
            AddActor("", "Reynolds", "Male");
            var modelBtnAdd = wait.Until(e => e.FindElement(By.Id("modelBtnAdd"))); ;
            modelBtnAdd.Click();
            WaitFor(4);
            //check for validation error
            var firstNameError = driver.FindElement(By.Id("firstNameErr"));
            Assert.AreEqual(expectedFirstNameEmptyErrMsg, firstNameError.GetAttribute("textContent"));
        }
        #region Test Helper methods  
        /*
         * This is a helper method that is used to create new actor
         * fName: actor first name
         * lName: actor last name
         */
        private void AddActor(string fName, string lName, string genderType)
        {
            //set url
            appURL = "localhost:5000/actor";
            //navigate to the actor page
            driver.Navigate().GoToUrl(appURL);
            //wait for the Add Actor button to show up
            WaitFor(2);
            var btnAdd = wait.Until(e => e.FindElement(By.Id("btnAddActor")));            
            btnAdd.Click();
            System.Threading.Thread.Sleep(4000);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            //wait for the model to loadup properly 
            wait.Until(e => e.FindElement(By.Id("modelBtnAdd")));

            //Enter new actor
            var firstName = driver.FindElement(By.Id("firstName"));
            var lastName = driver.FindElement(By.Id("lastName"));
            var gender = new SelectElement(driver.FindElement(By.Id("gender")));
            
            firstName.SendKeys(fName);
            WaitFor(1);
            lastName.SendKeys(lName);
            WaitFor(1);
            gender.SelectByValue(genderType);
            WaitFor(1);
        }
        /// <summary>
        /// helper method use to slow down the process
        /// </summary>
        /// <param name="second"></param>
        private void WaitFor(int second)
        {
            System.Threading.Thread.Sleep(second * 1000);
        }
        /**
         * This is a helper method that is used to remove actor from the database
         */
        private void RemoveActor(int order)
        {
            var btnDelete = wait.Until(e => e.FindElement(By.Id("btnDelete_" + order)));
            btnDelete.Click();
            WaitFor(1);
            var alert = driver.SwitchTo().Alert();
            alert.Accept();
        }
        #endregion
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize()]
        public void SetupTest()
        {
            appURL = "http://localhost:5000/";
            //appURL = "http://eaapp.somee.com/";
            string browser = "chrome";
            switch (browser)
            {
                case "chrome":
                    ChromeOptions caps = new ChromeOptions();
                    caps.AddArgument("--ignore-ssl-errors=yes");
                    caps.AddArgument("--ignore-certificate-errors");
                    driver = new ChromeDriver(caps);

                    break;
                case "FireFox":
                    driver = new FirefoxDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }
            //initialize wait element, this will be used to instruct the system 
            //to wait until a specified element appeared
            TimeSpan duration = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, duration);
        }

        [TestCleanup()]
        public void MytestCleanup()
        {
            driver.Quit();
        }
    }
}
