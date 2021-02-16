using System;
using WireMock.Server;

namespace Pan.Web.Tests
{
    public class MockServerFixture : IDisposable
    {
        public readonly WireMockServer Server;

        public MockServerFixture()
        {
            Server = WireMockServer.Start();
        }

        public void Dispose()
        {
            Server.Stop();
            Server.Dispose();
        }
    }
}