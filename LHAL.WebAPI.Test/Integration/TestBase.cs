using System.Transactions;
using NUnit.Framework;

namespace LHAL.WebAPI.Test.Integration
{
    [TestFixture]
    public class TestBase
    {
        private TransactionScope _transaction;

        [SetUp]
        public void Init()
        { _transaction = new TransactionScope(); }

        [TearDown]
        public void Cleanup()
        { _transaction.Dispose(); }
    }
}
