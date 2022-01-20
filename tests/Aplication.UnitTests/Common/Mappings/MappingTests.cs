using System;
using Application.Dto;
using Domain.Entities;
using FluentAssertions;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IMapper _mapper;

        public MappingTests()
        {
            TypeAdapterConfig typeAdapterConfig = new TypeAdapterConfig();

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(typeAdapterConfig);
            services.AddScoped<IMapper, ServiceMapper>();

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            _mapper = scope.ServiceProvider.GetService<IMapper>();
        }


        [Test]
        [TestCase(typeof(CompraGadoItem), typeof(CompraGadoItemDto))]
        [TestCase(typeof(Pecuarista), typeof(CompraGadoDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }

        [Test]
        public void ShouldMappingCorrectly()
        {
            var result = new CompraGadoItem() { Id = 1, Quantidade = 10 };
            var resultDto = _mapper.Map<CompraGadoItem, CompraGadoItemDto>(result);
            resultDto.Quantidade.Should().Be(10);
        }
    }
}
