using System;
using TechTalk.SpecFlow;

namespace MysiePysieService.Tests.Steps
{
    [Binding]
    public class SubjectControlSteps
    {
        [Given(@"I'm on subjects page")]
        public void GivenIMOnSubjectsPage()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have a list of all subjects")]
        public void GivenIHaveAListOfAllSubjects()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I click the '(.*)' button")]
        public void WhenIClickTheButton(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I click on updatge subject with id")]
        public void WhenIClickOnUpdatgeSubjectWithId()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I click on add a new subject")]
        public void WhenIClickOnAddANewSubject()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I click on find a subject by id")]
        public void WhenIClickOnFindASubjectById()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I click on delete subject")]
        public void WhenIClickOnDeleteSubject()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"a list of subjects is returned in a table on page:")]
        public void ThenAListOfSubjectsIsReturnedInATableOnPage(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"a subject with the given id is updated")]
        public void ThenASubjectWithTheGivenIdIsUpdated()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"A new subject matching the data is created")]
        public void ThenANewSubjectMatchingTheDataIsCreated()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"a subject with given id is returned")]
        public void ThenASubjectWithGivenIdIsReturned()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"subject is removed from the database")]
        public void ThenSubjectIsRemovedFromTheDatabase()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
