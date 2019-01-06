﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace AgeRangerWebUi.PageFactoryObjects
{
    public class AgeRangerMainPage

    {
        public AgeRangerMainPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.Id, Using = "searchText")]
        public IWebElement SearchTextField;

        [FindsBy(How = How.Name, Using = "FirstName")]
        public IWebElement FirstName;

        [FindsBy(How = How.Name, Using = "LastName")]
        public IWebElement LastName;

        [FindsBy(How = How.Name, Using = "Age")]
        public IWebElement Age;

        [FindsBy(How = How.XPath, Using = "//table")]
        public IWebElement PeopleTable { get; set; }

        [FindsBy(How = How.XPath, Using = "//td[@class='col-md-7 ng-binding']")]
        public IList<IWebElement> ExistingUserName { get; set; }

        [FindsBy(How = How.XPath, Using = "//td[@class='col-md-2 ng-binding']")]
        public IList<IWebElement> ExistingUserAge { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@ng-click='openEditForm(person)']")]
        public IWebElement EditPerson { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@ng-click='delete(person)']")]
        public IWebElement DeletePerson { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@ng-click='openNewPersonForm()']")]
        public IWebElement AddPerson { get; set; }

        [FindsBy(How = How.XPath, Using = "//p[@class='help-block']")]
        public IWebElement FirstNameInlineError { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@ng-click='submit()']")]
        public IWebElement SubmitButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@ng-click='close()']")]
        public IWebElement CloseButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@data-bb-handler='confirm']")]
        public IWebElement OkButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@data-bb-handler='cancel']")]
        public IWebElement CancelButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@data-bb-handler='cancel']")]
        public IWebElement DefaultButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@data-bb-handler='confirm']")]
        public IWebElement DeleteConfirmButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@class='bootbox-close-button close']")]
        public IWebElement CrossButton { get; set; }
    }
}
