using System;
using TechTalk.SpecFlow;

namespace MysiePysieService.Tests.Steps
{
    [Binding]
    public class StudentControlSteps
    {
        [Given(@"I'm on a students page")]
        public void GivenIMOnAStudentsPage()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I'm logged in with ""(.*)"" role")]
        public void GivenIMLoggedInWithRole(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I'm logged in without ""(.*)"" role")]
        public void GivenIMLoggedInWithoutRole(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I fill in students (.*), (.*), (.*) and (.*)")]
        public void WhenIFillInStudentsAnd(string p0, string p1, string p2, string p3)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I click on add button")]
        public void WhenIClickOnAddButton()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I fill in Students (.*)")]
        public void WhenIFillInStudents(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I fill in new (.*) which i wished to change")]
        public void WhenIFillInNewWhichIWishedToChange(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"Click on update student")]
        public void WhenClickOnUpdateStudent()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"Click on get a single student")]
        public void WhenClickOnGetASingleStudent()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I fill in student (.*)")]
        public void WhenIFillInStudent(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"click on delete student")]
        public void WhenClickOnDeleteStudent()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"The student should be added to the database")]
        public void ThenTheStudentShouldBeAddedToTheDatabase()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"All data should match the input")]
        public void ThenAllDataShouldMatchTheInput()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the student shouldn't be added if he already exists")]
        public void ThenTheStudentShouldnTBeAddedIfHeAlreadyExists()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"An error should be returned")]
        public void ThenAnErrorShouldBeReturned()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the student shouldn't be added to the database")]
        public void ThenTheStudentShouldnTBeAddedToTheDatabase()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"A list of all students should appears on page:")]
        public void ThenAListOfAllStudentsShouldAppearsOnPage()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Student is updated")]
        public void ThenStudentIsUpdated()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"New age should match input age")]
        public void ThenNewAgeShouldMatchInputAge()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Student shouldn't be updated")]
        public void ThenStudentShouldnTBeUpdated()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"A single student object matching the id should be returned")]
        public void ThenASingleStudentObjectMatchingTheIdShouldBeReturned()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Student no longer exists in database")]
        public void ThenStudentNoLongerExistsInDatabase()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Student shouldn't be deleted and still exist in database")]
        public void ThenStudentShouldnTBeDeletedAndStillExistInDatabase()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
