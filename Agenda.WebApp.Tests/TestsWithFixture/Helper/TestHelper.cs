using System.Reflection;

namespace Agenda.WebApp.Tests.TestsWithFixture.Helper
{
    public static class TestHelper
    {
        public static string PathTest => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public const string CollectionName = "Agenda Web Tests";
    }
}
