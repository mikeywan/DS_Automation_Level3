using AgeRangerWebUi.Steps;
using TechTalk.SpecFlow;

namespace AgeRangerWebUi.Utilities
{
    [Binding]
    public class Hooks : BaseClass
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Driver.Quit();
        }

        [AfterTestRun]
        public static void AfterWebFeature()
        {
            Driver.Dispose();
        }
    }
}
