using System;
using System.Collections.Generic;
using NUnit.Framework;
using White.CustomCommands;
using White.CustomControls.Peers.Automation;

namespace White.NonCoreTests.CustomControls.Automation
{
    [TestFixture]
    public class CommandAssemblyTest
    {
        [Test]
        public void AddClassesMarkedWithDataContractAsKnownTypes()
        {
            var holder = new KnownTypeHolderStub();
            new CommandAssembly(typeof (Thickness).Assembly, holder);
            Assert.AreEqual(true, holder.KnownTypes.Contains(typeof(Thickness)));
        }
    }

    public class KnownTypeHolderStub : IKnownTypeHolder
    {
        private readonly List<Type> knownTypes = new List<Type>();

        public void Add(Type type)
        {
            knownTypes.Add(type);
        }

        public List<Type> KnownTypes
        {
            get { return knownTypes; }
        }
    }
}