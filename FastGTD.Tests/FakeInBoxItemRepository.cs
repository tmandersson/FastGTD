using System.Collections.Generic;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;

namespace FastGTD.Tests
{
    public class FakeInBoxItemRepository : IInBoxItemRepository
    {
        public IList<InBoxItem> GetAll()
        {
            return new List<InBoxItem>();
        }

        public void DeleteAll()
        {
        }

        public InBoxItem CreateNew(string item)
        {
            return new InBoxItem(item);
        }

        public void Delete(InBoxItem item)
        {
        }
    }
}