using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.WebApp.Tests.TestsWithFixture.Helper;

namespace Agenda.WebApp.Tests.TestsWithFixture.Fixture
{
    [CollectionDefinition(TestHelper.CollectionName)]
    public class CollectionFixture : ICollectionFixture<TestFixture>
    {
    }
}
