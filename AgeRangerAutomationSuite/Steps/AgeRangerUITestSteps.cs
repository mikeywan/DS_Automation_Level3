using AgeRangerWebUi.PageFactoryObjects;
using AgeRangerWebUi.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace AgeRangerWebUi.Steps
{
    [Binding]
    public class AgeRangerUITestSteps : BaseClass
    {
        [Given(@"I am on Age Ranger Home Page")]
        public void GoToHomePage()
        {
            Driver = DriverFactory.InitiateWebDriver(CommonConstants.DriverSettings.ChromeBrowser);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Navigate().GoToUrl(CommonConstants.ApplicationSettings.BaseUrl + ".net");
            Sleep(5);
        }

        [When(@"I click Add New Person button")]
        public void AddNewPerson()
        {
            var pageObject = new AgeRangerMainPage(Driver);
            if (ElementIsPresent(pageObject.AddPerson))
            {
                pageObject.AddPerson.Click();
            }
        }

        [When(@"I Submit the form")]
        public void SubmitForm()
        {
            var pageObject = new AgeRangerMainPage(Driver);
            if (ElementIsPresent(pageObject.SubmitButton))
            {
                pageObject.SubmitButton.Click();
            }
        }

        [When(@"I confirm the action")]
        public void ConfirmAction()
        {
            var pageObject = new AgeRangerMainPage(Driver);
            if (ElementIsPresent(pageObject.OkButton))
            {
                pageObject.OkButton.Click();
            }
            Sleep(5);
        }

        [When(@"I enter (.*), (.*) and (.*) in the form")]
        public void EnterNewPersonDetails(String firstName, String lastName, String age)
        {
            var pageObject = new AgeRangerMainPage(Driver);
            if (!firstName.Equals("NoChange"))
            {
                if (ElementIsPresent(pageObject.FirstName))
                {
                    pageObject.FirstName.Clear();
                    pageObject.FirstName.SendKeys(firstName);
                }               
            }
            if (!lastName.Equals("NoChange"))
            {
                if (ElementIsPresent(pageObject.LastName))
                {
                    pageObject.LastName.Clear();
                    pageObject.LastName.SendKeys(lastName);
                }
            }
            if (!age.Equals("NoChange"))
            {
                if (ElementIsPresent(pageObject.Age))
                {
                    pageObject.Age.Clear();
                    pageObject.Age.SendKeys(age.ToString());
                }
            }
        }

        [When(@"I delete created person (.*), (.*) and (.*)")]
        [Then(@"I delete created person (.*), (.*) and (.*)")]
        public void DeletePerson(String firstName, String lastName, String age)
        {
            // Search using First Name
            SearchUsingFirstName(firstName);

            var pageObject = new AgeRangerMainPage(Driver);
            string firstLastName = (firstName + ' ' + lastName);
            bool userFound = false;
            IList<IWebElement> tableRow = pageObject.PeopleTable.FindElements(By.TagName("tr"));
            foreach (IWebElement row in tableRow)
            {
                if (row.Text.Contains(firstLastName) && row.Text.Contains(age.ToString()))
                {
                    if (ElementIsPresent(pageObject.PeopleTable.FindElement(By.XPath("//td[contains(text(), '" + firstLastName + "')]/..//a[@ng-click='delete(person)']"))))
                    {
                        pageObject.PeopleTable.FindElement(By.XPath("//td[contains(text(), '" + firstLastName + "')]/..//a[@ng-click='delete(person)']")).Click();
                        if (ElementIsPresent(pageObject.DeleteConfirmButton))
                        {
                            pageObject.DeleteConfirmButton.Click();
                            Sleep(5);
                            userFound = true;
                            break;
                        }
                    }
                }
            }
            Assert.True(userFound, "User not Found.");
        }

        [Then(@"I delete updated person (.*), (.*), (.*), (.*), (.*) and (.*)")]
        public void DeleteUpdatedPerson(String firstName, String lastName, String age, String newFirstName, String newLastName, String newAge)
        {
            // Search using First Name
            SearchUsingFirstName(firstName);

            if (!newFirstName.Equals("NoChange"))
            {
                firstName = newFirstName;
            }

            if (!newLastName.Equals("NoChange"))
            {
                lastName = newLastName;
            }

            if (!newAge.Equals("NoChange"))
            {
                age = newAge;
            }

            DeletePerson(firstName, lastName, age);
        }

        [Given(@"I created a new person with (.*), (.*) and (.*)")]
        public void CreateNewPerson(String firstName, String lastName, String age)
        {
            GoToHomePage();
            AddNewPerson();
            EnterNewPersonDetails(firstName, lastName, age);
            SubmitForm();
            ConfirmAction();
        }

        public void SearchUsingFirstName(String firstName)
        {
            var pageObject = new AgeRangerMainPage(Driver);
            if (ElementIsPresent(pageObject.SearchTextField))
            {
                pageObject.SearchTextField.SendKeys(firstName);
                pageObject.SearchTextField.Clear();
                pageObject.SearchTextField.SendKeys(Keys.Enter);
            }
        }

        [When(@"I update the (.*), (.*) and (.*) with (.*), (.*) and (.*)")]
        public void UpdatePerson(String oldFirstName, String oldLastName, String oldAge, String firstName, String lastName, String age)
        {
            //Search using First Name
            SearchUsingFirstName(oldFirstName);

            var pageObject = new AgeRangerMainPage(Driver);
            IList<IWebElement> tableRows = pageObject.PeopleTable.FindElements(By.TagName("tr"));
            string firstLastName = (oldFirstName + " " + oldLastName);
            foreach (IWebElement row in tableRows)
            {
                if (row.Text.Contains(firstLastName) && row.Text.Contains(oldAge.ToString()))
                {
                    if (ElementIsPresent(pageObject.PeopleTable.FindElement(By.XPath("//td[contains(text(), '" + firstLastName + "')]/..//a[@ng-click='openEditForm(person)']"))))
                    {
                        pageObject.PeopleTable.FindElement(By.XPath("//td[contains(text(), '" + firstLastName + "')]/..//a[@ng-click='openEditForm(person)']")).Click();
                        EnterNewPersonDetails(firstName, lastName, age);
                        break;
                    }
                }
            }
        }

        [Then(@"I should see (.*) and (.*) in the People view with correct (.*) instead of (.*), (.*) and (.*)")]
        public void UserExistVerification(String newFirstName, String newLastName, String newAge, String oldFirstName, String oldLastName, String oldAge)
        {
            if (newFirstName.Equals("NoChange"))
            {
                newFirstName = oldFirstName;
            }
            if (newLastName.Equals("NoChange"))
            {
                newLastName = oldLastName;
            }
            if (newAge.Equals("NoChange"))
            {
                newAge = oldAge;
            }

            // Search using First Name
            SearchUsingFirstName(newFirstName);

            var pageObject = new AgeRangerMainPage(Driver);
            IList<IWebElement> tableRows = pageObject.PeopleTable.FindElements(By.TagName("tr"));
            string firstLastName = (newFirstName + " " + newLastName);

            bool userFound = false;

            foreach (IWebElement row in tableRows)
            {
                if (row.Text.Contains(firstLastName) && row.Text.Contains(newAge.ToString()))
                {
                    userFound = true;
                    break;
                }
            }

            Assert.True(userFound, "User not exists.");
        }

        [Then(@"I should see (.*) and (.*) in the People view with correct (.*) and the correct (.*)")]
        public void VerifyPerson(String firstName, String lastName, String age, String ageGroup)
        {
            // Search using First Name
            SearchUsingFirstName(firstName);

            var pageObject = new AgeRangerMainPage(Driver);
            string firstLastName = (firstName + " " + lastName);

            bool userFound = false;
            IList<IWebElement> tableRow = pageObject.PeopleTable.FindElements(By.TagName("tr"));
            foreach (IWebElement row in tableRow)
            {
                if (row.Text.Contains(firstLastName) && row.Text.Contains(age.ToString()) && row.Text.Contains(ageGroup.ToString()))
                {
                    userFound = true;
                    break;
                }
            }
            Assert.True(userFound, "User not exists.");
        }

        [Then(@"I should not see (.*), (.*) and (.*) record anymore")]
        public void UserNotExistVerification(String firstName, String lastName, String age)
        {
            // Search using First Name
            SearchUsingFirstName(firstName);

            var pageObject = new AgeRangerMainPage(Driver);
            IList<IWebElement> tableRows = pageObject.PeopleTable.FindElements(By.TagName("tr"));
            string firstLastName = (firstName + " " + lastName);
            bool userFound = false;

            foreach (IWebElement row in tableRows)
            {
                if (row.Text.Contains(firstLastName) && row.Text.Contains(age.ToString()))
                {
                    userFound = true;
                    break;
                }
            }

            Assert.False(userFound, "User exists.");
        }

        ~AgeRangerUITestSteps()
        {
        }
    }
}