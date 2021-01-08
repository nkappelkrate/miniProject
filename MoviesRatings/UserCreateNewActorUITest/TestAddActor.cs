//Author: Hieu Nguyen

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace UserCreateNewActorUITest
{
    [TestClass]
    public class TestAddActor
    {
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContextInstance;
        private IWebDriver driver;
        private string appURL;
        private WebDriverWait wait;

        private const string firstNameEmptyErrMsg = "First Name is requried";
        private const string lastNameEmptyErrMsg = "Last Name is requried";
        private const string firstNameOutOfBoundErrMsg = "First Name must be between 2 and 50 characters long";
        private const string lastNameOutOfBoundErrMsg = "Last Name must be between 2 and 50 characters long";


        public TestAddActor() { }

        #region test methods
        [TestMethod]
        [TestCategory("Chrome")]
        public void TestValidAddNewActor()
        {
            //Add new Actor
            AddActor("Morgan", "Freeman");

            var modelBtnAdd = driver.FindElement(By.Id("modelBtnAdd"));
            modelBtnAdd.Click();
            //Check for successful added message

            //Not cleaning up Morgan Freeman in the database, will be using it for the next test
        }


        [TestMethod]
        [TestCategory("Chrome")]
        public void TestValidAddAlreadyExistedActor()
        {
           
            //re-adding actor
            AddActor("Morgan", "Freeman");
            var modelBtnAdd = driver.FindElement(By.Id("modelBtnAdd"));
            modelBtnAdd.Click();
            //checking for error message

            //clean up
            RemoveActor();
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void TestExceptionCreateActorWithoutFirstName()
        {
            AddActor("", "Reynolds");
            //check for validation error
            var firstNameError = driver.FindElement(By.Id("firstNameError"));
            Assert.Equals(firstNameError.GetAttribute("textContent"), firstNameEmptyErrMsg);

        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void TestExceptionCreateActorWithoutLastName()
        {
            AddActor("Ryan", "");
            //check for validation error
            var lastNameError = driver.FindElement(By.Id("lastNameError"));
            Assert.Equals(lastNameError.GetAttribute("textContent"), lastNameEmptyErrMsg);

        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void TestBoundaryFirstNameWithTwoCharacters()
        {
            
            AddActor("Ba", "Pitt");
            var modelBtnAdd = driver.FindElement(By.Id("modelBtnAdd"));
            modelBtnAdd.Click();
            //Check for successful added message
            //Check for the first row's value
            //Clean up
            RemoveActor();
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void TestBoundaryFirstNameWith50Characters()
        {
            //generate a line of repeated a 50 times            
            AddActor(new string('a', 50), "Damon");
            var modelBtnAdd = driver.FindElement(By.Id("modelBtnAdd"));
            modelBtnAdd.Click();
            //Check for successful added message
            //Check for the first row's value
            //Clean up
            RemoveActor();
        }
        [TestMethod]
        [TestCategory("Chrome")]
        public void TestBoundaryFirstNameWithOneCharacters()
        {
            AddActor("a", "Bale");
            //check for validation error
            var firstNameError = driver.FindElement(By.Id("FirstNameError"));
            Assert.IsTrue(firstNameError.Displayed);
            Assert.Equals(firstNameError.GetAttribute("textContent"), firstNameOutOfBoundErrMsg);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void TestBoundaryFirstNameWith51Characters()
        {
            
            AddActor(new string('a', 51), "Hanks");
            //check for validation error
            var firstNameError = driver.FindElement(By.Id("FirstNameError"));
            Assert.IsTrue(firstNameError.Displayed);
            Assert.Equals(firstNameError.GetAttribute("textContent"), firstNameOutOfBoundErrMsg);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void TestBoundaryLastNameWIthTwoCharacters()
        {
            AddActor("Ryan", "Re");
            //Check for successful message
            //chech the fisrt row of the table
            RemoveActor();
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void TestBoundaryLastNameWIth50Characters()
        {

            AddActor("Ryan", new string('a', 50));
            //Check for successful message
            //chech the fisrt row of the table
            RemoveActor();
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void TestBoundaryLastNameWIthOneCharacters()
        {

            AddActor("Denis", "a");
            
            //check for validation error
            var lastNameError = driver.FindElement(By.Id("lastNameError"));
            Assert.IsTrue(lastNameError.Displayed);
            Assert.Equals(lastNameError.GetAttribute("textContent"), lastNameOutOfBoundErrMsg);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void TestBoundaryLastNameWIth51Characters()
        {

            AddActor("Jessica", new string('A', 51));
            //Select femate
            var gender = driver.FindElement(By.Name("Gender"));
            var selectElement = new SelectElement(gender);

            selectElement.SelectByText("Female");

            //check for validation error
            var lastNameError = driver.FindElement(By.Id("lastNameError"));
            Assert.IsTrue(lastNameError.Displayed);
            Assert.Equals(lastNameError.GetAttribute("textContent"), lastNameOutOfBoundErrMsg);
        }

        #endregion

        #region Test Helper methods  
        /*
         * This is a helper method that is used to create new actor
         * fName: actor first name
         * lName: actor last name
         */
        private void AddActor(string fName, string lName)
        {
            //set url
            appURL = "";
            //navigate to the actor page
            driver.Navigate().GoToUrl(appURL);
            //wait for the Add Actor button to show up
            var btnAdd = wait.Until(e => e.FindElement(By.Id("btnAddActor")));
            btnAdd.Click();
            //wait for the model to loadup properly 
            wait.Until(e => e.FindElement(By.Id("")));

            //Enter new actor
            var firstName = driver.FindElement(By.Id("firstName"));
            var lastName = driver.FindElement(By.Id("lastName"));
            

            firstName.SendKeys(fName);
            lastName.SendKeys(lName);
            
        }

        /**
         * This is a helper method that is used to remove actor from the database
         */
        private void RemoveActor()
        {

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
        [TestInitialize]
        public void SetupTest()
        {
            appURL = "http://localhost:5000";
            string brower = "Chrome";

            switch (brower)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "Firefox":
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
        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }

    }
}
