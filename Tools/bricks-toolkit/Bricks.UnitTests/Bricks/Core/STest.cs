using System;
using System.Collections;
using System.Globalization;
using NUnit.Framework;

namespace Bricks.Core
{
    [TestFixture]
    public class STest
    {
        [Test]
        public void LastLine()
        {
            Assert.AreEqual("efg", S.LastLine("abc\refg"));
            Assert.AreEqual("efg", S.LastLine("abc\r\nefg"));
            Assert.AreEqual("efg", S.LastLine("abc\refg\r"));
            Assert.AreEqual("efg", S.LastLine("abc\refg\r\n\r\n"));
        }

        [Test]
        public void NumberOfLines()
        {
            Assert.AreEqual(2, S.NumberOfLines("abc\refg"));
            Assert.AreEqual(3, S.NumberOfLines("abc\refg\rijk"));
            Assert.AreEqual(2, S.NumberOfLines("abc\r\nefg"));
            Assert.AreEqual(3, S.NumberOfLines("abc\r\nefg\r\nijk"));
        }

        [Test]
        public void LastLines()
        {
            Assert.AreEqual("efg", S.LastLines("abc\refg", 1));
            Assert.AreEqual("efg\nijk", S.LastLines("abc\refg\rijk", 2));
            Assert.AreEqual("efg", S.LastLines("abc\r\nefg", 1));
            Assert.AreEqual("efg\nijk", S.LastLines("abc\r\nefg\r\nijk", 2));
        }

        [Test]
        public void EqualsIgnoreCase()
        {
            Assert.AreEqual(true, S.EqualsIgnoreCase("a", "A"));
            Assert.AreEqual(true, S.EqualsIgnoreCase("a", "a"));
            Assert.AreEqual(false, S.EqualsIgnoreCase("a", "b"));
        }

        [Test]
        public void ToBool()
        {
            Assert.AreEqual(false, S.ToBool("false", true));
            Assert.AreEqual(false, S.ToBool("False", true));
            Assert.AreEqual(true, S.ToBool("True", false));
            Assert.AreEqual(true, S.ToBool("True", false));
            Assert.AreEqual(false, S.ToBool("fdsf", false));
            Assert.AreEqual(true, S.ToBool("fdsf", true));
        }

        [Test]
        public void LastParts()
        {
            string[] words = S.LastWords("Name Row 0", 2);
            Assert.AreEqual("Row", words[0]);
            Assert.AreEqual("0", words[1]);

            words = S.LastWords("Name Row 0", 1);
            Assert.AreEqual("0", words[0]);
        }

        [Test]
        public void ShouldCapitalizeFirstCharacterInEveryWord()
        {
            Assert.IsEmpty(S.CapitalizeFirst(string.Empty));
            Assert.IsNull(S.CapitalizeFirst(null));
            Assert.AreEqual("Medha", S.CapitalizeFirst("medha"));
        }

        [Test]
        public void List()
        {
            Assert.AreEqual(string.Empty, S.List());
            Assert.AreEqual("blam, kaboom", S.List("blam", "kaboom"));
            Assert.AreEqual("foo", S.List(new string[] {"foo"}));
            Assert.AreEqual("bar, goo", S.List(new string[] {"bar", "goo"}));
            Assert.AreEqual("bar,  , goo", S.List(true, new string[] {"bar", " ", "goo"}));
            Assert.AreEqual("bar, goo", S.List(false, new string[] {"bar", " ", "goo"}));
        }

        [Test]
        public void ListWithNull()
        {
            Assert.AreEqual("foo, bar", S.List(false, new object[] {"foo", null, "bar"}));
            Assert.AreEqual("foo, , bar", S.List(true, new object[] {"foo", null, "bar"}));
        }

        [Test]
        public void ListWithAllNull()
        {
            Assert.AreEqual("", S.List(false, new object[] {"", null, null}));
        }

        [Test]
        public void AreDifferent()
        {
            Assert.IsFalse(S.AreDifferent(null, null));
            Assert.IsTrue(S.AreDifferent(null, "foo"));
            Assert.IsTrue(S.AreDifferent("foo", null));
            Assert.IsTrue(S.AreDifferent("foo", "bar"));
        }

        [Test]
        public void AreEqual()
        {
            Assert.IsTrue(S.AreEqual("a", "a"));
            Assert.IsTrue(S.AreEqual("a", "a "));
            Assert.IsTrue(S.AreEqual("a ", "a"));
            Assert.IsFalse(S.AreEqual("ab", "abc"));

            Assert.IsTrue(S.AreEqual(null, null));
            Assert.IsFalse(S.AreEqual("a", null));
            Assert.IsFalse(S.AreEqual(null, "a"));
        }

        [Test]
        public void RemovesSpaces()
        {
            Assert.AreEqual("WC2B4PH", S.RemoveSpaces("  WC2B 4PH "));
        }

