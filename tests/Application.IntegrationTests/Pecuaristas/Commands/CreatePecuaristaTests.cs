using System;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Pecuaristas.Commands.Create;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Pecuaristas.Commands
{
    public class CreatePecuaristaTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreatePecuaristaCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();

        }

        [Test]
        public async Task ShouldRequireUniqueName()
        {
            await SendAsync(new CreatePecuaristaCommand
            {
                Name = "Bursa"
            });

            var command = new CreatePecuaristaCommand
            {
                Name = "Bursa"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreatePecuarista()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreatePecuaristaCommand
            {
                Name = "Kastamonu"
            };

            var result = await SendAsync(command);

            var list = await FindAsync<Pecuarista>(result.Data.Id);

            list.Should().NotBeNull();
            list.Nome.Should().Be(command.Name);
            list.Creator.Should().Be(userId);
            list.CreateDate.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
