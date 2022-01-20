using System;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Pecuaristas.Commands.Create;
using Application.Pecuaristas.Commands.Update;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Pecuaristas.Commands
{
    public class UpdatePecuaristaTests : TestBase
    {
        [Test]
        public void ShouldRequireValidPecuaristaId()
        {
            var command = new UpdatePecuaristaCommand
            {
                Id = 99,
                Name = "Kayseri"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldRequireUniqueName()
        {
            var city = await SendAsync(new CreatePecuaristaCommand
            {
                Name = "Malatya"
            });

            await SendAsync(new CreatePecuaristaCommand
            {
                Name = "Denizli"
            });

            var command = new UpdatePecuaristaCommand
            {
                Id = city.Data.Id,
                Name = "Denizli"
            };

            FluentActions.Invoking(() =>
                    SendAsync(command))
                .Should().Throw<ValidationException>().Where(ex => ex.Errors.ContainsKey("Name"))
                .And.Errors["Name"].Should().Contain("The specified city already exists. If you just want to activate the city leave the name field blank!");
        }

        [Test]
        public async Task ShouldUpdatePecuarista()
        {
            var userId = await RunAsDefaultUserAsync();

            var result = await SendAsync(new CreatePecuaristaCommand
            {
                Name = "Kayyysseri"
            });

            var command = new UpdatePecuaristaCommand
            {
                Id = result.Data.Id,
                Name = "Kayseri"
            };

            await SendAsync(command);

            var city = await FindAsync<Pecuarista>(result.Data.Id);

            city.Nome.Should().Be(command.Name);
            city.Modifier.Should().NotBeNull();
            city.Modifier.Should().Be(userId);
            city.ModifyDate.Should().NotBeNull();
            city.ModifyDate.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}
