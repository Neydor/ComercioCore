namespace ComercioCore.Application.DTOs.Responses;

public class CardResponse
{
    public Guid CardId { get; set; }
    public required string CardNumber { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
