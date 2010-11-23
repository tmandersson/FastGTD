using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Bricks.Core
{
    public class S
    {
        public const string SELECT_ONE = "<Select One>";
        public const string NONE = "<None>";
        public const string PICK_ONE = "<Please pick one>";
        public const string UNKNOWN = "";
        public const string POUND_SIGN = "\u00A3";
        public const string OTHER_NEW_LINE = "\n";
        public const string SERVER_PRINTER = "Default";
        public const string SPACE = " ";

        public static bool IsEmpty(string s)
        {
            return string.IsNullOrEmpty(s) || s.Trim() == string.Empty;
        }

        public static string EmptyIfDBNull(object o)
        {
            if (o == null) return string.Empty;
            return o.ToString();
        }

        public static bool IsDBEmpty(object o)
        {
            return o == null || o.Equals(DBNull.Value) || IsEmpty((string) o);
        }

        public static bool ToBool(string s, bool @default)
        {
            if (IsEmpty(s)) return @default;
            bool result;
            if (!bool.TryParse(s, out result)) return @default;
            return result;
        }

        public static bool ToBool(string s)
        {
            return bool.Parse(s);
        }

        public static string LowerCase(object o)
        {
            return Value(o).ToLowerInvariant();
        }

        public static bool IsNotEmpty(string s)
        {
            return !IsEmpty(s);
        }

        public static bool AreAllEmpty(params string[] strings)
        {
            foreach (string s in strings)
                if (IsNotEmpty(s)) return false;
            return true;
        }

        public static string TrimSafe(string value)
        {
            return Value(value).Trim();
        }

        public static bool IsNumber(string c)
        {
            int result;
            return int.TryParse(c, out result);
        }

        public static bool IsNotANumber(string c)
        {
            return !IsNumber(c);
        }

        public static string CapitalizeFirst(string word)
        {
            if (IsNotEmpty(word)) return word[0].ToString().ToUpper() + word.Substring(1);
            return word;
        }

        public static string CapitalizeAll(string word)
        {
            if (IsNotEmpty(word)) return word.ToUpper();
            return word;
        }

        public static bool IsExactMatch(string originalString, string regex)
        {
            Match match = Regex.Match(originalString, regex);
            string matchedString = match.Success ? match.Result("$1") : null;
            return originalString.Equals(matchedString);
        }

        public static string List(ICollection items)
        {
            return List(false, items);
        }

        public static string List(bool includeEmpty, ICollection items)
        {
            return List(", ", includeEmpty, items);
        }

        public static string List(string delimiter, bool includeEmpty, ICollection items)
        {
            if (items.Count == 0) return string.Empty;
            StringBuilder b = new StringBuilder();
            foreach (object item in items)
            {
                string s = item == null ? string.Empty : item.ToString();
                if (s.Trim().Length > 0 || includeEmpty) b.Append(s).Append(delimiter);
            }
            return b.ToString().TrimEnd(' ', ',', '\r', '\n');
        }

        public static string Get(string s, string @default)
        {
            return IsEmpty(s) ? @default : s;
        }

        public static string List(params object[] items)
        {
            return List((ICollection) items);
        }

        public static string List(bool includeEmpty, params object[] items)
        {
            return List(includeEmpty, (ICollection) items);
        }

        public static bool AreDifferent(object first, object second)
        {
            if (first == null && second == null)
                return false;
            if (first != null)
                return !first.Equals(second);
            return true;
        }

        public static string CorrectLabelText(string text)
        {
            if (string.Empty.Equals(text) || text.IndexOf('?') != -1) return text;
            text = text.Replace(':', ' ');
            text = text.Trim();
            return text + " :";
        }

        public static bool ValidateEmail(object validationKey, string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9\.\-_]+@([a-zA-Z0-9\-_]+\.)+[a-zA-Z]+$");
        }

        public static bool IsUnknown(string s)
        {
            return s.Equals(UNKNOWN) || s.Equals(SELECT_ONE);
        }

        public static bool AreEqual(string s1, string s2)
        {
            if (IsEmpty(s1) && IsEmpty(s2)) return true;
            if (IsEmpty(s1) || IsEmpty(s2)) return false;
            return s1.Trim().Equals(s2.Trim());
        }

        public static bool AreEqualIgnoreCase(string s1, string s2)
        {
            s1 = s1.ToLower();
            s2 = s2.ToLower();
            return AreEqual(s1, s2);
        }

        public static string Value(object value)
        {
            return (value == null || value.ToString() == null) ? string.Empty : value.ToString();
        }

        public static string RemoveSpaces(string value)
        {
            return value.Replace(SPACE, "");
        }

        public static string NotNullValue(string value)
        {
            if (IsEmpty(value)) return string.Empty;
            return value;
        }

        public static string TrimCharacter(string name, char toRemove)
        {
            return name.TrimStart(toRemove, ' ').TrimEnd(toRemove, ' ');
        }

        public static string AsPound(string s)
        {
            return POUND_SIGN + s;
        }

        public static string AsPound(Money money)
        {
            return AsPound(money.Amount);
        }

        public static string AsPound(decimal d)
        {
            return AsPound(d.ToString());
        }

        public static string FirstLine(string s)
        {
            return FirstLineConversion(FirstLineConversion(s, Environment.NewLine), OTHER_NEW_LINE);
        }

        private static string FirstLineConversion(string s, string newLine)
        {
            s = NotNullValue(s);
            if (!s.Contains(newLine)) return s;
            return s.Substring(0, s.IndexOf(newLine));
        }

        public static string RichTextify(string s)
        {
            s = NotNullValue(s);
            s = s.Replace(Environment.NewLine, "\\line ");
            s = s.Replace(OTHER_NEW_LINE, "\\line ");
            return s;
        }

        public static string CDATA(string s)
        {
            return "<![CDATA[" + s + "]]>";
        }

        public static string RemoveWhitespaces(string expected)
        {
            return expected.Replace(SPACE, "").Replace("\n", "").Replace("\t", "").Replace("\r", "");
        }

        public static string Join(string delimiter, params string[] strings)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < strings.Length; i++)
            {
                if (i > 0) builder.Append(delimiter);
                builder.Append(strings[i]);
            }
            return builder.ToString();
        }

        public static ArrayList GetSubstringsStartingAndEndingWith(string str, string startChar, string endChar, char separator)
        {
            string[] substrings = str.Split(separator);
            ArrayList requiredSubstrings = new ArrayList();
            for (int i = 0; i < substrings.Length; i++)
            {
                if (substrings[i].StartsWith(startChar) && substrings[i].EndsWith(endChar))
                    requiredSubstrings.Add(substrings[i]);
            }
            return requiredSubstrings;
        }

        public static string AddMenuShortcut(string title)
        {
            return IsEmpty(title) || title.Contains("&") ? title : "&" + title;
        }

        public static string YNOnCount(int collectionCount)
        {
            return BoolToYn(collectionCount > 0);
        }

        public static string BoolToYn(bool b)
        {
            return b ? "Y" : "N";
        }

        public static string UnCamel(string name)
        {
            string uncamelled = "";
            for (int i = 0; i < name.Length; i++)
            {
                if (Eligible(i, name))
                    uncamelled += SPACE;

                uncamelled += name[i];
            }
            return uncamelled;
        }

        private static bool Eligible(int i, string name)
        {
            return char.IsUpper(name[i]) && i > 0 && i < name.Length - 1 && (char.IsLower(name[i + 1]) || char.IsLower(name[i - 1]));
        }

        public static decimal Decimalise(string moneyWithCurrencySymbol)
        {
            StringBuilder money = new StringBuilder();
            moneyWithCurrencySymbol = TrimNonNumbersFromStart(moneyWithCurrencySymbol);
            foreach (char c in moneyWithCurrencySymbol)
                if (isPartOfADecimal(c)) money.Append(c);
            return decimal.Parse(money.ToString());
        }

        private static bool isPartOfADecimal(char c)
        {
            return Char.IsDigit(c) || c.Equals('.');
        }

        private static string TrimNonNumbersFromStart(string moneyWithCurrencySymbol)
        {
            return Regex.Replace(moneyWithCurrencySymbol, "^[^0-9]*", string.Empty);
        }

        public static string ListNewLine(bool b, params object[] strings)
        {
            return List(" ," + Environment.NewLine, false, strings);
        }

        public static string Replace(string sourceString, string whattoReplce, string withNewReplacedment)
        {
            return sourceString.Replace(whattoReplce, withNewReplacedment);
        }

        public static bool ContainsIgnoreCase(string @in, string what)
        {
            return @in.ToLower().Contains(what.ToLower());
        }

        public static void AssertNotNull(string itemText, string errorMessage)
        {
            if (itemText == null) throw new ArgumentException(errorMessage);
        }

        public static int ToInt(string value, int defaultValue)
        {
            int result;
            if (!int.TryParse(value, out result)) return defaultValue;
            return result;
        }

        public static int ToInt(string value)
        {
            return int.Parse(value);
        }

        public static string[] LastWords(string of, int count)
        {
            string[] words = new string[count];

            string[] strings = of.Split(' ');
            int j = 0;
            for (int i = strings.Length - count; i < strings.Length; i++, j++)
                words[j] = strings[i];
            return words;
        }

        public static bool EqualsIgnoreCase(string first, string second)
        {
            return first.ToLower().Equals(second.ToLower());
        }

        public static string LastLine(string s)
        {
            return LastLines(s, 1);
        }

        public static string LastLines(string s, int numberOfLines)
        {
            int totalNumberOfLines = NumberOfLines(s);
            using (StringReader stringReader = new StringReader(s))
            {
                for (int i = 0; i < totalNumberOfLines - numberOfLines; i++)
                    stringReader.ReadLine();
                StringBuilder stringBuilder = new StringBuilder();
                while (true)
                {
                    string line = stringReader.ReadLine();
                    if (IsEmpty(line)) return stringBuilder.ToString().TrimEnd('\n');
                    stringBuilder.Append(line).Append('\n');
                }
            }
        }

        public static int NumberOfLines(string s)
        {
            int numberOfLines = 0;
            using (StringReader stringReader = new StringReader(s))
                while (IsNotEmpty(stringReader.ReadLine())) numberOfLines++;
            return numberOfLines;
        }

        public static string WrapInsideDoubleQuotes(string wrapString)
        {
            return string.Format("\"{0}\"", wrapString);
        }

        public static string ToString(object @object)
        {
            return @object == null ? "null" : @object.ToString();
        }

        public static string ReplaceLast(string replaceIn, string replace, string with)
        {
            int index = replaceIn.LastIndexOf(replace);
            if (index == -1) return replaceIn;
            return replaceIn.Substring(0, index) + with + replaceIn.Substring(index + replace.Length);
        }

        public static string[] Split(string @string)
        {
            List<string> words = new List<string>();
            char[] chars = @string.ToCharArray();
            string currentWord = string.Empty;
            foreach (char c in chars)
            {
                if (c.ToString().Equals(c.ToString().ToUpper()) && currentWord.Length > 0)
                {
                    words.Add(currentWord);
                    currentWord = string.Empty;
                }
                currentWord += c;
            }
            words.Add(currentWord);
            return words.ToArray();
        }
    }
}