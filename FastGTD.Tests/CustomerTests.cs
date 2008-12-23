using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void InBoxResultIsSaved()
        {
            // TODO: Move tests into main assembly again? No benefit in having it separate?
            // TODO: Model and Form should not be responsible for saving etc. Refactor test and code.

            string ITEM = Guid.NewGuid().ToString();

            FastGTDApp app = StartNewApplication();
            app.InModel.ClearItems();
            Assert.That(app.InForm.InBoxItems.Count, Is.EqualTo(0));
            app.InModel.AddItem(ITEM);
            app.Close();

            FastGTDApp app2 = StartNewApplication();
            Assert.That(app2.InModel.Items, Has.Count(1));
            Assert.That(app2.InModel.Items, Has.Member(ITEM));
            app2.Close();
        }

        private static FastGTDApp StartNewApplication()
        {
            var app = new FastGTDApp();
            app.Start();
            return app;
        }
    }
}