using NUnit.Framework;

namespace FastGTD.UnitTests
{
    public class EventTestingFixture
    {
        private bool _event_was_raised;

        [SetUp]
        public void ClearRaisedFlag()
        {
            _event_was_raised = false;
        }

        protected void CatchEvent()
        {
            _event_was_raised = true;
        }

        protected bool EventWasRaised()
        {
            return _event_was_raised;
        }
    }
}