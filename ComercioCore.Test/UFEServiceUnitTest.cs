using ComercioCore.Application.Interfaces.Services;
using ComercioCore.Application.Services;
using ComercioCore.Domain.Entities;
using ComercioCore.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

public class UFEServiceUnitTests
{
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly Mock<IEstablecimientoRepository> _feeHistoryRepositoryMock;
    private readonly IUFEService _ufeService;

    public UFEServiceUnitTests()
    {
        _serviceProviderMock = new Mock<IServiceProvider>();
        _feeHistoryRepositoryMock = new Mock<IEstablecimientoRepository>();

        var serviceScopeMock = new Mock<IServiceScope>();
        var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
        
        serviceScopeMock.Setup(x => x.ServiceProvider).Returns(_serviceProviderMock.Object);
        serviceScopeFactoryMock.Setup(x => x.CreateScope()).Returns(serviceScopeMock.Object);

        _serviceProviderMock.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactoryMock.Object);
        _serviceProviderMock.Setup(x => x.GetService(typeof(IEstablecimientoRepository))).Returns(_feeHistoryRepositoryMock.Object);

        _ufeService = new UFEService(_serviceProviderMock.Object);
    }

    //[Fact]
    //public async Task GetCurrentFee_ShouldReturnCurrentFee()
    //{
    //    var expectedFee = 0.05m;
    //    var effectiveFrom = DateTime.UtcNow.AddHours(-2);

    //    // Configurar el mock del repositorio para devolver un FeeHistory válido
    //    _feeHistoryRepositoryMock.Setup(r => r.GetCurrentFeeHistory())
    //        .ReturnsAsync(new FeeHistory { FeeRate = expectedFee, EffectiveFrom = effectiveFrom });

    //    // Configurar el mock del scope para devolver el servicio mockeado
    //    var serviceScopeMock = new Mock<IServiceScope>();
    //    serviceScopeMock.Setup(x => x.ServiceProvider).Returns(new ServiceProviderMock(_feeHistoryRepositoryMock.Object));

    //    var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
    //    serviceScopeFactoryMock.Setup(x => x.CreateScope()).Returns(serviceScopeMock.Object);

    //    _serviceProviderMock.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactoryMock.Object);

    //    // Crear una instancia del servicio dentro del alcance
    //    var ufeService = new UFEService(_serviceProviderMock.Object);

    //    // Act
    //    var result = await ufeService.GetCurrentFee();

    //    // Assert
    //    Assert.Equal(expectedFee, result);

    //    _feeHistoryRepositoryMock.Verify(r => r.GetCurrentFeeHistory(), Times.Once);
    //}

    //public class ServiceProviderMock : IServiceProvider
    //{
    //    private readonly IFeeHistoryRepository _feeHistoryRepository;

    //    public ServiceProviderMock(IFeeHistoryRepository feeHistoryRepository)
    //    {
    //        _feeHistoryRepository = feeHistoryRepository;
    //    }

    //    public object GetService(Type serviceType)
    //    {
    //        if (serviceType == typeof(IFeeHistoryRepository))
    //        {
    //            return _feeHistoryRepository;
    //        }
    //        throw new NotImplementedException();
    //    }
    //}


    [Fact]
    public async Task GetCurrentFee_ShouldUpdateFeeAfterOneHour()
    {
        // Arrange
        var initialFee = 0.01m;
        var feeHistory = new FeeHistory
        {
            FeeRate = initialFee,
            EffectiveFrom = DateTime.UtcNow.AddHours(-2),
            EffectiveTo = DateTime.UtcNow.AddHours(-1)
        };

        _feeHistoryRepositoryMock.Setup(x => x.GetCurrentFeeHistory()).ReturnsAsync(feeHistory);

        // Act
        var result = await _ufeService.GetCurrentFee();

        // Assert
        Assert.NotEqual(initialFee, result);
    }

}

