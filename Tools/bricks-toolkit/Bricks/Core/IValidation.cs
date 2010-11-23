using System.Collections.Generic;

namespace Bricks.Core
{
    public interface IValidation
    {
        bool HasErrors();
        bool HasNoErrors();
        IList<Error> Errors { get; }
    }
}