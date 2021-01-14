/**Author: Hieu Nguyen
 */
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
        public void Test1_validAddNewActor()
        {            
                 
            //Wait for 2 second    
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
        public void Test2_exceptionCreateActorWithoutFirstName()
        {
            AddActor("", "Reynolds", "Male");
            var modelBtnAdd = wait.Until(e => e.FindElement(By.Id("modelBtnAdd"))); 
            modelBtnAdd.Click();
            WaitFor(2);
            //check for validation error
            var firstNameError = driver.FindElement(By.Id("firstNameErr"));
            Assert.AreEqual(expectedFirstNameEmptyErrMsg, firstNameError.GetAttribute("textContent"));
        }
        [TestMethod]
        [TestCategory("Chrome")]
        public void Test3_exceptionCreateActorWithoutLastName()
        {
            AddActor("Ryan", "", "Male");
            var modelBtnAdd = wait.Until(e => e.FindElement(By.Id("modelBtnAdd"))); ;
            modelBtnAdd.Click();
            WaitFor(2);
            //check for validation error
            var lastNameError = driver.FindElement(By.Id("lastNameErr"));
            Assert.AreEqual(expectedLastNameEmptyErrMsg, lastNameError.GetAttribute("textContent"));

        }
        [TestMethod]
        [TestCategory("Chrome")]
        public void Test4_exceptionCreateActorWithoutSelectAgenderType()
        {
            AddActor("Ryan", "Renold", "-1");
            var modelBtnAdd = wait.Until(e => e.FindElement(By.Id("modelBtnAdd"))); ;
            modelBtnAdd.Click();
            WaitFor(2);
            //check for validation error
            var genderErr = driver.FindElement(By.Id("genderErr"));
            Assert.AreEqual(expectedGenderRequiredErrMsg, genderErr.GetAttribute("textContent"));

        }
        [TestMethod]
        [TestCategory("Chrome")]
        public void Test5_validBoundaryFirstNameWithTwoCharacters()
        {

            AddActor("Ba", "Pitt", "Male");
            var modelBtnAdd = driver.FindElement(By.Id("modelBtnAdd"));
            modelBtnAdd.Click();
            WaitFor(2);
            //Check for successful added message
            //Check for the first row's value            
            var alert = driver.SwitchTo().Alert();
            Assert.AreEqual(expectedAlertAddedMsg, alert.Text);
            alert.Accept();
            //Clean up
            RemoveActor(3);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void Test6_validBoundaryFirstNameWith50Characters()
        {
            //generate a line of repeated a 50 times            
            AddActor(new string('a', 50), "Damon", "Male");
            var modelBtnAdd = driver.FindElement(By.Id("modelBtnAdd"));
            modelBtnAdd.Click();
            WaitFor(2);
            //Check for successful added message
            //Check for the first row's value
            var alert = driver.SwitchTo().Alert();
            Assert.AreEqual(expectedAlertAddedMsg, alert.Text);
            alert.Accept();
            //Clean up
            RemoveActor(3);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void Test7_invalidBoundaryFirstNameWithOneCharacters()
        {
            AddActor("a", "Bale", "Male");
            WaitFor(2);
            //check for validation error
            var firstNameError = driver.FindElement(By.Id("firstNameErr"));
            Assert.AreEqual(expectedFirstNameOutOfBoundErrMsg, firstNameError.GetAttribute("textContent"));

        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void Test8_invalidBoundaryFirstNameWith51Characters()
        {

            AddActor(new string('a', 51), "Hanks", "Male");
            WaitFor(2);
            //check for validation error
            var firstNameError = driver.FindElement(By.Id("firstNameErr"));
            Assert.IsTrue(firstNameError.Displayed);
            Assert.AreEqual(expectedFirstNameOutOfBoundErrMsg, firstNameError.GetAttribute("textContent"));
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void Test9_validBoundaryLastNameWIthTwoCharacters()
        {
            AddActor("Ryan", "Re", "Male");
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
        public void Test_10_validBoundaryLastNameWIth50Characters()
        {

            AddActor("Ryan", new string('a', 50), "Male");
            var modelBtnAdd = wait.Until(e => e.FindElement(By.Id("modelBtnAdd"))); ;
            modelBtnAdd.Click();
            WaitFor(1);
            //Check for successful added message
            var alert = driver.SwitchTo().Alert();
            Assert.AreEqual(expectedAlertAddedMsg, alert.Text);
            alert.Accept();
            //Removing actor
            RemoveActor(3);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void Test_11_invalidBoundaryLastNameWIthOneCharacters()
        {

            AddActor("Denis", "a", "Female");            
            WaitFor(1);
            //check for validation error
            var lastNameError = driver.FindElement(By.Id("lastNameErr"));
            Assert.IsTrue(lastNameError.Displayed);
            Assert.AreEqual(expectedLastNameOutOfBoundErrMsg, lastNameError.GetAttribute("textContent"));
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void Test_12_invalidBoundaryLastNameWIth51Characters()
        {

            AddActor("Jessica", new string('A', 51), "Female");
            WaitFor(1);
            //check for validation error
            var lastNameError = driver.FindElement(By.Id("lastNameErr"));
            Assert.IsTrue(lastNameError.Displayed);
            Assert.AreEqual(expectedLastNameOutOfBoundErrMsg, lastNameError.GetAttribute("textContent"));
        }

        #region Test Helper methods  
        /*
         * This is a helper method that is used to create new actor
         * fName: actor first name
         * lName: actor last name
         * genderType: actor gender
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
            WaitFor(1);
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
