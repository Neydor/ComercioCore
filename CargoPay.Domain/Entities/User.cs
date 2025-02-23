using CargoPay.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CargoPay.Domain
{
    public class User : IdentityUser
    {
        public ICollection<Card?> Cards { get; set; } = [];

    }
}