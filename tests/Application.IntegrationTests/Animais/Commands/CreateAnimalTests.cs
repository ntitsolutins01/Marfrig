using System;
using System.Threading.Tasks;
using Application.Animais.Commands.Create;
using Application.Common.Exceptions;
using Application.Pecuaristas.Commands.Create;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Animais.Commands
{
    public class CreateAnimalTests
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateAnimalCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();

        }

        [Test]
        public async Task ShouldCreateAnimal()
        {
            var city = await SendAsync(new CreatePecuaristaCommand
            {
                Name = "Bursa"
            });

            var userId = await RunAsDefaultUserAsync();

            var command = new CreateAnimalCommand
            {
                Name = "Karacabey"
            };

            var result = await SendAsync(command);

            var list = await FindAsync<Animal>(result.Data.Id);

            list.Should().NotBeNull();
            list.Descricao.Should().Be(command.Name);
            list.Creator.Should().Be(userId);
            list.CreateDate.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
