using BankNTProject.Data.Repositories;
using BankNTProject.Service.DTOs.CreditDTOs;
using BankNTProject.Service.DTOs.UserDTOs;
using System;

namespace BankNTProject.Domain.Entities
{
    public class Credit : Auditable
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public float Duration { get; set; }
        public float Persantage { get; set; } = 23;

        public static implicit operator CreditViewModel(Credit credit)
        {
            var _userRepository = new UserRepository();
            return credit is null ? null : new CreditViewModel
            {
                Amount = credit.Amount,
                Duration = credit.Duration,
                User = (UserViewModel)_userRepository.Get(p => p.PrimeryKey == credit.UserId),
                Id = credit.PrimeryKey,

            };
        }
    }
}
