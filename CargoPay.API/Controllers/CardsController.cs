using CargoPay.Application.DTOs.Resquests;
using CargoPay.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CargoPay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            var cardNumber = await _cardService.CreateCardAsync(userId, request.Balance);
            return Ok(new { CardNumber = cardNumber });
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] PayRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var card = await _cardService.GetCard(request.CardNumber);
            if (card == null || card.UserId != userId) return Unauthorized();
            var paymentId = await _cardService.PayAsync(request.CardNumber, request.Amount);
            return Ok(new { PaymentId = paymentId } );
        }

        [HttpGet("balance/{cardNumber}")]
        public async Task<IActionResult> GetBalance(string cardNumber)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var card = await _cardService.GetCard(cardNumber);
            if (card == null || card.UserId != userId) return Unauthorized();
            var balance = await _cardService.GetBalanceAsync(cardNumber);
            return Ok(new { Balance = balance });
        }
    }

}