        [Test]
        public void TrimCharacter()
        {
            Assert.AreEqual("WC2B4PH", S.TrimCharacter(",WC2B4PH", ','));
            Assert.AreEqual("WC2B4PH", S.TrimCharacter(" ,WC2B4PH", ','));
        }

        [Test]
        public void FirstLine()
        {
            Assert.AreEqual("aa", S.FirstLine("aa"));
            Assert.AreEqual("aa", S.FirstLine("aa" + Environment.NewLine + "bb"));
            Assert.AreEqual("", S.FirstLine(""));
            Assert.AreEqual("aaa", S.FirstLine("aaa" + Environment.NewLine + "bbb" + Environment.NewLine + "ccc"));
        }

        [Test]
        public void FirstLineWorksWithSlashNs()
        {
            Assert.AreEqual("aa", S.FirstLine("aa\nbb"));
        }

        [Test]
        public void RichTextify()
        {
            Assert.AreEqual("line1\\line line2\\line line3", S.RichTextify("line1" + Environment.NewLine + "line2" + S.OTHER_NEW_LINE + "line3"));
        }

        [Test]
        public void ShouldReturnEmtpyWhenTheValueItSelfReturnsNull()
        {
            Assert.AreEqual("", S.Value(""));
        }

        [Test]
        public void ShouldGetAllSubstringsStartingWithSpecifiedCharactersFromAString()
        {
            string str = "<abc> def <pqr> vvv nnn";
            ArrayList requiredSubstrings = S.GetSubstringsStartingAndEndingWith(str, "<", ">", ' ');
            Assert.AreEqual(2, requiredSubstrings.Count);
            Assert.AreEqual("<abc>", requiredSubstrings[0]);
            Assert.AreEqual("<pqr>", requiredSubstrings[1]);
        }

        [Test]
        public void ShouldAddAmpersand()
        {
            Assert.AreEqual("&foo", S.AddMenuShortcut("foo"));
        }

        [Test]
        public void ShouldNotAddAmpersandIfOneAlreadyExists()
        {
            Assert.AreEqual("f&oo", S.AddMenuShortcut("f&oo"));
        }

        [Test]
        public void ShouldNotModifyIfEmptyOrNull()
        {
            Assert.AreEqual("", S.AddMenuShortcut(""));
            Assert.IsNull(S.AddMenuShortcut(null));
        }

        [Test]
        public void UnCamel()
        {
            Assert.AreEqual("Payment Info", S.UnCamel("PaymentInfo"));
            Assert.AreEqual("Payment BACS", S.UnCamel("PaymentBACS"));
            Assert.AreEqual("Policy Add On", S.UnCamel("PolicyAddOn"));
        }

        [Test]
        public void Decimalises()
        {
            Assert.AreEqual(10000, S.Decimalise("$ 10000"));
            Assert.AreEqual(20000, S.Decimalise("Rs.20000"));
            Assert.AreEqual(30000, S.Decimalise("£30000"));
            Assert.AreEqual(40000, S.Decimalise("£4s0s0s0s0ffsf"));
            Assert.AreEqual(40.234, S.Decimalise("£40.234"));
            Assert.AreEqual(40.234, S.Decimalise("Rs.40.234"));
        }

        [Test]
        public void ShouldCheckForEqualsIgnoringCase()
        {
            Assert.AreEqual(true, S.AreEqualIgnoreCase("Ab", "ab"));
            Assert.AreEqual(true, S.AreEqualIgnoreCase("AB", "ab"));
            Assert.AreEqual(true, S.AreEqualIgnoreCase(" AB", " ab "));
            Assert.AreEqual(false, S.AreEqualIgnoreCase("AB", "ABc"));
        }

        [Test]
        public void ShouldReplace()
        {
            Assert.AreEqual("<?xml version=\"1.0\" ?>", S.Replace("<?xml version=\"1.0\" encoding=\"utf-8\" ?>", "encoding=\"utf-8\" ", string.Empty));
        }

        [Test]
        public void ContainsIgnoreCase()
        {
            Assert.AreEqual(true, S.ContainsIgnoreCase("abc", "b"));
            Assert.AreEqual(true, S.ContainsIgnoreCase("abc", "B"));
            Assert.AreEqual(false, S.ContainsIgnoreCase("abc", "d"));
            Assert.AreEqual(false, S.ContainsIgnoreCase("abc", "D"));
        }

        [Test]
        public void ReplaceLast()
        {
            Assert.AreEqual("ap", S.ReplaceLast("ap.exe", ".exe", string.Empty));
            Assert.AreEqual("ap.exe", S.ReplaceLast("ap.exe", "pp", string.Empty));
        }

        [Test]
        public void SplitCamelCased()
        {
            string[] parts = S.Split("CamelCase");
            Assert.AreEqual(2, parts.Length);
            Assert.AreEqual("Camel", parts[0]);
            Assert.AreEqual("Case", parts[1]);
        }
    }
}