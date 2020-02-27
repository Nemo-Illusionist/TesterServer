using System.Diagnostics.CodeAnalysis;

namespace Tester.Db
{
    public static class DbConstant
    {
        [SuppressMessage("ReSharper", "CA1034")]
        public static class Scheme
        {
            public const string Default = "app";
            public const string Client = "client";
            public const string Report = "report";
        }
    }
}