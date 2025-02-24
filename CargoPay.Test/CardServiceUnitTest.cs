using CargoPay.Application.Interfaces.Services;
using CargoPay.Application.Services;
using CargoPay.Domain.Entities;
using CargoPay.Domain.Interfaces.Repositories;
using CargoPay.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

public class CardServiceUnitTests
{
    private readonly Mock<ICardRepository> _cardRepositoryMock;
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IUFEService> _ufeServiceMock;
    private readonly Mock<IMemoryCache> _cacheMock;
    private readonly CardService _cardService;
    private readonly ApplicationDbContext _dbContext;

    public CardServiceUnitTests()
    {
        _cardRepositoryMock = new Mock<ICardRepository>();
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _ufeServiceMock = new Mock<IUFEService>();
        _cacheMock = new Mock<IMemoryCache>();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        _dbContext = new ApplicationDbContext(options, new Mock<IConfiguration>().Object);

        _cardService = new CardService(
            _dbContext,
            _cardRepositoryMock.Object,
            _paymentRepositoryMock.Object,
            _cacheMock.Object,
            _ufeServiceMock.Object);
    }

    [Fact]
    public async Task CreateCardAsync_ShouldCreateCard()
    {
        var userId = "user123";
        var balance = 100m;
        var cardNumber = "123456789012345";

        _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync(It.IsAny<string>())).ReturnsAsync((Card)null);
        _cardRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Card>())).Returns(Task.CompletedTask);

        var result = await _cardService.CreateCardAsync(userId, balance);

        Assert.NotNull(result);
        Assert.Equal(15, result.Length);
        _cardRepositoryMock.Verify(x => x.GetByCardNumberAsync(It.IsAny<string>()), Times.Once);
        _cardRepositoryMock.Verify(x => x.AddAsync(It.Is<Card>(c => c.UserId == userId && c.Balance == balance)), Times.Once);
    }

    [Fact]
    public async Task PayAsync_ShouldThrowExceptionIfCardNotFound()
    {
        var cardNumber = "123456789012345";
        var amount = 50m;

        _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync(cardNumber)).ReturnsAsync((Card)null);

        await Assert.ThrowsAsync<System.Exception>(() => _cardService.PayAsync(cardNumber, amount));
    }

    [Fact]
    public async Task PayAsync_ShouldUpdateCardBalance()
    {
        var cardNumber = "123456789012345";
        var amount = 50m;
        var card = new Card { CardNumber = cardNumber, Balance = 100m, UserId = "user123" };

        _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync(cardNumber)).ReturnsAsync(card);
        _paymentRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Payment>())).Returns(Task.CompletedTask);
        _cardRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Card>())).Returns(Task.CompletedTask);

        await _cardService.PayAsync(cardNumber, amount);

        _cardRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Card>(c => c.CardNumber == cardNumber && c.Balance == 50m)), Times.Once);
    }

    [Fact]
    public async Task GetBalanceAsync_ShouldReturnBalance()
    {
        var cardNumber = "123456789012345";
        var card = new Card { CardNumber = cardNumber, Balance = 100m, UserId = "user123" };

        _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync(cardNumber)).ReturnsAsync(card);

        object cacheEntry;
        _cacheMock.Setup(m => m.TryGetValue(It.IsAny<object>(), out cacheEntry)).Returns(false);
        _cacheMock.Setup(m => m.CreateEntry(It.IsAny<object>())).Returns(Mock.Of<ICacheEntry>);

        var result = await _cardService.GetBalanceAsync(cardNumber);

        Assert.Equal(100m, result);
    }

    [Fact]
    public async Task GetBalanceAsync_ShouldThrowExceptionIfCardNotFound()
    {
        var cardNumber = "123456789012345";

        _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync(cardNumber)).ReturnsAsync((Card)null);

        await Assert.ThrowsAsync<Exception>(() => _cardService.GetBalanceAsync(cardNumber));
    }
}
