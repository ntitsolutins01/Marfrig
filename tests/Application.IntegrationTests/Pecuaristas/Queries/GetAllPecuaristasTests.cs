using System.Threading.Tasks;
using Application.Pecuaristas.Queries.GetPecuaristas;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Pecuaristas.Queries
{
    public class GetAllPecuaristasTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllPecuaristas()
        {
            await AddAsync(new Pecuarista()
            {
                Nome = "Manisa",
            });

            var query = new GetAllPecuaristasQuery();

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Count.Should().BeGreaterThan(0);
        }
    }
}