using System;
using System.Collections.Generic;

namespace Bricks.RuntimeFramework
{
    [Serializable]
    public class ComparisonStatuses : List<ComparisonStatus>
    {
        public Tristate State
        {
            get
            {
                ComparisonStatus finalStatus = ComparisonStatus.DontKnow;
                foreach (ComparisonStatus status in this)
                {
                    finalStatus.And(status);
                }
                finalStatus.SetProcessed();
                return finalStatus.State;
            }
        }
    }
}