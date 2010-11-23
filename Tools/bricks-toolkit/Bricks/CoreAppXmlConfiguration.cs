using System.Collections.Generic;
using Bricks.Core;

namespace Bricks
{
    public class CoreAppXmlConfiguration : AssemblyConfiguration, CoreConfirguration
    {
        private static CoreConfirguration singleton;
        private static readonly Dictionary<string, object> defaultValues = new Dictionary<string, object>();

        private static readonly string clockRecheckDuration = CodePath.Get(CodePath.New<CoreConfirguration>().RecheckDurationInMilliseconds);

        static CoreAppXmlConfiguration()
        {
            defaultValues.Add(clockRecheckDuration, 100.ToString());
        }

        private CoreAppXmlConfiguration(string sectionGroup, string section) : base(sectionGroup, section, defaultValues, null) {}

        public static CoreConfirguration Instance
        {
            get
            {
                if (singleton == null) singleton = new CoreAppXmlConfiguration("Bricks", "Bricks");
                return singleton;
            }
        }

        public virtual int RecheckDurationInMilliseconds
        {
            get { return S.ToInt(usedValues[clockRecheckDuration], -1); }
        }
    }
}