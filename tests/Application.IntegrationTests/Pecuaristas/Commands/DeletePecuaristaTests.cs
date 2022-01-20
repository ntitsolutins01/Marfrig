using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Pecuaristas.Commands.Create;
using Application.Pecuaristas.Commands.Delete;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Pecuaristas.Commands
{
    public class DeletePecuaristaTests : TestBase
    {
        [Test]
        public void ShouldRequireValidPecuaristaId()
        {
            var command = new DeletePecuaristaCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeletePecuarista()
        {
            var city = await SendAsync(new CreatePecuaristaCommand
            {
                Name = "Kayseri"
            });

            await SendAsync(new DeletePecuaristaCommand
            {
                Id = city.Data.Id
            });

            var list = await FindAsync<CompraGadoItem>(city.Data.Id);

            list.Should().BeNull();
        }
    }
}
